using FluentValidation;
using Store.Application.Features.Invoices.Commands.UpdateInvoice;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Invoices.Validators
{
    public class UpdateInvoiceCommandValidator 
        : AbstractValidator<UpdateInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;


        public UpdateInvoiceCommandValidator(
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;

            RuleFor(i => i.Id)
                .MustAsync(async (id, cancellationToken) => await _invoiceRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No invoice with the id {i.Id} found");

            RuleFor(i => i.CustomerId)
                .MustAsync(async (id, cancellationToken) => await _customerRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No customer with the id {i.CustomerId} found");
        }
    }
}
