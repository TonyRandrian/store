using MediatR;
using Store.Application.DTOs.Categories;

namespace Store.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, Guid? ParentCategoryId)
        : IRequest<CategoryResponse>;
}
