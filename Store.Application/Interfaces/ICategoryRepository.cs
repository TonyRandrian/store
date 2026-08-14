using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category?> GetByIdAsync(Guid id);
        public Task<Category> AddAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task DeleteAsync(Guid id);
        public Task<bool> IsUsed(Guid id);
        public Task<bool> Exists(Guid id);
    }
}
