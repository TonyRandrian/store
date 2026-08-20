using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<PagedResult<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Category> categories = await _categoryRepository.GetAllAsync(request.PageNumber, request.PageSize);
            PagedResult<CategoryResponse> result = new()
            {
                TotalRecords = categories.TotalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            foreach (Category category in categories.Data)
            {
                result.Data.Add(new CategoryResponse(category));
            }

            return result;
        }
    }
}
