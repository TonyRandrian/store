using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class GetInvoiceUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task<InvoiceResponse> Execute(Guid id)
        {
            Invoice? invoice = await InvoiceRepository.GetByIdAsync(id) 
                ?? throw new KeyNotFoundException($"No invoice with the id {id} found");

            return new InvoiceResponse(invoice);
        }
    }
}
