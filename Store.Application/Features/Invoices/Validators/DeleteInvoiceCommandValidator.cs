using FluentValidation;
using Store.Application.Features.Invoices.Commands.DeleteInvoice;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Invoices.Validators
{
    public class DeleteInvoiceCommandValidator
        : AbstractValidator<DeleteInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;


        public DeleteInvoiceCommandValidator(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;

            RuleFor(i => i.Id)
                .MustAsync(async (id, cancellationToken) => await _invoiceRepository.GetByIdAsync(id) == null)
                .WithMessage(i => $"No invoice with the id {i.Id} found");
        }
    }
}
