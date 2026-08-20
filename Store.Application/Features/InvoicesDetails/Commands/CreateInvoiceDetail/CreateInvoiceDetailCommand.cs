using MediatR;
using Store.Application.DTOs.InvoicesDetails;

namespace Store.Application.Features.InvoicesDetails.Commands.CreateInvoiceDetail
{
    public record CreateInvoiceDetailCommand(Guid InvoiceId, Guid ProductId, double Quantity)
        : IRequest<InvoiceDetailResponse>;
}
