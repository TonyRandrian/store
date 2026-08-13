using Store.Application.DTOs.Suppliers;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Suppliers
{
    public class UpdateSupplierUseCase(
        ISupplierRepository supplierRepository,
        IProductRepository productRepository)
    {
        private readonly ISupplierRepository SupplierRepository = supplierRepository;
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<SupplierResponse> Execute(int id, UpdateSupplierRequest request)
        {
            // validation
            Supplier? supplier = await SupplierRepository.GetByIdAsync(id)
                ?? throw new Exception($"No supplier with the id {id} found");

            // udpate
            supplier.Name = request.Name;
            supplier.Products.Clear();

            foreach (int pid in request.ProductsIds)
            {
                Product? product = await ProductRepository.GetByIdAsync(pid) 
                    ?? throw new KeyNotFoundException($"No product with the id {pid} found");

                supplier.Products.Add(product);
            }

            // persistence
            supplier = await SupplierRepository.UpdateAsync(supplier);
            return new SupplierResponse(supplier);
        }
    }
}
