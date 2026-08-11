using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> AGetAll();
        public Task<Supplier?> AGetById(int id);
        public Task AAdd(Supplier supplier);
        public Task AUpdate(Supplier supplier);
        public Task ADelete(int id);
    }
}
