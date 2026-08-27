using FluentValidation;
using Store.Application.Features.Suppliers.Commands.DeleteSupplier;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Suppliers.Validators
{
    public class DeleteSupplierCommandValidator
        : AbstractValidator<DeleteSupplierCommand>
    {
        private readonly ISupplierRepository _supplierRepository;


        public DeleteSupplierCommandValidator(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;

            RuleFor(s => s.Id)
                .MustAsync(async (id, cancellationToken) => await _supplierRepository.GetByIdAsync(id) != null)
                .WithMessage(s => $"No supplier with the id {s.Id} found");
        }
    }
}
