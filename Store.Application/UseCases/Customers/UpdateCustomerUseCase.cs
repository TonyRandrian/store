using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class UpdateCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<CustomerResponse> Execute(int id, UpdateCustomerRequest request)
        {
            // validation
            Customer? customer = await CustomerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No customer with the id {id} found");

            // update
            customer.Name = request.Name;

            // persistence
            customer = await CustomerRepository.UpdateAsync(customer);
            return new CustomerResponse(customer);
        }
    }
}
