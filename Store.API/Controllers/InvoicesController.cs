using Microsoft.AspNetCore.Mvc;
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
        DeleteInvoiceUseCase deleteInvoiceUseCase) : ControllerBase
    {
        private readonly GetInvoiceUseCase GetInvoiceUseCase = getInvoiceUseCase;
        private readonly CreateInvoiceUseCase CreateInvoiceUseCase = createInvoiceUseCase;
        private readonly GetInvoicesUseCase GetInvoicesUseCase = getInvoicesUseCase;
        private readonly DeleteInvoiceUseCase DeleteInvoiceUseCase = deleteInvoiceUseCase;


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                InvoiceResponse response = await CreateInvoiceUseCase.Execute(request);
                return CreatedAtAction(
                    nameof(GetInvoice),
                    new { id = response.Id },
                    response
                    );
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoice([FromRoute] int id)
        {
            try
            {
                InvoiceResponse response = await GetInvoiceUseCase.Execute(id);
                return Ok(response);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            List<InvoiceResponse> responses = await GetInvoicesUseCase.Execute();
            return Ok(responses);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await DeleteInvoiceUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }
    }
}
