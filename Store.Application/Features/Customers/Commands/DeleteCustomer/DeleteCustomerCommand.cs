using MediatR;

namespace Store.Application.Features.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerCommand(Guid Id) : IRequest;
}
