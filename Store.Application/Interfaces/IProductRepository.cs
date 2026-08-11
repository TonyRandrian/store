using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> AGetAll();
        public Task<Product?> AGetById(int id);
        public Task AAdd(Product product);
        public Task AUpdate(Product product);
        public Task ADelete(int id);
    }
}
