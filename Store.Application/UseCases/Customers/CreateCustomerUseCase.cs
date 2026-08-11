using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;

        
        public async Task<Customer> Execute(string name)
        {
            Customer customer = new(name);

            return await CustomerRepository.AddAsync(customer);
        }
    }
}
