using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id);
        public Task<Customer> AddAsync(Customer customer);
        public Task<Customer> UpdateAsync(Customer customer);
        public Task DeleteAsync(int id);
    }
}
