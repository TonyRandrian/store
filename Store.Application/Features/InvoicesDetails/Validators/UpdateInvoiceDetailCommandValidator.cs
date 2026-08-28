using FluentValidation;
using Store.Application.Features.InvoicesDetails.Commands.UpdateInvoiceDetail;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.InvoicesDetails.Validators
{
    public class UpdateInvoiceDetailCommandValidator
        : AbstractValidator<UpdateInvoiceDetailCommand>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository;
        private readonly IProductRepository _productRepository;


        public UpdateInvoiceDetailCommandValidator(
            IInvoiceDetailsRepository invoiceDetailsRepository,
            IProductRepository productRepository)
        {
            _invoiceDetailsRepository = invoiceDetailsRepository;
            _productRepository = productRepository;

            RuleFor(i => i.Id)
                .MustAsync(async (id, CancellationToken) => await _invoiceDetailsRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No invoice detail with the id {i.Id} found");

            RuleFor(i => i.ProductId)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No product with the id {i.ProductId} found");

            RuleFor(i => i.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity should be greater than or equal to 0");
        }
    }
}
