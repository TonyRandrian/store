using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Suppliers.Queries.GetSupplierProducts
{
    public record GetSupplierProductsQuery(Guid Id, int PageNumber, int PageSize)
        : IRequest<PagedResult<ProductResponse>>;
}
