using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Files.Images;
using Store.Application.DTOs.Products;
using Store.Application.Features.Products.Commands.AddProductDocument;
using Store.Application.Features.Products.Commands.AddProductImage;
using Store.Application.Features.Products.Commands.CreateProduct;
using Store.Application.Features.Products.Commands.DeleteProduct;
using Store.Application.Features.Products.Commands.DeleteProductImage;
using Store.Application.Features.Products.Commands.RemoveProductDocument;
using Store.Application.Features.Products.Commands.UpdateProduct;
using Store.Application.Features.Products.Commands.UpdateProductImage;
using Store.Application.Features.Products.Queries.GetProduct;
using Store.Application.Features.Products.Queries.GetProductCategory;
using Store.Application.Features.Products.Queries.GetProducts;

namespace Store.API.Controllers
{
    public class ProductFileRequest
    {
        public IFormFile File { get; set; }
    }

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
            ProductResponse response = await _mediator.Send(new CreateProductCommand(
                request.Name, request.Price, request.CategoryId, request.SuppliersIds));

            return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product created"));
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
            ProductResponse response = await _mediator.Send(new GetProductQuery(id));

            return Ok(ApiResponse<ProductResponse>.Ok(200, response, "Product retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));

            return Ok(ApiResponse<object>.Ok(204, null, "Product deleted"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> Update([FromRoute] Guid id,
            [FromBody] UpdateProductRequest request)
        {
            ProductResponse response = await _mediator.Send(new UpdateProductCommand(
                id, request.Name, request.Price, request.CategoryId, request.SuppliersIds));

            return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product updated"));
        }

        [HttpGet("{id:Guid}/category")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetProductCategory([FromRoute] Guid id)
        {
            CategoryResponse response = await _mediator.Send(new GetProductCategoryQuery(id));

            return Ok(ApiResponse<CategoryResponse>.Ok(200, response, "Category retrieved"));
        }

        [HttpPost("{productId:Guid}/images")]
        public async Task<ActionResult<ApiResponse<ImageResponse>>> AddImage(
            [FromRoute] Guid productId,
            [FromForm] List<IFormFile> files)
        {
            List<CreateProductFile> uploads = [.. files.Select(file =>
                new CreateProductFile(
                    file.OpenReadStream(),
                    file.FileName,
                    file.ContentType,
                    file.Length
                    )
                )];

            ProductResponse response = await _mediator.Send(new AddProductImageCommand(productId, uploads));

            return Ok(ApiResponse<ProductResponse>.Ok(200, response, "Images added"));
        }

        [HttpDelete("{productId:Guid}/images/{imageId:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteImage(
            [FromRoute] Guid productId,
            [FromRoute] Guid imageId)
        {
            await _mediator.Send(new DeleteProductImageCommand(productId, imageId));

            return Ok(ApiResponse<object>.Ok(204, null, "Product's image deleted"));
        }

        [HttpPatch("{productId:Guid}/images/{imageId:Guid}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> UpdateImage(
            [FromRoute] Guid productId,
            [FromRoute] Guid imageId,
            [FromForm] ProductFileRequest formFile)
        {
            CreateProductFile file = new(
                formFile.File.OpenReadStream(),
                formFile.File.FileName,
                formFile.File.ContentType,
                formFile.File.Length);

            ProductResponse response = await _mediator.Send(new UpdateProductImageCommand(
                productId, imageId, file));

            return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product's image updated"));
        }

        [HttpPost("{productId:Guid}/document")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> CreateDocument(
            [FromRoute] Guid productId,
            [FromForm] ProductFileRequest formFile)
        {
            CreateProductFile file = new(
                formFile.File.OpenReadStream(),
                formFile.File.FileName,
                formFile.File.ContentType,
                formFile.File.Length);

            ProductResponse response = await _mediator.Send(new AddProductDocumentCommand(
                productId, file));

            return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product's document created"));
        }

        [HttpDelete("{productId:Guid}/document")]
        public async Task<ActionResult<ApiResponse<object>>> RemoveDocument([FromRoute] Guid productId)
        {
            await _mediator.Send(new RemoveProductDocumentCommand(productId));

            return Ok(ApiResponse<object>.Ok(201, null, "Product's document removed"));
        }
    }
}
