using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoice>> GetAllAsync();
        public Task<Invoice?> GetByIdAsync(int id);
        public Task<Invoice> AddAsync(Invoice invoice);
        public Task<Invoice> UpdateAsync(Invoice invoice);
        public Task DeleteAsync(int id);
    }
}
