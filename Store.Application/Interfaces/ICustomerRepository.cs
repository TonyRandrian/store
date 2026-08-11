using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> AGetAll();
        public Task<Customer?> AGetById(int id);
        public Task AAdd(Customer customer);
        public Task AUpdate(Customer customer);
        public Task ADelete(int id);
    }
}
