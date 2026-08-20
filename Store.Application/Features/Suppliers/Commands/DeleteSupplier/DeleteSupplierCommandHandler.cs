using MediatR;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Suppliers.Commands.DeleteSupplier
{
    public class DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
        : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository = supplierRepository;


        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            Supplier? supplier = await _supplierRepository.GetByIdAsync(request.Id);
            if (supplier != null && supplier.Products.Count > 0)
            {
                throw new InvalidOperationException("This supplier is linked to products, cannot delete");
            }

            await _supplierRepository.DeleteAsync(request.Id);
        }
    }
}
