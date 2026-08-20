using MediatR;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No product with the id {request.Id} found");

            return new(product);
        }
    }
}
