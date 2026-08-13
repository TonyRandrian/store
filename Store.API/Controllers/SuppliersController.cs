using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Suppliers;
using Store.Application.UseCases.Suppliers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController(
        GetSupplierUseCase getSupplierUseCase) : ControllerBase
    {
        private readonly GetSupplierUseCase GetSupplierUseCase = getSupplierUseCase;


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSupplier([FromRoute] int id)
        {
            try
            {
                SupplierResponse response = await GetSupplierUseCase.Execute(id);
                return Ok(response);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }
    }
}
