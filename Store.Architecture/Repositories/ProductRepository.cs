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
            return await Context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
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
