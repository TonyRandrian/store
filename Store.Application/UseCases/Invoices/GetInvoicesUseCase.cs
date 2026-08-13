using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class GetInvoicesUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task<List<InvoiceResponse>> Execute()
        {
            List<Invoice> invoices = await InvoiceRepository.GetAllAsync();
            List<InvoiceResponse> responses = [];

            foreach (Invoice invoice in invoices)
            {
                responses.Add(new InvoiceResponse(invoice));
            }

            return responses;
        }
    }
}
