using FluentValidation;
using Store.Application.Features.Invoices.Commands.DeleteInvoice;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.InvoicesDetails.Validators
{
    public class DeleteInvoiceDetailCommandValidator
        : AbstractValidator<DeleteInvoiceCommand>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailRepository;


        public DeleteInvoiceDetailCommandValidator(IInvoiceDetailsRepository invoiceDetailsRepository)
        {
            _invoiceDetailRepository = invoiceDetailsRepository;

            RuleFor(i => i.Id)
                .MustAsync(async (id, cancellationToken) => await _invoiceDetailRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No invoice detail with the id {i.Id} found");
        }
    }
}
