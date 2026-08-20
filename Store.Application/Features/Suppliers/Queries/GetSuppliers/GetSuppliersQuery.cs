using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Suppliers;

namespace Store.Application.Features.Suppliers.Queries.GetSuppliers
{
    public record GetSuppliersQuery(int PageNumber, int PageSize) : IRequest<PagedResult<SupplierResponse>>;
}
