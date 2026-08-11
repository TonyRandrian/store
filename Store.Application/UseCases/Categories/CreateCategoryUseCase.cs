using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class CreateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<Category> Execute(string name, int categoryParentId)
        {
            Category? categoryParent = await CategoryRepository.GetByIdAsync(categoryParentId);

            Category category = new(name, categoryParent);
            return await CategoryRepository.AddAsync(category);
        }
    }
}
