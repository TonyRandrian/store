using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Queries.GetSuppliers
{
    public class GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
        : IRequestHandler<GetSuppliersQuery, PagedResult<SupplierResponse>>
    {
        public ISupplierRepository _supplierRepository = supplierRepository;


        public async Task<PagedResult<SupplierResponse>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Supplier> suppliers = await _supplierRepository.GetAllAsync(
                request.PageNumber, request.PageSize);
            PagedResult<SupplierResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = suppliers.TotalRecords
            };

            foreach (Supplier supplier in suppliers.Data)
            {
                result.Data.Add(new SupplierResponse(supplier));
            }

            return result;
        }
    }
}
