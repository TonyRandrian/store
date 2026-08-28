using FluentValidation;
using Store.Application.Features.InvoicesDetails.Commands.CreateInvoiceDetail;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.InvoicesDetails.Validators
{
    public class CreateInvoiceDetailCommandValidator 
        : AbstractValidator<CreateInvoiceDetailCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;


        public CreateInvoiceDetailCommandValidator(
            IInvoiceRepository invoiceRepository,
            IProductRepository productRepository)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;

            RuleFor(i => i.InvoiceId)
                .MustAsync(async (id, cancellationToken) => await _invoiceRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No invoice with the id {i.InvoiceId} found");

            RuleFor(i => i.ProductId)
                .MustAsync(async (id, CancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(i => $"No product with the id {i.ProductId} found");

            RuleFor(i => i.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"Quantity should be greater or equal than 0");
        }
    }
}
