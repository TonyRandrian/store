using MediatR;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<DeleteCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
