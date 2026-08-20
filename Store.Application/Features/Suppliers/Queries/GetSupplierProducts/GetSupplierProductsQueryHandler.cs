using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Queries.GetSupplierProducts
{
    public class GetSupplierProductsQueryHandler(ISupplierRepository supplierRepository)
        : IRequestHandler<GetSupplierProductsQuery, PagedResult<ProductResponse>>
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;


        public async Task<PagedResult<ProductResponse>> Handle(GetSupplierProductsQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Product> products = await _supplierRepository.GetSupplierProducts(
                request.Id, request.PageNumber, request.PageSize);
            PagedResult<ProductResponse> response = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = products.TotalRecords
            };

            foreach (Product product in products.Data)
            {
                response.Data.Add(new ProductResponse(product));
            }

            return response;
        }
    }
}
