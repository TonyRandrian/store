using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository(StoreDbContext context) : IProductRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<List<Product>> AGetAll()
        {
            return await Context.Products.ToListAsync();
        }

        public async Task<Product?> AGetById(int id)
        {
            return await Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AAdd(Product product)
        {
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();
        }

        public async Task AUpdate(Product product)
        {
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
        }

        public async Task ADelete(int id)
        {
            Product? product = await Context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return;

            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
        }
    }
}
