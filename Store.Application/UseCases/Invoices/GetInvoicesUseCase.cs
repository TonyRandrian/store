using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class GetInvoicesUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task<List<Invoice>> Execute()
        {
            return await InvoiceRepository.GetAllAsync();
        }
    }
}
