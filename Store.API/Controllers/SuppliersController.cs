using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Suppliers;
using Store.Application.UseCases.Suppliers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SuppliersController(
        GetSupplierUseCase getSupplierUseCase,
        CreateSupplierUseCase createSupplierUseCase,
        GetSuppliersUseCase getSuppliersUseCase,
        DeleteSupplierUseCase deleteSupplierUseCase,
        UpdateSupplierUseCase updateSupplierUseCase) : ControllerBase
    {
        private readonly GetSupplierUseCase GetSupplierUseCase = getSupplierUseCase;
        private readonly CreateSupplierUseCase CreateSupplierUseCase = createSupplierUseCase;
        private readonly GetSuppliersUseCase GetSuppliersUseCase = getSuppliersUseCase;
        private readonly DeleteSupplierUseCase DeleteSupplierUseCase = deleteSupplierUseCase;
        private readonly UpdateSupplierUseCase UpdateSupplierUseCase = updateSupplierUseCase;


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

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            List<SupplierResponse> responses = await GetSuppliersUseCase.Execute();
            return Ok(responses);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await DeleteSupplierUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new { ioe.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateSupplierRequest request)
        {
            try
            {
                SupplierResponse response = await UpdateSupplierUseCase.Execute(id, request);
                return Ok(response);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }
    }
}
