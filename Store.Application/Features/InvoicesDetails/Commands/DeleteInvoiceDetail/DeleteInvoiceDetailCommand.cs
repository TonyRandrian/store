using MediatR;

namespace Store.Application.Features.InvoicesDetails.Commands.DeleteInvoiceDetail
{
    public record DeleteInvoiceDetailCommand(Guid Id) : IRequest;
}
