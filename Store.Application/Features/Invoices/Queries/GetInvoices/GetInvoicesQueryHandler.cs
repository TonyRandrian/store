using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Invoices;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
        : IRequestHandler<GetInvoicesQuery, PagedResult<InvoiceResponse>>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;


        public async Task<PagedResult<InvoiceResponse>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Invoice> invoices = await _invoiceRepository.GetAllAsync(request.PageNumber, request.PageSize);
            PagedResult<InvoiceResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = invoices.TotalRecords
            };

            foreach (Invoice invoice in invoices.Data)
            {
                result.Data.Add(new InvoiceResponse(invoice));
            }

            return result;
        }
    }
}
