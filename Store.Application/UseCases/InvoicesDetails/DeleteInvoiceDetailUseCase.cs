using Store.Application.Interfaces;

namespace Store.Application.UseCases.InvoicesDetails
{
    public class DeleteInvoiceDetailUseCase(IInvoiceDetailsRepository invoiceDetailsRepository)
    {
        private readonly IInvoiceDetailsRepository InvoiceDetailsRepository = invoiceDetailsRepository;


        public async Task Execute(Guid id)
        {
            await InvoiceDetailsRepository.DeleteAsync(id);
        }
    }
}
