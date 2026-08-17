using Store.Application.Commons;
using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class GetCustomersUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;

        
        public async Task<PagedResult<CustomerResponse>> Execute(int pageNum, int pageSize)
        {
            PagedResult<Customer> customers = await CustomerRepository.GetAllAsync(pageNum, pageSize);
            PagedResult<CustomerResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = customers.TotalRecords
            };

            foreach (Customer customer in customers.Data)
            {
                result.Data.Add(new CustomerResponse(customer));
            }

            return result;
        }
    }
}
