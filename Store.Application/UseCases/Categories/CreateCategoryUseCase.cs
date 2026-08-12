using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class CreateCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<CategoryResponse> Execute(CreateCategoryRequest request)
        {
            Category? categoryParent = await CategoryRepository.GetByIdAsync(request.ParentCategoryId);

            Category category = new(request.Name, categoryParent);
            await CategoryRepository.AddAsync(category);

            return new CategoryResponse(category.Id, category.Name, categoryParent?.Id);
        }
    }
}
