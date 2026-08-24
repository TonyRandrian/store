using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository(StoreDbContext context) : IProductRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<Product>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Products.CountAsync();
            List<Product> products = await _context.Products
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
            return await _context.Products
                .Include(p => p.Suppliers)
                .Include(p => p.Images)
                .Include(p => p.Document)
                .Include(p => p.Category)
                .ThenInclude(c => c!.Parent)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            Product? product = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No product with the id {id} found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            return await _context.Suppliers
                .AnyAsync(s => s.Products.Any(p => p.Id == id));
        }

        public async Task<Category?> GetProductCategory(Guid productId)
        {
            Category? category = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Category)
                .FirstOrDefaultAsync();

            return category;
        }
    }
}
