using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Products;
using Store.Application.UseCases.Products;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(
        CreateProductUseCase createProductUseCase,
        GetProductsUseCase getProductsUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase CreateProductUseCase = createProductUseCase;
        private readonly GetProductsUseCase GetProductsUseCase = getProductsUseCase;

        /*[HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            try
            {
                ProductResponse response = await CreateProductUseCase.Execute(request);
                return CreatedAtAction(
                    nameof(GetById)
                    );
            }
        }*/

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<ProductResponse> response = await GetProductsUseCase.Execute();

            return Ok(response);
        }
    }
}
