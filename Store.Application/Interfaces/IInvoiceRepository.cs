using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoice>> AGetAll();
        public Task<Invoice?> AGetById(int id);
        public Task AAdd(Invoice invoice);
        public Task AUpdate(Invoice invoice);
        public Task ADelete(int id);
    }
}
