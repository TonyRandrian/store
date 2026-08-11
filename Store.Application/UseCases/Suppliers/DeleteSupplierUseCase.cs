using Store.Application.Interfaces;

namespace Store.Application.UseCases.Suppliers
{
    public class DeleteSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task Execute(int id)
        {
            await SupplierRepository.DeleteAsync(id);
        }
    }
}
