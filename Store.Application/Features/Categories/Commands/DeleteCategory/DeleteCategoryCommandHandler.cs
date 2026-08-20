using MediatR;
using Store.Application.Interfaces;

namespace Store.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.IsUsed(request.Id))
                throw new InvalidOperationException("Other Category or Product still use this category, cannot delete");

            await _categoryRepository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
