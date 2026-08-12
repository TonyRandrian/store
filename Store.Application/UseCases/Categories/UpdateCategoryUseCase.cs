using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<Category> Execute(int id, UpdateCategoryRequest request)
        {
            // validation
            Category? category= await CategoryRepository.GetByIdAsync(id)
                ?? throw new Exception($"No category with the id {id} found");

            Category? categoryParent = request.ParentCategoryId == null ? null : 
                await CategoryRepository.GetByIdAsync(request.ParentCategoryId.Value);

            // update
            category.Name = request.Name;
            category.Parent = categoryParent;

            // persistence
            return await CategoryRepository.UpdateAsync(category);
        }
    }
}
