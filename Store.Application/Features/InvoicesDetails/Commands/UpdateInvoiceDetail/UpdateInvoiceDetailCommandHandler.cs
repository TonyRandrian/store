using MediatR;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.InvoicesDetails.Commands.UpdateInvoiceDetail
{
    public class UpdateInvoiceDetailCommandHandler(
        IInvoiceDetailsRepository invoiceDetailsRepository,
        IProductRepository productRepository)
        : IRequestHandler<UpdateInvoiceDetailCommand, InvoiceDetailResponse>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository = invoiceDetailsRepository;
        private readonly IProductRepository _productRepository = productRepository;


        public async Task<InvoiceDetailResponse> Handle(UpdateInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
            InvoiceDetail? invoiceDetail = await _invoiceDetailsRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {request.Id} found");

            Product? product = await _productRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            invoiceDetail.Product = product;
            invoiceDetail.Quantity = request.Quantity;

            invoiceDetail = await _invoiceDetailsRepository.UpdateAsync(invoiceDetail);
            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
