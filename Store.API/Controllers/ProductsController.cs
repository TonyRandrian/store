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
        DeleteProductUseCase deleteProductUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase CreateProductUseCase = createProductUseCase;
        private readonly GetProductsUseCase GetProductsUseCase = getProductsUseCase;
        private readonly GetProductUseCase GetProductUseCase = getProductUseCase;
        private readonly DeleteProductUseCase DeleteProductUseCase = deleteProductUseCase;

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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            ProductResponse? response = await GetProductUseCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(new { Message = $"No product with the id {id} found" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
    }
}
