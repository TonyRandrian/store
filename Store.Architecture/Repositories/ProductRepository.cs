using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository(StoreDbContext context) : IProductRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<PagedResult<Product>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await Context.Products.CountAsync();
            List<Product> products = await Context.Products
                .Include(p => p.Suppliers)
                .Include(p => p.Images)
                .Include(p => p.Document)
                .Include(p => p.Category)
                .ThenInclude(c => c!.Parent)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                Data = products,
                TotalRecords = totalRecords
            };
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await Context.Products
                .Include(p => p.Suppliers)
                .Include(p => p.Images)
                .Include(p => p.Document)
                .Include(p => p.Category)
                .ThenInclude(c => c!.Parent)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            Context.Products.Update(product);
            await Context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product? product = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No product with the id {id} found");

            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            return await Context.Suppliers
                .AnyAsync(s => s.Products.Any(p => p.Id == id));
        }

        public async Task<Category?> GetProductCategory(Guid productId)
        {
            Category? category = await Context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Category)
                .FirstOrDefaultAsync();

            return category;
        }
    }
}
