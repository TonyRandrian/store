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
        GetProductUseCase getProductUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase CreateProductUseCase = createProductUseCase;
        private readonly GetProductsUseCase GetProductsUseCase = getProductsUseCase;
        private readonly GetProductUseCase GetProductUseCase = getProductUseCase;

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
    }
}
