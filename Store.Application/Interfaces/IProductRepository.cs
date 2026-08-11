using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> AGetAll();
        Task<Product> AGetById(int id);
        Task AAdd(Product product);
        Task AUpdate(Product product);
        Task ADelete(int id);
    }
}
