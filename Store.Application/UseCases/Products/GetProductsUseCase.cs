using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductsUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<List<Product>> Execute()
        {
            return await ProductRepository.GetAllAsync();
        }
    }
}
