using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class GetSuppliersUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<List<Supplier>> Execute()
        {
            return await SupplierRepository.GetAllAsync();
        }
    }
}
