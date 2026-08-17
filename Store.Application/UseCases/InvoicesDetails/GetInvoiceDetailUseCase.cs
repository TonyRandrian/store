using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.InvoicesDetails
{
    public class GetInvoiceDetailUseCase(IInvoiceDetailsRepository invoiceDetailsRepository)
    {
        private readonly IInvoiceDetailsRepository InvoiceDetailsRepository = invoiceDetailsRepository;


        public async Task<InvoiceDetailResponse> Execute(Guid id)
        {
            InvoiceDetail? invoiceDetail = await InvoiceDetailsRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {id} found");

            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
