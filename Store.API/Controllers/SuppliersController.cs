using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
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
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> Create(CreateSupplierRequest request)
        {
            try
            {
                SupplierResponse response = await CreateSupplierUseCase.Execute(request);

                return Ok(ApiResponse<SupplierResponse>.Ok(201, response, "Supplier created"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> GetSupplier([FromRoute] Guid id)
        {
            try
            {
                SupplierResponse response = await GetSupplierUseCase.Execute(id);
                return Ok(ApiResponse<SupplierResponse>.Ok(200, response, "Supplier retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<SupplierResponse>>>> GetSuppliers(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<SupplierResponse> responses = await GetSuppliersUseCase.Execute(pageNum, pageSize);
            return Ok(ApiResponse<PagedResult<SupplierResponse>>.Ok(200, responses, "Suppliers retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteSupplierUseCase.Execute(id);
                return Ok(ApiResponse<object>.Ok(204, null, "Supplier deleted"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ApiResponse<object>.Error(400, ioe.Message));
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateSupplierRequest request)
        {
            try
            {
                SupplierResponse response = await UpdateSupplierUseCase.Execute(id, request);
                return Ok(ApiResponse<SupplierResponse>.Ok(201, response, "Supplier updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }
    }
}
