using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class UpdateCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<Customer> Execute(int id, string name)
        {
            // validation
            Customer? customer = await CustomerRepository.GetByIdAsync(id)
                ?? throw new Exception($"No customer with the id {id} found");

            // update
            customer.Name = name;

            // persistence
            return await CustomerRepository.UpdateAsync(customer);
        }
    }
}
