using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class CreateProductUseCase(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
    {

        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;
        private readonly ISupplierRepository SupplierRepository = supplierRepository; 

        public async Task<ProductResponse> Execute(CreateProductRequest request)
        {
            // validation
            Category? category = await CategoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException($"No category with the id {request.CategoryId} found");

            // creation
            List<Supplier> suppliers = [];
            foreach (Guid id in request.SuppliersIds)
            {
                Supplier? supplier = await SupplierRepository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"No supplier with the id {id} found");

                suppliers.Add(supplier);
            }

            Product product = new(request.Name, request.Price, category, suppliers);

            // persistence
            await ProductRepository.AddAsync(product);

            return new ProductResponse(product);
        }
    }
}
