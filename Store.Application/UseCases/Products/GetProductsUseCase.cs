using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductsUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<List<ProductResponse>> Execute()
        {
            List<Product> products = await ProductRepository.GetAllAsync();
            List<ProductResponse> result = [];

            foreach (Product product in products)
            {
                result.Add(new ProductResponse(product));
            }

            return result;
        }
    }
}
