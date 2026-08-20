using MediatR;

namespace Store.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public record DeleteSupplierCommand(Guid Id) : IRequest;
}
