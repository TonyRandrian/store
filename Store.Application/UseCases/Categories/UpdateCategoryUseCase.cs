using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<Category> Execute(int id, string name, int categoryParentId)
        {
            // validation
            Category? category= await CategoryRepository.GetByIdAsync(id)
                ?? throw new Exception($"No category with the id {id} found");

            Category? categoryParent = await CategoryRepository.GetByIdAsync(categoryParentId);

            // update
            category.Name = name;
            category.Parent = categoryParent;

            // persistence
            return await CategoryRepository.UpdateAsync(category);
        }
    }
}
