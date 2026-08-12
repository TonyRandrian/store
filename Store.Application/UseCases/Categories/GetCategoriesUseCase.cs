using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class GetCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<List<CategoryResponse>> Execute()
        {
            List<CategoryResponse> result = [];
            List<Category> categories = await CategoryRepository.GetAllAsync();

            foreach (Category category in categories)
            {
                result.Add(new CategoryResponse(category.Id, category.Name));
            }

            return result;
        }
    }
}
