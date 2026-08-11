using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ISupplier
    {
        Task<List<Supplier>> AGetAll();
        Task<Supplier> GetById(int id);
        Task AAdd(Supplier supplier);
        Task AUpdate(Supplier supplier);
        Task ADelete(int id);
    }
}
