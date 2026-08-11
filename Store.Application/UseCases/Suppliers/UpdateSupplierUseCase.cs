using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class UpdateSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task<Supplier> Execute(int id, string name)
        {
            // validation
            Supplier? supplier = await SupplierRepository.GetByIdAsync(id)
                ?? throw new Exception($"No supplier with the id {id} found");

            // udpate
            supplier.Name = name;

            // persistence
            return await SupplierRepository.UpdateAsync(supplier);
        }
    }
}
