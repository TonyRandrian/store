using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class UpdateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;
        private readonly ICategoryRepository CategoryRepository = categoryRepository;
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


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

            /// remove the supplier that are not in the request list
            List<Supplier> suppliersToRemove = [.. product.Suppliers.Where(supplier => !request.SuppliersIds.Contains(supplier.Id))];
            foreach (Supplier supplier in suppliersToRemove)
            {
                product.Suppliers.Remove(supplier);
            }

            /// add the suppliers that are not in the suppliers list yet
            List<int> existingSuppliersIds = [.. product.Suppliers.Select(p => p.Id)];
            foreach (int sid in request.SuppliersIds)
            {
                if (!existingSuppliersIds.Contains(sid))
                {
                    /// better use GetBy using batch later 
                    Supplier? supplier = await SupplierRepository.GetByIdAsync(sid)
                        ?? throw new KeyNotFoundException($"No supplier with the id {sid} found");

                    product.Suppliers.Add(supplier);
                }
            }

            // persistence
            product = await ProductRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
