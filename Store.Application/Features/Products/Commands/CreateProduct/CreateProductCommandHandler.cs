using MediatR;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository)
        : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ISupplierRepository _supplierRepository = supplierRepository;


        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // validation
            Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException($"No category with the id {request.CategoryId} found");

            // creation
            List<Supplier> suppliers = [];
            foreach (Guid id in request.SuppliersIds)
            {
                Supplier? supplier = await _supplierRepository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"No supplier with the id {id} found");

                suppliers.Add(supplier);
            }

            Product product = new(request.Name, request.Price, category, suppliers);

            // persistence
            await _productRepository.AddAsync(product);

            return new ProductResponse(product);
        }
    }
}
