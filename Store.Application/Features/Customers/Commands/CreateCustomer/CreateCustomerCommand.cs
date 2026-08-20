using MediatR;
using Store.Application.DTOs.Customers;

namespace Store.Application.Features.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand(string Name) : IRequest<CustomerResponse>;
}
