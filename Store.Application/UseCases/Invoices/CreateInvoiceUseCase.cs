using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class CreateInvoiceUseCase(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<Invoice> Execute(string reference, int customerId, decimal total)
        {
            Customer? customer = await CustomerRepository.GetByIdAsync(customerId)
                ?? throw new Exception($"No customer with the id {customerId} found");

            Invoice invoice = new(reference, customer, total);

            return await InvoiceRepository.AddAsync(invoice);
        }
    }
}
