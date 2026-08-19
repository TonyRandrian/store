using MediatR;
using Store.Application.DTOs.Invoices;

namespace Store.Application.Features.Invoices.Commands.UpdateInvoice
{
    public record UpdateInvoiceCommand(
        Guid Id,
        string Reference,
        decimal Total,
        Guid CustomerId) : IRequest<InvoiceResponse>;
}
