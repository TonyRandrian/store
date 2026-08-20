using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Queries.GetProductCategory
{
    public class GetProductCategoryQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetProductCategoryQuery, CategoryResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<CategoryResponse> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _productRepository.GetProductCategory(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            return new CategoryResponse(category);
        }
    }
}
