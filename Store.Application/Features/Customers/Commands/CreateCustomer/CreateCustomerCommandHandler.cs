using MediatR;
using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = new(request.Name);

            customer = await _customerRepository.AddAsync(customer);
            return new CustomerResponse(customer);
        }
    }
}
