using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Categories;
using Store.Application.UseCases.Categories;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(
        CreateCategoryUseCase createCategoryUseCase, 
        GetCategoriesUseCase getCategoriesUseCase) : ControllerBase
    {
        private readonly CreateCategoryUseCase CreateCategoryUseCase = createCategoryUseCase;
        private readonly GetCategoriesUseCase GetCategoriesUseCase = getCategoriesUseCase;

        [HttpPost]
        public async Task<CategoryResponse> Create(CreateCategoryRequest request)
        {
            return await CreateCategoryUseCase.Execute(request);
        }

        [HttpGet]
        public async Task<List<CategoryResponse>> GetCategories()
        {
            return await GetCategoriesUseCase.Execute();
        }
    }
}
