using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<PagedResult<Supplier>> GetAllAsync(int pageNum, int pageSize);
        public Task<Supplier?> GetByIdAsync(Guid id);
        public Task<Supplier> AddAsync(Supplier supplier);
        public Task<Supplier> UpdateAsync(Supplier supplier);
        public Task DeleteAsync(Guid id);
    }
}
