using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository(StoreDbContext context) : IProductRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<List<Product>> GetAllAsync()
        {
            return await Context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Context.Products
                .Include(p => p.Category)
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

        public async Task DeleteAsync(int id)
        {
            Product? product = await GetByIdAsync(id);

            if (product == null)
                return;

            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
        }
    }
}
