using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class UpdateSupplierUseCase(
        ISupplierRepository supplierRepository,
        IProductRepository productRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<SupplierResponse> Execute(Guid id, UpdateSupplierRequest request)
        {
            // validation
            Supplier? supplier = await SupplierRepository.GetByIdAsync(id)
                ?? throw new Exception($"No supplier with the id {id} found");

            // udpate
            supplier.Name = request.Name;

            /// remove products that are not present in the request list
            List<Product> productsToRemove = [.. supplier.Products.Where(product => !request.ProductsIds.Contains(product.Id))];
            foreach (Product product in productsToRemove)
            {
                supplier.Products.Remove(product);
            }

            /// add only products that are not present in the supplier product list
            List<Guid> existingProductsIds = [.. supplier.Products.Select(p => p.Id)];
            foreach (Guid pid in request.ProductsIds)
            {
                if (!existingProductsIds.Contains(pid))
                {
                    /// better use GetBy using batch later 
                    Product? product = await ProductRepository.GetByIdAsync(pid)
                    ?? throw new KeyNotFoundException($"No product with the id {pid} found");

                    supplier.Products.Add(product);
                }
            }

            // persistence
            supplier = await SupplierRepository.UpdateAsync(supplier);
            return new SupplierResponse(supplier);
        }
    }
}
