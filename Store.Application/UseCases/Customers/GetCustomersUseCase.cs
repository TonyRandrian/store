using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class GetCustomersUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;

        
        public async Task<List<Customer>> Execute()
        {
            return await CustomerRepository.GetAllAsync();
        }
    }
}
