using MediatR;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces.Repositories;
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
            Product? product = await _productRepository.GetByIdAsync(request.Id);
            Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            HashSet<Guid> requestedSupplierIds = [.. request.SuppliersIds];

            // update
            product!.Name = request.Name;
            product.Price = request.Price;
            product.Category = category!;

            /// remove the supplier that are not in the request list
            List<Supplier> suppliersToRemove = [.. product.Suppliers.Where(supplier => !requestedSupplierIds.Contains(supplier.Id))];
            foreach (Supplier supplier in suppliersToRemove)
            {
                product.Suppliers.Remove(supplier);
            }

            /// add the suppliers that are not in the suppliers list yet
            HashSet<Guid> existingSuppliersIds = [.. product.Suppliers.Select(p => p.Id)];
            List<Guid> notExistingSuppliersIds = [.. requestedSupplierIds.Except(existingSuppliersIds)];

            List<Supplier> suppliers = await _supplierRepository.GetByIdsAsync(notExistingSuppliersIds);
            foreach (Supplier supplier in suppliers)
            {
                product.Suppliers.Add(supplier);
            }

            // persistence
            product = await _productRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
