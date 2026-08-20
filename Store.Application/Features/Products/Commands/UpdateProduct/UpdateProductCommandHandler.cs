using MediatR;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
        : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ISupplierRepository _supplierRepository = supplierRepository;


        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // validation
            Product? product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No product with the id {request.Id} found");

            Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId)
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
            List<Guid> existingSuppliersIds = [.. product.Suppliers.Select(p => p.Id)];
            foreach (Guid sid in request.SuppliersIds)
            {
                if (!existingSuppliersIds.Contains(sid))
                {
                    /// better use GetBy using batch later 
                    Supplier? supplier = await _supplierRepository.GetByIdAsync(sid)
                        ?? throw new KeyNotFoundException($"No supplier with the id {sid} found");

                    product.Suppliers.Add(supplier);
                }
            }

            // persistence
            product = await _productRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
