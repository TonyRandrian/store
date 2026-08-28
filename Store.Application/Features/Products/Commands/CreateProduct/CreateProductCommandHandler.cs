using MediatR;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces.Repositories;
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
            Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            // creation
            List<Supplier> suppliers = await _supplierRepository.GetByIdsAsync(request.SuppliersIds);
            Product product = new(request.Name, request.Price, category, suppliers);

            // persistence
            await _productRepository.AddAsync(product);

            return new ProductResponse(product);
        }
    }
}
