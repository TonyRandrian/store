using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class GetCategoriesUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<PagedResult<CategoryResponse>> Execute(int pageNumber, int pageSize)
        {
            PagedResult<Category> categories = await CategoryRepository.GetAllAsync(pageNumber, pageSize);
            PagedResult<CategoryResponse> result = new()
            {
                TotalRecords = categories.TotalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            foreach (Category category in categories.Data)
            {
                result.Data.Add(new CategoryResponse(category));
            }

            return result;
        }
    }
}
