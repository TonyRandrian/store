using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.InvoicesDetails.Queries.GetInvoicesDetails
{
    public class GetInvoicesDetailsQueryHandler(IInvoiceDetailsRepository invoiceDetailsRepository)
        : IRequestHandler<GetInvoicesDetailsQuery, PagedResult<InvoiceDetailResponse>>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository = invoiceDetailsRepository;


        public async Task<PagedResult<InvoiceDetailResponse>> Handle(GetInvoicesDetailsQuery request, CancellationToken cancellationToken)
        {
            PagedResult<InvoiceDetail> ids = await _invoiceDetailsRepository.GetAllAsync(
                request.PageNumber, request.PageSize);
            PagedResult<InvoiceDetailResponse> result = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = ids.TotalRecords
            };

            foreach (InvoiceDetail id in ids.Data)
            {
                result.Data.Add(new InvoiceDetailResponse(id));
            }

            return result;
        }
    }
}
