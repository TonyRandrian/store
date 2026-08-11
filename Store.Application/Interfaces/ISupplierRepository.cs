using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> GetAllAsync();
        public Task<Supplier?> GetByIdAsync(int id);
        public Task AddAsync(Supplier supplier);
        public Task UpdateAsync(Supplier supplier);
        public Task DeleteAsync(int id);
    }
}
