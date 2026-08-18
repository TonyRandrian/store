using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class GetCategoryChildrenUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<PagedResult<CategoryResponse>> Execute(Guid categoryId, int pageNum, int pageSize)
        {
            PagedResult<Category> categories = await CategoryRepository.GetCategoryChildren(categoryId, pageNum, pageSize)
                ?? throw new KeyNotFoundException($"No category with the id {categoryId} found");

            PagedResult<CategoryResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = categories.TotalRecords
            };

            foreach (Category category in categories.Data)
            {
                result.Data.Add(new CategoryResponse(category));
            }

            return result;
        }
    }
}
