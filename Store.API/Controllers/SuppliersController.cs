using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Suppliers;
using Store.Application.UseCases.Suppliers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController(
        GetSupplierUseCase getSupplierUseCase,
        CreateSupplierUseCase createSupplierUseCase) : ControllerBase
    {
        private readonly GetSupplierUseCase GetSupplierUseCase = getSupplierUseCase;
        private readonly CreateSupplierUseCase CreateSupplierUseCase = createSupplierUseCase;


        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierRequest request)
        {
            try
            {
                SupplierResponse response = await CreateSupplierUseCase.Execute(request);

                return CreatedAtAction(
                    nameof(GetSupplier),
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
