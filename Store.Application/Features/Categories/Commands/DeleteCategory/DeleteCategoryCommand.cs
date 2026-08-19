using MediatR;

namespace Store.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Guid>;
}
