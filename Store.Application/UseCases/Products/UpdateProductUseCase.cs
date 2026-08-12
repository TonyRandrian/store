using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class UpdateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<ProductResponse> Excecute(int id, UpdateProductRequest request)
        {
            // validation
            Product? product = await ProductRepository.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException($"No product with the id {id} found");

            Category? category = await CategoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException($"No category with the id {request.CategoryId} found");

            // update
            product.Name = request.Name;
            product.Price = request.Price;
            product.Category = category;

            // persistence
            product = await ProductRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
