using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Invoices;
using Store.Application.UseCases.Invoices;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController(
        GetInvoiceUseCase getInvoiceUseCase) : ControllerBase
    {
        private readonly GetInvoiceUseCase GetInvoiceUseCase = getInvoiceUseCase;

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
