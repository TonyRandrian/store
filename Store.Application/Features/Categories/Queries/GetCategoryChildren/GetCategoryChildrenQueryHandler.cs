using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Queries.GetCategoryChildren
{
    public class GetCategoryChildrenQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoryChildrenQuery, PagedResult<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<PagedResult<CategoryResponse>> Handle(GetCategoryChildrenQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Category> categories = await _categoryRepository.GetCategoryChildren(request.CategoryId, request.PageNumber, request.PageSize)
                ?? throw new KeyNotFoundException($"No category with the id {request.CategoryId} found");

            PagedResult<CategoryResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
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
