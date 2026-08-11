using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> AGetAll();
        public Task<Category?> AGetById(int id);
        public Task AAdd(Category category);
        public Task AUpdate(Category category);
        public Task ADelete(int id);
    }
}
