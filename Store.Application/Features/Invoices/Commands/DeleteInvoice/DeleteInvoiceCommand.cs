using MediatR;

namespace Store.Application.Features.Invoices.Commands.DeleteInvoice
{
    public record DeleteInvoiceCommand(Guid Id) : IRequest;
}
