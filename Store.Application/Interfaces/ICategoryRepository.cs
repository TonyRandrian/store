using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task AddAsync(Category category);
        public Task UpdateAsync(Category category);
        public Task DeleteAsync(int id);
    }
}
