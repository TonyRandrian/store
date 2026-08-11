using Store.Application.Interfaces;

namespace Store.Application.UseCases.Invoices
{
    public class DeleteInvoiceUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task Execute(int id)
        {
            await InvoiceRepository.DeleteAsync(id);
        }
    }
}
