using MediatR;
using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierCommandHandler(
        ISupplierRepository supplierRepository, 
        IProductRepository productRepository) 
        : IRequestHandler<UpdateSupplierCommand, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<SupplierResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            // validation
            Supplier? supplier = await _supplierRepository.GetByIdAsync(request.Id)
                ?? throw new Exception($"No supplier with the id {request.Id} found");

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
                    Product? product = await _productRepository.GetByIdAsync(pid)
                    ?? throw new KeyNotFoundException($"No product with the id {pid} found");

                    supplier.Products.Add(product);
                }
            }

            // persistence
            supplier = await _supplierRepository.UpdateAsync(supplier);
            return new SupplierResponse(supplier);
        }
    }
}
