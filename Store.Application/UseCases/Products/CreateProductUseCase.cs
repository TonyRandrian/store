using Store.Application.DTOs.Categories;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class CreateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {

        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<ProductResponse> Execute(CreateProductRequest request)
        {
            // validation
            Category? category = await CategoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new Exception($"No category with the id {request.CategoryId} found");

            // persistence
            Product product = new(request.Name, request.Price, category);
            await ProductRepository.AddAsync(product);

            return new ProductResponse(product);
        }
    }
}
