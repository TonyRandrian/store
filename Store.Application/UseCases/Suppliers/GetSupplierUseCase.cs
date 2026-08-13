using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class GetSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<SupplierResponse> Execute(int id)
        {
            Supplier supplier = await SupplierRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No supplier with the id {id} found");

            return new SupplierResponse(supplier);
        }
    }
}
