using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler(ICustomerRepository customerRepository)
        : IRequestHandler<GetCustomersQuery, PagedResult<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<PagedResult<CustomerResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Customer> customers = await _customerRepository.GetAllAsync(
                request.PageNumber, request.PageSize);
            PagedResult<CustomerResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
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
