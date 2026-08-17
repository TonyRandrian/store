using Store.Application.Commons;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.InvoicesDetails
{
    public class GetInvoicesDetailsUseCase(IInvoiceDetailsRepository invoiceDetailsRepository)
    {
        private readonly IInvoiceDetailsRepository InvoiceDetailsRepository = invoiceDetailsRepository;


        public async Task<PagedResult<InvoiceDetailResponse>> Execute(int pageNum, int pageSize)
        {
            PagedResult<InvoiceDetail> ids = await InvoiceDetailsRepository.GetAllAsync(pageNum, pageSize);
            PagedResult<InvoiceDetailResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
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
