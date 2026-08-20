using MediatR;
using Store.Application.DTOs.InvoicesDetails;

namespace Store.Application.Features.InvoicesDetails.Commands.UpdateInvoiceDetail
{
    public record UpdateInvoiceDetailCommand(Guid Id, Guid ProductId, double Quantity)
        : IRequest<InvoiceDetailResponse>;
}
