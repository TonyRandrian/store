using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> AGetAll();
        Task<Category> AGetById(int id);
        Task AAdd(Category category);
        Task AUpdate(Category category);
        Task ADelete(int id);
    }
}
