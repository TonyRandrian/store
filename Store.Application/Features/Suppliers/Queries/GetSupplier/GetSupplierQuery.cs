using MediatR;
using Store.Application.DTOs.Suppliers;

namespace Store.Application.Features.Suppliers.Queries.GetSupplier
{
    public record GetSupplierQuery(Guid Id) : IRequest<SupplierResponse>;
}
