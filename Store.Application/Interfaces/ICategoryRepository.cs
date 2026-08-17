using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<PagedResult<Category>> GetAllAsync(int pageNumber, int pageSize);
        public Task<Category?> GetByIdAsync(Guid id);
        public Task<Category> AddAsync(Category category);
        public Task<Category> UpdateAsync(Category category);
        public Task DeleteAsync(Guid id);
        public Task<bool> IsUsed(Guid id);
        public Task<bool> Exists(Guid id);
    }
}
