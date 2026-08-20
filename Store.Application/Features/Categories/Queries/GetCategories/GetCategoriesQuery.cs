using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery(int PageNumber, int PageSize) : IRequest<PagedResult<CategoryResponse>>;
}
