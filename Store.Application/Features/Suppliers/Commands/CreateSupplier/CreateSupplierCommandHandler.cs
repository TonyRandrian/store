using MediatR;
using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IProductRepository productReposiroty)
        : IRequestHandler<CreateSupplierCommand, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;
        private readonly IProductRepository _productRepository = productReposiroty;


        public async Task<SupplierResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier supplier = new(request.Name);

            foreach (Guid id in request.ProductsIds)
            {
                Product? product = await _productRepository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"No product with the id {id} found");

                supplier.Products.Add(product);
            }

            supplier = await _supplierRepository.AddAsync(supplier);
            return new SupplierResponse(supplier);
        }
    }
}
