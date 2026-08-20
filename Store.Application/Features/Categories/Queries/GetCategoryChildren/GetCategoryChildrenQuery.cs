using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Categories.Queries.GetCategoryChildren
{
    public record GetCategoryChildrenQuery(Guid CategoryId, int PageNumber, int PageSize)
        : IRequest<PagedResult<CategoryResponse>>;
}
