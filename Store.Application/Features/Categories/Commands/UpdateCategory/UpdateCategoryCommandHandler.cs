using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
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
            Category? category = await _categoryRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No category with the id {request.Id} found");

            Category? categoryParent = request.ParentCategoryId == null ? null :
                await _categoryRepository.GetByIdAsync(request.ParentCategoryId.Value);

            if (categoryParent != null && categoryParent.Id == request.Id)
            {
                throw new InvalidOperationException("Cannot be a parent of itself");
            }
            else if (request.ParentCategoryId != null && categoryParent == null)
            {
                throw new KeyNotFoundException($"No category with the id {request.ParentCategoryId} found");
            }

            // update
            category.Name = request.Name;
            category.Parent = categoryParent;

            // persistence
            category = await _categoryRepository.UpdateAsync(category);
            return new CategoryResponse(category);
        }
    }
}
