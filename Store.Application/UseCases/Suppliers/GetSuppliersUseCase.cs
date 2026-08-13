using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class GetSuppliersUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<List<SupplierResponse>> Execute()
        {
            List<Supplier> suppliers = await SupplierRepository.GetAllAsync();
            List<SupplierResponse> responses = [];

            foreach (Supplier supplier in suppliers)
            {
                responses.Add(new SupplierResponse(supplier));
            }

            return responses;
        }
    }
}
