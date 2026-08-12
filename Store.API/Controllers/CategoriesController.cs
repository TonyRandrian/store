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
        public async Task<Category> Create(CreateCategoryRequest request)
        {
            return await CreateCategoryUseCase.Execute(request);
        }

        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            return await GetCategoriesUseCase.Execute();
        }
    }
}
