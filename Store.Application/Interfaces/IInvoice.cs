using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IInvoice
    {
        Task<List<Invoice>> AGetAll();
        Task<Invoice> AGetById(int id);
        Task AAdd(Invoice invoice);
        Task AUpdate(Invoice invoice);
        Task ADelete(int id);
    }
}
