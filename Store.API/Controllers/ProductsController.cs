using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Files.Images;
using Store.Application.DTOs.Products;
using Store.Application.Features.Products.Commands.AddProductImage;
using Store.Application.Features.Products.Commands.CreateProduct;
using Store.Application.Features.Products.Commands.DeleteProduct;
using Store.Application.Features.Products.Commands.UpdateProduct;
using Store.Application.Features.Products.Queries.GetProduct;
using Store.Application.Features.Products.Queries.GetProductCategory;
using Store.Application.Features.Products.Queries.GetProducts;

namespace Store.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> Create(CreateProductRequest request)
        {
            try
            {
                ProductResponse response = await _mediator.Send(new CreateProductCommand(
                    request.Name, request.Price, request.CategoryId, request.SuppliersIds));
                return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product created"));
            }
            catch (KeyNotFoundException knf)
            {
                return BadRequest(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductResponse>>>> GetProducts(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<ProductResponse> responses = await _mediator.Send(new GetProductsQuery(pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<ProductResponse>>.Ok(200, responses, "Products retrieved"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProduct([FromRoute] Guid id)
        {
            try
            {
                ProductResponse response = await _mediator.Send(new GetProductQuery(id));
                return Ok(ApiResponse<ProductResponse>.Ok(200, response, "Product retrieved"));
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
                await _mediator.Send(new DeleteProductCommand(id));
                return Ok(ApiResponse<object>.Ok(204, null, "Product deleted"));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ApiResponse<object>.Error(400, ioe.Message));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> Update([FromRoute] Guid id,
            [FromBody] UpdateProductRequest request)
        {
            try
            {
                ProductResponse response = await _mediator.Send(new UpdateProductCommand(
                    id, request.Name, request.Price, request.CategoryId, request.SuppliersIds));
                return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}/category")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetProductCategory([FromRoute] Guid id)
        {
            try
            {
                CategoryResponse response = await _mediator.Send(new GetProductCategoryQuery(id));
                return Ok(ApiResponse<CategoryResponse>.Ok(200, response, "Category retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpPost("{productId:Guid}/images")]
        public async Task<ActionResult<ApiResponse<ImageResponse>>> AddImage(
            Guid productId,
            [FromForm] List<IFormFile> files)
        {
            try
            {
                List<FileUpload> uploads = [.. files.Select(file =>
                new FileUpload(
                    file.OpenReadStream(),
                    file.FileName,
                    file.ContentType,
                    file.Length
                    )
                )];

                ProductResponse response = await _mediator.Send(new AddProductImageCommand(productId, uploads));
                return Ok(ApiResponse<ProductResponse>.Ok(200, response, "Images added"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ApiResponse<object>.Error(400, ae.Message));
            }
        }
    }
}
