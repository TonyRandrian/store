using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.DTOs.Invoices;
using Store.Application.UseCases.Invoices;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController(
        GetInvoiceUseCase getInvoiceUseCase,
        CreateInvoiceUseCase createInvoiceUseCase,
        GetInvoicesUseCase getInvoicesUseCase,
        DeleteInvoiceUseCase deleteInvoiceUseCase,
        UpdateInvoiceUseCase updateInvoiceUseCase) : ControllerBase
    {
        private readonly GetInvoiceUseCase GetInvoiceUseCase = getInvoiceUseCase;
        private readonly CreateInvoiceUseCase CreateInvoiceUseCase = createInvoiceUseCase;
        private readonly GetInvoicesUseCase GetInvoicesUseCase = getInvoicesUseCase;
        private readonly DeleteInvoiceUseCase DeleteInvoiceUseCase = deleteInvoiceUseCase;
        private readonly UpdateInvoiceUseCase UpdateInvoiceUseCase = updateInvoiceUseCase;


        [HttpPost]
        public async Task<ActionResult<InvoiceResponse>> Create([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                InvoiceResponse response = await CreateInvoiceUseCase.Execute(request);
                return Ok(ApiResponse<InvoiceResponse>.Ok(201, response, "Invoice created"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceResponse>>> GetInvoice([FromRoute] Guid id)
        {
            try
            {
                InvoiceResponse response = await GetInvoiceUseCase.Execute(id);
                return Ok(ApiResponse<InvoiceResponse>.Ok(200, response, "Invoice retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InvoiceResponse>>>> GetInvoices()
        {
            List<InvoiceResponse> responses = await GetInvoicesUseCase.Execute();
            return Ok(ApiResponse<List<InvoiceResponse>>.Ok(200, responses, "Invoices retrieved"));
        }
        
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteInvoiceUseCase.Execute(id);
                return Ok(ApiResponse<object>.Ok(204, null, "Invoice deleted"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateInvoiceRequest request)
        {
            try
            {
                InvoiceResponse response = await UpdateInvoiceUseCase.Execute(id, request);
                return Ok(ApiResponse<InvoiceResponse>.Ok(201, response, "Invoice udpdated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }
    }
}
