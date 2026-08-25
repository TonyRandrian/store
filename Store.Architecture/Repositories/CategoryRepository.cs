using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository(StoreDbContext context) : ICategoryRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<Category>> GetAllAsync(int pageNumber, int pageSize)
        {
            int totalRecords = await _context.Categories.CountAsync();

            List<Category> data = await _context.Categories
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
            return await _context.Categories
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task DeleteAsync(Guid id)
        {
            Category? category = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No category with the id {id} found");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            bool hasChildren = await _context.Categories.AnyAsync(c => c.Parent != null && c.Parent.Id == id);
            if (hasChildren) return true;

            return await _context.Products.AnyAsync(p => p.Category != null && p.Category.Id == id);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<PagedResult<Product>> GetCategoryProducts(Guid categoryId, int pageNumber, int pageSize)
        {
            IQueryable<Product> query = _context.Products.Where(p => p.Category.Id == categoryId);

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
            IQueryable<Category> query = _context.Categories
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
