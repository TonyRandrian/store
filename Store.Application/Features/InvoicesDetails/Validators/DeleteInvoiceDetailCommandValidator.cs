using FluentValidation;
using Store.Application.Features.InvoicesDetails.Commands.DeleteInvoiceDetail;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.InvoicesDetails.Validators
{
    public class DeleteInvoiceDetailCommandValidator
        : AbstractValidator<DeleteInvoiceDetailCommand>
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
