using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<CategoryResponse> Execute(Guid id, UpdateCategoryRequest request)
        {
            // validation
            Category? category= await CategoryRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No category with the id {id} found");

            Category? categoryParent = request.ParentCategoryId == null ? null : 
                await CategoryRepository.GetByIdAsync(request.ParentCategoryId.Value);

            if (categoryParent != null && categoryParent.Id == id)
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
            category = await CategoryRepository.UpdateAsync(category);
            return new CategoryResponse(category);
        }
    }
}
