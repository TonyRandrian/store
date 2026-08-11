using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class CreateSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<Supplier> Execute(string name)
        {
            Supplier supplier = new(name);

            return await SupplierRepository.AddAsync(supplier);
        }
    }
}
