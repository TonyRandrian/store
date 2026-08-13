using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Invoices;
using Store.Application.UseCases.Invoices;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController(
        GetInvoiceUseCase getInvoiceUseCase,
        CreateInvoiceUseCase createInvoiceUseCase) : ControllerBase
    {
        private readonly GetInvoiceUseCase GetInvoiceUseCase = getInvoiceUseCase;
        private readonly CreateInvoiceUseCase CreateInvoiceUseCase = createInvoiceUseCase;


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
    }
}
