using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<PagedResult<Customer>> GetAllAsync(int pageNum, int pageSize);
        public Task<Customer?> GetByIdAsync(Guid id);
        public Task<Customer> AddAsync(Customer customer);
        public Task<Customer> UpdateAsync(Customer customer);
        public Task DeleteAsync(Guid id);
        public Task<bool> IsUsed(Guid id);
    }
}
