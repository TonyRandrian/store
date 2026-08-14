using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {
                ProductResponse response = await CreateProductUseCase.Execute(request);
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = response.Id },
                    response);
            }
            catch (KeyNotFoundException knf)
            {
                return BadRequest(new { knf.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<ProductResponse> response = await GetProductsUseCase.Execute();

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            ProductResponse? response = await GetProductUseCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(new { Message = $"No product with the id {id} found" });
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteProductUseCase.Execute(id);
                return NoContent();
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new { ioe.Message });
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateProductRequest request)
        {
            try
            {
                ProductResponse response = await UpdateProductUseCase.Excecute(id, request);
                return Ok(response);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }
    }
}
