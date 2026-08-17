using Store.Application.Commons;
using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class GetInvoicesUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task<PagedResult<InvoiceResponse>> Execute(int pageNum, int pageSize)
        {
            PagedResult<Invoice> invoices = await InvoiceRepository.GetAllAsync(pageNum, pageSize);
            PagedResult<InvoiceResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = invoices.TotalRecords
            };

            foreach (Invoice invoice in invoices.Data)
            {
                result.Data.Add(new InvoiceResponse(invoice));
            }

            return result;
        }
    }
}
