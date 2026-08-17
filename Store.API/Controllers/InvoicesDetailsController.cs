using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.UseCases.InvoicesDetails;
using Store.Domain.Entities;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices-details")]
    public class InvoicesDetailsController(
        CreateInvoiceDetailUseCase createInvoiceDetailUseCase,
        GetInvoiceDetailUseCase getInvoiceDetailUseCase,
        GetInvoicesDetailsUseCase getInvoicesDetailsUseCase,
        DeleteInvoiceDetailUseCase deleteInvoiceDetailUseCase,
        UpdateInvoiceDetailUseCase updateInvoiceDetailUseCase) : ControllerBase
    {
        private readonly CreateInvoiceDetailUseCase CreateInvoiceDetailUseCase = createInvoiceDetailUseCase;
        private readonly GetInvoiceDetailUseCase GetInvoiceDetailUseCase = getInvoiceDetailUseCase;
        private readonly GetInvoicesDetailsUseCase GetInvoicesDetailsUseCase = getInvoicesDetailsUseCase;
        private readonly DeleteInvoiceDetailUseCase DeleteInvoiceDetailUseCase = deleteInvoiceDetailUseCase;
        private readonly UpdateInvoiceDetailUseCase UpdateInvoiceDetailUseCase = updateInvoiceDetailUseCase;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> Create(
            [FromBody] CreateInvoiceDetailRequest request)
        {
            try
            {
                InvoiceDetailResponse response = await CreateInvoiceDetailUseCase.Execute(request);
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(201, response, "Created Successfully"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteInvoiceDetailUseCase.Execute(id);
                return Ok(ApiResponse<object>.Ok(204, null, "Invoice detail deleted"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> GetInvoiceDetail([FromRoute] Guid id)
        {
            try
            {
                InvoiceDetailResponse result = await GetInvoiceDetailUseCase.Execute(id);
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(200, result, "Invoice detail retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<InvoiceDetailResponse>>>> GetInvoicesDetails(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<InvoiceDetailResponse> response = await GetInvoicesDetailsUseCase.Execute(pageNum, pageSize);
            return Ok(ApiResponse<PagedResult<InvoiceDetailResponse>>.Ok(200, response, "Invoices details retrieved"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateInvoiceDetailRequest request)
        {
            try
            {
                InvoiceDetailResponse response = await UpdateInvoiceDetailUseCase.Execute(id, request);
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(201, response, "Invoice detail updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<InvoiceDetailResponse>.Error(404, knf.Message));
            }
        }
    }
}
