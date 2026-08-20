using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery(int PageNumber, int PageSize) : IRequest<PagedResult<ProductResponse>>;
}
