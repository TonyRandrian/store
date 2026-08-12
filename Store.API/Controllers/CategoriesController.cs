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
        GetCategoriesUseCase getCategoriesUseCase,
        GetCategoryUseCase getCategoryUseCase,
        DeleteCategoryUseCase deleteCategoryUseCase,
        UpdateCategoryUseCase updateCategoryUseCase) : ControllerBase
    {
        private readonly CreateCategoryUseCase CreateCategoryUseCase = createCategoryUseCase;
        private readonly GetCategoriesUseCase GetCategoriesUseCase = getCategoriesUseCase;
        private readonly GetCategoryUseCase GetCategoryUseCase = getCategoryUseCase;
        private readonly DeleteCategoryUseCase DeleteCategoryUseCase = deleteCategoryUseCase;
        private readonly UpdateCategoryUseCase UpdateCategoryUseCase = updateCategoryUseCase;


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

        [HttpGet("{id:int}")]
        public async Task<CategoryResponse?> GetCategory([FromRoute] int id)
        {
            return await GetCategoryUseCase.Execute(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await DeleteCategoryUseCase.Execute(id);
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateCategoryRequest request)
        {
            Category category = await UpdateCategoryUseCase.Execute(id, request);
            CategoryResponse response = new(category);

            return Ok(response);
        }
    }
}
