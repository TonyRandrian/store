using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Queries.GetCategoryProducts
{
    public class GetCategoryProductsQueryHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<GetCategoryProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<PagedResult<ProductResponse>> Handle(GetCategoryProductsQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Product> products = await _categoryRepository.GetCategoryProducts(
                request.CategoryId, request.PageNumber, request.PageSize);

            PagedResult<ProductResponse> response = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = products.TotalRecords
            };

            foreach (Product product in products.Data)
            {
                response.Data.Add(new ProductResponse(product));
            }

            return response;
        }
    }
}
