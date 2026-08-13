using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class GetCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<CategoryResponse?> Execute(int id)
        {
            Category? category = await CategoryRepository.GetByIdAsync(id);

            return category == null ? null : new CategoryResponse(category);
        }
    }
}
