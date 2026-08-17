using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.InvoicesDetails
{
    public class CreateInvoiceDetailUseCase(
        IInvoiceDetailsRepository invoiceDetailsRepository,
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository)
    {
        private readonly IInvoiceDetailsRepository InvoiceDetailRepository = invoiceDetailsRepository;
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<InvoiceDetailResponse> Execute(CreateInvoiceDetailRequest request) 
        {
            Invoice? invoice = await InvoiceRepository.GetByIdAsync(request.InvoiceId)
                ?? throw new KeyNotFoundException($"No invoice with the id {request.InvoiceId} found");

            Product? product = await ProductRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            InvoiceDetail invoiceDetail = new(invoice, product, request.Quantity);
            invoiceDetail = await InvoiceDetailRepository.AddAsync(invoiceDetail);

            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
