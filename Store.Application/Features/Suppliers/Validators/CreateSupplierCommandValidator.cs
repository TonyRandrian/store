using FluentValidation;
using Store.Application.Features.Suppliers.Commands.CreateSupplier;

namespace Store.Application.Features.Suppliers.Validators
{
    public class CreateSupplierCommandValidator
        : AbstractValidator<CreateSupplierCommand>
    {

        public CreateSupplierCommandValidator() { }
    }
}
