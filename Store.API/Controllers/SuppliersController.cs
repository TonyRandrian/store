using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.DTOs.Suppliers;
using Store.Application.Features.Suppliers.Commands.CreateSupplier;
using Store.Application.Features.Suppliers.Commands.DeleteSupplier;
using Store.Application.Features.Suppliers.Commands.UpdateSupplier;
using Store.Application.Features.Suppliers.Queries.GetSupplier;
using Store.Application.Features.Suppliers.Queries.GetSupplierProducts;
using Store.Application.Features.Suppliers.Queries.GetSuppliers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class SuppliersController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> Create(CreateSupplierRequest request)
        {

            SupplierResponse response = await _mediator.Send(new CreateSupplierCommand(
                request.Name, request.ProductsIds));

            return Ok(ApiResponse<SupplierResponse>.Ok(201, response, "Supplier created"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> GetSupplier([FromRoute] Guid id)
        {

            SupplierResponse response = await _mediator.Send(new GetSupplierQuery(id));

            return Ok(ApiResponse<SupplierResponse>.Ok(200, response, "Supplier retrieved"));

        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<SupplierResponse>>>> GetSuppliers(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<SupplierResponse> responses = await _mediator.Send(new GetSuppliersQuery(
                pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<SupplierResponse>>.Ok(200, responses, "Suppliers retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteSupplierCommand(id));

            return Ok(ApiResponse<object>.Ok(204, null, "Supplier deleted"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<SupplierResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateSupplierRequest request)
        {
            SupplierResponse response = await _mediator.Send(new UpdateSupplierCommand(
                id, request.Name, request.ProductsIds));

            return Ok(ApiResponse<SupplierResponse>.Ok(201, response, "Supplier updated"));
        }

        [HttpGet("{supplierId:Guid}/products")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductResponse>>>> GetSupplierProducts(
            [FromRoute] Guid supplierId,
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<ProductResponse> response = await _mediator.Send(new GetSupplierProductsQuery(
                supplierId, pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<ProductResponse>>.Ok(200, response, "Products retrieved"));
        }
    }
}
