using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.InvoicesDetails;

namespace Store.Application.Features.InvoicesDetails.Queries.GetInvoicesDetails
{
    public record GetInvoicesDetailsQuery(int PageNumber, int PageSize) : IRequest<PagedResult<InvoiceDetailResponse>>;
}
