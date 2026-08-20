using MediatR;
using Store.Application.DTOs.Invoices;

namespace Store.Application.Features.Invoices.Commands.CreateInvoice
{
    public record CreateInvoiceCommand(string Reference, decimal Total, Guid CustomerId)
        : IRequest<InvoiceResponse>;
}
