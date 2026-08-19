using MediatR;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryQuery(Guid Id) : IRequest<CategoryResponse?>;
}
