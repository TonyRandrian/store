using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class UpdateInvoiceUseCase(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<InvoiceResponse> Execute(Guid id, UpdateInvoiceRequest request)
        {
            // validation
            Invoice invoice = await InvoiceRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice with the id {id} found");

            Customer? customer = await CustomerRepository.GetByIdAsync(request.CustomerId)
                ?? throw new KeyNotFoundException($"No customer with the id {request.CustomerId} found");

            // update
            invoice.Reference = request.Reference;
            invoice.Customer = customer;
            invoice.Total = request.Total;

            // persistence
            invoice = await InvoiceRepository.UpdateAsync(invoice);
            return new InvoiceResponse(invoice);
        }
    }
}
