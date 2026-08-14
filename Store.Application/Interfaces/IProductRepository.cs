using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(Guid id);
        public Task<Product> AddAsync(Product product);
        public Task<Product> UpdateAsync(Product product);
        public Task DeleteAsync(Guid id);
        public Task<bool> IsUsed(Guid id);
    }
}
