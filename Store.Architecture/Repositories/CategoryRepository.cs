using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository(StoreDbContext context) : ICategoryRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<List<Category>> GetAllAsync()
        {
            return await Context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await Context.Categories
                .Include(c => c.Parent)
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

        public async Task DeleteAsync(int id)
        {
            Category? category = await GetByIdAsync(id);

            if (category == null)
                return;

            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
        }
    }
}
