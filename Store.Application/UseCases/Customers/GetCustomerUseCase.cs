using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class GetCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<CustomerResponse?> Execute(int id)
        {
            Customer? customer = await CustomerRepository.GetByIdAsync(id);

            return customer == null ? null : new CustomerResponse(customer);
        }
    }
}
