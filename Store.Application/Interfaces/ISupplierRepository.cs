using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> GetAllAsync();
        public Task<Supplier?> GetByIdAsync(Guid id);
        public Task<Supplier> AddAsync(Supplier supplier);
        public Task<Supplier> UpdateAsync(Supplier supplier);
        public Task DeleteAsync(Guid id);
    }
}
