using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class UpdateInvoiceUseCase(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<Invoice> Execute(int id, string reference, int customerId, decimal total)
        {
            // validation
            Invoice invoice = await InvoiceRepository.GetByIdAsync(id)
                ?? throw new Exception($"No invoice with the id {id} found");

            Customer? customer = await CustomerRepository.GetByIdAsync(customerId)
                ?? throw new Exception($"No customer with the id {customerId} found");

            // update
            invoice.Reference = reference;
            invoice.Customer = customer;
            invoice.Total = total;

            // persistence
            return await InvoiceRepository.UpdateAsync(invoice);
        }
    }
}
