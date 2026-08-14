using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class CreateSupplierUseCase(
        ISupplierRepository supplierRepository,
        IProductRepository productRepository
        )
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<SupplierResponse> Execute(CreateSupplierRequest request)
        {
            Supplier supplier = new(request.Name);

            foreach (Guid id in request.ProductsIds)
            {
                Product? product = await ProductRepository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"No product with the id {id} found");

                supplier.Products.Add(product);
            }

            supplier = await SupplierRepository.AddAsync(supplier);
            return new SupplierResponse(supplier);
        }
    }
}
