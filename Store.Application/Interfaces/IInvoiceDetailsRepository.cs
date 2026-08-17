using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IInvoiceDetailsRepository
    {
        public Task<PagedResult<InvoiceDetail>> GetAllAsync(int pageNum, int pageSize);
        public Task<InvoiceDetail?> GetByIdAsync(Guid id);
        public Task<InvoiceDetail> AddAsync(InvoiceDetail invoiceDetail);
        public Task<InvoiceDetail> UpdateAsync(InvoiceDetail invoiceDetail);
        public Task DeleteAsync(Guid id);
    }
}
