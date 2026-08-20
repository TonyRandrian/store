using MediatR;
using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler(ICustomerRepository customerRepository)
        : IRequestHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No customer with the id {request.Id} found");

            return new CustomerResponse(customer);
        }
    }
}
