using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Invoices
{
    public class CreateInvoiceUseCase(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository)
    {
        private readonly IInvoiceRepository InvoiceRepository = invoiceRepository;
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<InvoiceResponse> Execute(CreateInvoiceRequest request)
        {
            Customer? customer = await CustomerRepository.GetByIdAsync(request.CustomerId)
                ?? throw new KeyNotFoundException($"No customer with the id {request.CustomerId} found");

            Invoice invoice = new(request.Reference, customer, request.Total);

            invoice = await InvoiceRepository.AddAsync(invoice);
            return new InvoiceResponse(invoice);
        }
    }
}
