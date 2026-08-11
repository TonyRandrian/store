using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class GetInvoiceUseCase(IInvoiceRepository invoiceRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;


        public async Task<Invoice?> Execute(int id)
        {
            return await InvoiceRepository.GetByIdAsync(id);
        }
    }
}
