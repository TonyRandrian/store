using MediatR;
using Store.Application.DTOs.Customers;

namespace Store.Application.Features.Customers.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand(Guid Id, string Name) : IRequest<CustomerResponse>;
}
