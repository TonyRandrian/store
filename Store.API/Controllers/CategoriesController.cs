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
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            try
            {
                CategoryResponse response = await CreateCategoryUseCase.Execute(request);
                return CreatedAtAction(
                    nameof(GetCategory),
                    new { id = response.Id },
                    response
                    );
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<CategoryResponse> responses = await GetCategoriesUseCase.Execute();

            return Ok(responses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            CategoryResponse? response = await GetCategoryUseCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(new { Message = $"No category with the id {id} found" });
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
            CategoryResponse response = await UpdateCategoryUseCase.Execute(id, request);

            return Ok(response);
        }
    }
}
