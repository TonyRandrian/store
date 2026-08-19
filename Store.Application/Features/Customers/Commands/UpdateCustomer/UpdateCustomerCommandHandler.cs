using MediatR;
using Store.Application.DTOs.Customers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // validation
            Customer? customer = await _customerRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No customer with the id {request.Id} found");

            // update
            customer.Name = request.Name;

            // persistence
            customer = await _customerRepository.UpdateAsync(customer);
            return new CustomerResponse(customer);
        }
    }
}
