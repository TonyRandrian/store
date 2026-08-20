using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler(IProductRepository productRepository)
        : IRequestHandler<GetProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<PagedResult<ProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Product> products = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize);
            PagedResult<ProductResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = products.TotalRecords
            };

            foreach (Product product in products.Data)
            {
                result.Data.Add(new ProductResponse(product));
            }

            return result;
        }
    }
}
