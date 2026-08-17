using Store.Application.Commons;
using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class GetSuppliersUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<PagedResult<SupplierResponse>> Execute(int pageNum, int pageSize)
        {
            PagedResult<Supplier> suppliers = await SupplierRepository.GetAllAsync(pageNum, pageSize);
            PagedResult<SupplierResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
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
