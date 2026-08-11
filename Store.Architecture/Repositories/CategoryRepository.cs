using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext Context;
        

        public async Task<List<Category>> AGetAll()
        {
            return await Context.Categories.ToListAsync();
        }

        public async Task<Category?> AGetById(int id)
        {
            return await Context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AAdd(Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
        }

        public async Task AUpdate(Category category)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
        }

        public async Task ADelete(int id)
        {
            Category? category = await AGetById(id);

            if (category == null)
                return;

            Context.Categories.Remove(category);
            await Context.SaveChangesAsync();
        }
    }
}
