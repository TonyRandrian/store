using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class DeleteSupplierUseCase(ISupplierRepository supplierRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;


        public async Task Execute(Guid id)
        {
            Supplier? supplier = await SupplierRepository.GetByIdAsync(id);
            if (supplier != null && supplier.Products.Count > 0)
            {
                throw new InvalidOperationException("This supplier is linked to products, cannot delete");
            }

            await SupplierRepository.DeleteAsync(id);
        }
    }
}
