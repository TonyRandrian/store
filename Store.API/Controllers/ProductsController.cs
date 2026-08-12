using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Products;
using Store.Application.UseCases.Products;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(CreateProductUseCase createProductUseCase) : ControllerBase
    {
        private readonly CreateProductUseCase CreateProductUseCase = createProductUseCase;

        [HttpPost]
        public async Task<ProductResponse> Create(CreateProductRequest request)
        {
            return await CreateProductUseCase.Execute(request);
        }
    }
}
