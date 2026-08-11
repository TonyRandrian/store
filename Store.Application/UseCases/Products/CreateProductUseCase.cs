using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class CreateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {

        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<Product> Execute(string name, decimal price, int categoryId)
        {
            // validation
            Category? category = await CategoryRepository.GetByIdAsync(categoryId) 
                ?? throw new Exception($"No category with the id {categoryId} found");

            // persistence
            Product product = new(name, price, category);
            return await ProductRepository.AddAsync(product);
        }
    }
}
