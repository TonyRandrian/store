using FluentValidation;
using Store.Application.Features.Invoices.Commands.CreateInvoice;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Invoices.Validators
{
    public class CreateInvoiceCommandValidator
        : AbstractValidator<CreateInvoiceCommand>
    {
        private readonly ICustomerRepository _customerRepository;


        public CreateInvoiceCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(i => i.CustomerId)
                .MustAsync(async (id, cancellationToken) => await _customerRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No customer with the id {i.CustomerId} found");

            RuleFor(i => i.Total)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Invoice total must be greater than or equal to 0");
        }
    }
}
