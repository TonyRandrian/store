using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICustomer
    {
        Task<List<Customer>> AGetAll();
        Task<Customer> AGetById(int id);
        Task AAdd(Customer customer);
        Task AUpdate(Customer customer);
        Task ADelete(Customer customer);
    }
}
