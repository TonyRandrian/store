using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductCategoryUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<CategoryResponse> Execute(Guid productId)
        {
            Category? category = await ProductRepository.GetProductCategory(productId)
                ?? throw new KeyNotFoundException($"No product with the id {productId} found");

            return new CategoryResponse(category);
        }
    }
}
