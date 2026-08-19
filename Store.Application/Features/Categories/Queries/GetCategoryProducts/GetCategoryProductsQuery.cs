using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Categories.Queries.GetCategoryProducts
{
    public record GetCategoryProductsQuery(Guid CategoryId, int PageNumber, int PageSize)
        : IRequest<PagedResult<ProductResponse>>;
}
