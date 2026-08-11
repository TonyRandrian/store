using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class GetSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<Supplier?> Execute(int id)
        {
            return await SupplierRepository.GetByIdAsync(id);
        }
    }
}
