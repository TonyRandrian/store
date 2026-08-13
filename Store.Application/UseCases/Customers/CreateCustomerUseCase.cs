using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;

        
        public async Task<CustomerResponse> Execute(CreateCustomerRequest request)
        {
            Customer customer = new(request.Name);

            customer = await CustomerRepository.AddAsync(customer);
            return new CustomerResponse(customer);
        }
    }
}
