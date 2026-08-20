using MediatR;
using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Invoices.Queries.GetInvoice
{
    public class GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
        : IRequestHandler<GetInvoiceQuery, InvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;


        public async Task<InvoiceResponse> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No invoice with the id {request.Id} found");

            return new InvoiceResponse(invoice);
        }
    }
}
