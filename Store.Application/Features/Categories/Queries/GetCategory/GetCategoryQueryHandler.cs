using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoryQuery, CategoryResponse?>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<CategoryResponse?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetByIdAsync(request.Id);

            return category == null ? null : new CategoryResponse(category);
        }
    }
}
