using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class GetCustomersUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;

        
        public async Task<List<CustomerResponse>> Execute()
        {
            List<Customer> customers = await CustomerRepository.GetAllAsync();
            List<CustomerResponse> responses = [];

            foreach (Customer customer in customers)
            {
                responses.Add(new CustomerResponse(customer));
            }

            return responses;
        }
    }
}
