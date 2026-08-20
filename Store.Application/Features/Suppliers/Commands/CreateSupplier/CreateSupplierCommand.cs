using MediatR;
using Store.Application.DTOs.Suppliers;

namespace Store.Application.Features.Suppliers.Commands.CreateSupplier
{
    public record CreateSupplierCommand(string Name, HashSet<Guid> ProductsIds) : IRequest<SupplierResponse>;
}
