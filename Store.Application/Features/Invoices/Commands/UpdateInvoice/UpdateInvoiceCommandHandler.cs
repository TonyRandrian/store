using MediatR;
using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository)
        : IRequestHandler<UpdateInvoiceCommand, InvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task<InvoiceResponse> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetByIdAsync(request.Id);
            Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            // update
            invoice!.Reference = request.Reference;
            invoice.Customer = customer!;
            invoice.Total = request.Total;

            // persistence
            invoice = await _invoiceRepository.UpdateAsync(invoice);
            return new InvoiceResponse(invoice);
        }
    }
}
