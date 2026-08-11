using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<Product?> Execute(int id)
        {
            return await ProductRepository.GetByIdAsync(id);
        }
    }
}
