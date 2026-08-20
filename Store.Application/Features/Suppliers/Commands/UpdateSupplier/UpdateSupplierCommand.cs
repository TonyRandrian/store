using MediatR;
using Store.Application.DTOs.Suppliers;

namespace Store.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public record UpdateSupplierCommand(Guid Id, string Name, HashSet<Guid> ProductsIds)
        : IRequest<SupplierResponse>;
}
