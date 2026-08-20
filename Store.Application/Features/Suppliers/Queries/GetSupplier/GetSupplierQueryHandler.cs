using MediatR;
using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Queries.GetSupplier
{
    public class GetSupplierQueryHandler(ISupplierRepository supplierRepository)
        : IRequestHandler<GetSupplierQuery, SupplierResponse>
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;


        public async Task<SupplierResponse> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No supplier with the id {request.Id} found");

            return new SupplierResponse(supplier);
        }
    }
}
