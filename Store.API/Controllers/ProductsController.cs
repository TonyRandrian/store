using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.DTOs.Products;
using Store.Application.UseCases.Products;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(
        CreateProductUseCase createProductUseCase,
        GetProductsUseCase getProductsUseCase,
        GetProductUseCase getProductUseCase,
        DeleteProductUseCase deleteProductUseCase,
        UpdateProductUseCase updateProductUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase CreateProductUseCase = createProductUseCase;
        private readonly GetProductsUseCase GetProductsUseCase = getProductsUseCase;
        private readonly GetProductUseCase GetProductUseCase = getProductUseCase;
        private readonly DeleteProductUseCase DeleteProductUseCase = deleteProductUseCase;
        private readonly UpdateProductUseCase UpdateProductUseCase = updateProductUseCase;

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> Create(CreateProductRequest request)
        {
            try
            {
                ProductResponse response = await CreateProductUseCase.Execute(request);
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
            PagedResult<ProductResponse> responses = await GetProductsUseCase.Execute(pageNum, pageSize);

            return Ok(ApiResponse<PagedResult<ProductResponse>>.Ok(200, responses, "Products retrieved"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProduct([FromRoute] Guid id)
        {
            ProductResponse? response = await GetProductUseCase.Execute(id);

            if (response != null)
            {
                return Ok(ApiResponse<ProductResponse>.Ok(200, response, "Product retrieved"));
            }

            return NotFound(ApiResponse<object>.Error(404, $"No product with the id {id} found"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteProductUseCase.Execute(id);
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
                ProductResponse response = await UpdateProductUseCase.Excecute(id, request);
                return Ok(ApiResponse<ProductResponse>.Ok(201, response, "Product updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}/category")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetProductCategory([FromRoute] Guid id)
        {
            ProductResponse? response = await GetProductUseCase.Execute(id);

            if (response == null)
            {
                return NotFound(ApiResponse<object>.Error(404, $"No product with the id {id} found"));
            }

            return Ok(ApiResponse<CategoryResponse>.Ok(200, response.Category, "Product retrieved"));
        }
    }
}
