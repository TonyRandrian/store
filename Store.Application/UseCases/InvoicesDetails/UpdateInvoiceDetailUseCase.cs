using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.InvoicesDetails
{
    public class UpdateInvoiceDetailUseCase(
        IInvoiceDetailsRepository invoiceDetailsRepository,
        IProductRepository productRepository)
    {
        private readonly IInvoiceDetailsRepository InvoiceDetailsRepository = invoiceDetailsRepository;
        private readonly IProductRepository ProductRepository = productRepository;


        public async Task<InvoiceDetailResponse> Execute(Guid id, UpdateInvoiceDetailRequest request)
        {
            InvoiceDetail? invoiceDetail = await InvoiceDetailsRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {id} found");

            Product? product = await ProductRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            invoiceDetail.Product = product;
            invoiceDetail.Quantity = request.Quantity;

            invoiceDetail = await InvoiceDetailsRepository.UpdateAsync(invoiceDetail);
            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
