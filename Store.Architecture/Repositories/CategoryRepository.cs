using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository(StoreDbContext context) : ICategoryRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<PagedResult<Category>> GetAllAsync(int pageNumber, int pageSize)
        {
            int totalRecords = await Context.Categories.CountAsync();

            List<Category> data = await Context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Products)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Category>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await Context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await Context.Categories.AddAsync(category);
            await Context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Guid id)
        {
            Category? category = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No category with the id {id} found");

            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            bool hasChildren = await Context.Categories.AnyAsync(c => c.Parent != null && c.Parent.Id == id);
            if (hasChildren) return true;

            return await Context.Products.AnyAsync(p => p.Category != null && p.Category.Id == id);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await Context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<PagedResult<Product>> GetCategoryProducts(Guid categoryId, int pageNumber, int pageSize)
        {
            IQueryable<Product> query = Context.Products.Where(p => p.Category.Id == categoryId);

            int totalRecords = await query.CountAsync();
            List<Product> products = await query
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = products,
                TotalRecords = totalRecords
            };
        }

        public async Task<PagedResult<Category>?> GetCategoryChildren(Guid categoryId, int pageNum, int pageSize)
        {
            IQueryable<Category> query = Context.Categories
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Children)
                .Include(c => c.Parent)
                .Include(c => c.Products);

            int totalRecords = await query.CountAsync();
            List<Category>? categories = await query
                .OrderBy(c => c.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return categories == null
                ? null
                : new PagedResult<Category>()
                {
                    PageNumber = pageNum,
                    PageSize = pageSize,
                    Data = categories,
                    TotalRecords = totalRecords
                };
        }
    }
}
