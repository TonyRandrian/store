using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<PagedResult<Invoice>> GetAllAsync(int pageNum, int pageSize);
        public Task<Invoice?> GetByIdAsync(Guid id);
        public Task<Invoice> AddAsync(Invoice invoice);
        public Task<Invoice> UpdateAsync(Invoice invoice);
        public Task DeleteAsync(Guid id);
    }
}
