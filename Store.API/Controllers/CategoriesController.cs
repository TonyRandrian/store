using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Categories;
using Store.Application.UseCases.Categories;
using Store.Domain.Entities;

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
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            Category category = await CreateCategoryUseCase.Execute(request);
            return CreatedAtAction("Category created", category);
        }

        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            return await GetCategoriesUseCase.Execute();
        }
    }
}
