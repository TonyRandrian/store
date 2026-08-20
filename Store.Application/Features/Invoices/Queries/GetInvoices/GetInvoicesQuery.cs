using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Invoices;

namespace Store.Application.Features.Invoices.Queries.GetInvoices
{
    public record GetInvoicesQuery(int PageNumber, int PageSize) : IRequest<PagedResult<InvoiceResponse>>;
}
