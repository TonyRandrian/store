using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<ProductResponse?> Execute(Guid id)
        {
            Product? product = await ProductRepository.GetByIdAsync(id);
            return product == null ? null : new(product);
        }
    }
}
