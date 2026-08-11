using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class UpdateProductUseCase(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;

        public async Task<Product> Excecute(int id, string name, decimal price, int categoryId)
        {
            // validation
            Product? product = await ProductRepository.GetByIdAsync(id) 
                ?? throw new Exception($"No product with the id {id} found");

            Category? category = await CategoryRepository.GetByIdAsync(categoryId)
                ?? throw new Exception($"No category with the id {categoryId} found");

            // update
            product.Name = name;
            product.Price = price;
            product.Category = category;

            // persistence
            await ProductRepository.UpdateAsync(product);
            return product;
        }
    }
}
