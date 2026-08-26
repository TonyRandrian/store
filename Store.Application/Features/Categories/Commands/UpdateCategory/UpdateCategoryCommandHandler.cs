using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // validation
            Category category = await _categoryRepository.GetByIdAsync(request.Id)
                ?? throw new InvalidOperationException("Category not found despite passing validation");

            Category? categoryParent = request.ParentCategoryId.HasValue
                ? await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value)
                : null;

            // update
            category.Name = request.Name;
            category.Parent = categoryParent;

            // persistence
            category = await _categoryRepository.UpdateAsync(category);
            return new CategoryResponse(category);
        }
    }
}
