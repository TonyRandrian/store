using MediatR;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Products.Queries.GetProductCategory
{
    public record GetProductCategoryQuery(Guid ProductId) : IRequest<CategoryResponse>;
}
