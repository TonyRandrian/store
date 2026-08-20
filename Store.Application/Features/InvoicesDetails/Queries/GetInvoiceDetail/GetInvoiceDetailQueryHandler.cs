using MediatR;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.InvoicesDetails.Queries.GetInvoiceDetail
{
    public class GetInvoiceDetailQueryHandler(IInvoiceDetailsRepository invoiceDetailsRepository)
        : IRequestHandler<GetInvoiceDetailQuery, InvoiceDetailResponse>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository = invoiceDetailsRepository;


        public async Task<InvoiceDetailResponse> Handle(GetInvoiceDetailQuery request, CancellationToken cancellationToken)
        {
            InvoiceDetail? invoiceDetail = await _invoiceDetailsRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {request.Id} found");

            return new InvoiceDetailResponse(invoiceDetail);
        }
    }
}
