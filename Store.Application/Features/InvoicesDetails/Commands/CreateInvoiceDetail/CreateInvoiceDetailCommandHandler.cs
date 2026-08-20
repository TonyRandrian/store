using MediatR;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.InvoicesDetails.Commands.CreateInvoiceDetail
{
    public class CreateInvoiceDetailCommandHandler(
        IInvoiceDetailsRepository invoiceDetailsRepository,
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository)
        : IRequestHandler<CreateInvoiceDetailCommand, InvoiceDetailResponse>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailRepository = invoiceDetailsRepository;
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<InvoiceDetailResponse> Handle(CreateInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetByIdAsync(request.InvoiceId)
                ?? throw new KeyNotFoundException($"No invoice with the id {request.InvoiceId} found");

            Product? product = await _productRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            InvoiceDetail invoiceDetail = new(invoice, product, request.Quantity);
            invoiceDetail = await _invoiceDetailRepository.AddAsync(invoiceDetail);

            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
