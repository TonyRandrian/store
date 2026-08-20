using MediatR;
using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository)
        : IRequestHandler<CreateInvoiceCommand, InvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<InvoiceResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId)
                ?? throw new KeyNotFoundException($"No customer with the id {request.CustomerId} found");

            Invoice invoice = new(request.Reference, customer, request.Total);

            invoice = await _invoiceRepository.AddAsync(invoice);
            return new InvoiceResponse(invoice);
        }
    }
}
