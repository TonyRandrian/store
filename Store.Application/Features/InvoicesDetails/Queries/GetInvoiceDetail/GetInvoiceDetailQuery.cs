using MediatR;
using Store.Application.DTOs.InvoicesDetails;

namespace Store.Application.Features.InvoicesDetails.Queries.GetInvoiceDetail
{
    public record GetInvoiceDetailQuery(Guid Id) : IRequest<InvoiceDetailResponse>;
}
