using MediatR;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, Guid? ParentCategoryId) : IRequest<CategoryResponse>;
}
