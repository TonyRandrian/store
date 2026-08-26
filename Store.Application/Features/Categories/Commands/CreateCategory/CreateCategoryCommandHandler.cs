using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? categoryParent = request.ParentCategoryId.HasValue
                ? await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value) : null;

            Category category = new(request.Name, categoryParent);
            await _categoryRepository.AddAsync(category);

            return new CategoryResponse(category);
        }
    }
}
