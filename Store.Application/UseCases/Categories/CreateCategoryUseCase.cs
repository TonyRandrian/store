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
            Category? categoryParent = null;
            if (request.ParentCategoryId != null)
            {
                Guid id = request.ParentCategoryId.Value;

                if (!await CategoryRepository.Exists(id))
                {
                    throw new KeyNotFoundException($"No category with id {id} found, cannot create parent");
                }

                categoryParent = await CategoryRepository.GetByIdAsync(id);
            }

            Category category = new(request.Name, categoryParent);
            await CategoryRepository.AddAsync(category);

            return new CategoryResponse(category);
        }
    }
}
