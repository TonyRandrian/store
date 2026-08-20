using MediatR;
using Store.Application.DTOs.Invoices;

namespace Store.Application.Features.Invoices.Queries.GetInvoice
{
    public record GetInvoiceQuery(Guid Id) : IRequest<InvoiceResponse>;
}
