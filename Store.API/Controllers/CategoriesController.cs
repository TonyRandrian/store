using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.UseCases.Categories;

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
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> Create(CreateCategoryRequest request)
        {
            try
            {
                CategoryResponse response = await CreateCategoryUseCase.Execute(request);
                return Ok(ApiResponse<CategoryResponse>.Ok(201, response, "Category created successfully"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<CategoryResponse>>>> GetCategories(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<CategoryResponse> responses = await GetCategoriesUseCase.Execute(pageNum, pageSize);

            return Ok(ApiResponse<PagedResult<CategoryResponse>>.Ok(200, responses, "Categories retrieved"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetCategory([FromRoute] Guid id)
        {
            CategoryResponse? response = await GetCategoryUseCase.Execute(id);

            if (response != null)
            {
                return Ok(ApiResponse<CategoryResponse>.Ok(200, response, "Category retrieved"));
            }

            return NotFound(ApiResponse<object>.Error(404, $"No category with the id {id} found"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteCategoryUseCase.Execute(id);
                return Ok(ApiResponse<object>.Ok(204, null, "Category deleted"));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ApiResponse<object>.Error(400, ioe.Message));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCategoryRequest request)
        {
            try
            {
                CategoryResponse response = await UpdateCategoryUseCase.Execute(id, request);
                return Ok(ApiResponse<CategoryResponse>.Ok(201, response, "Category updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<CategoryResponse>.Error(404, knf.Message));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ApiResponse<CategoryResponse>.Error(400, ioe.Message));
            }
        }
    }
}
