using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Categories;
using Store.Application.DTOs.Products;
using Store.Application.Features.Categories.Commands.CreateCategory;
using Store.Application.Features.Categories.Commands.DeleteCategory;
using Store.Application.Features.Categories.Commands.UpdateCategory;
using Store.Application.Features.Categories.Queries.GetCategories;
using Store.Application.Features.Categories.Queries.GetCategory;
using Store.Application.Features.Categories.Queries.GetCategoryChildren;
using Store.Application.Features.Categories.Queries.GetCategoryProducts;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/categories")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CategoriesController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> Create(CreateCategoryRequest request)
        {
            try
            {
                CategoryResponse response = await _mediator.Send(
                    new CreateCategoryCommand(request.Name, request.ParentCategoryId));

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
            PagedResult<CategoryResponse> responses = await _mediator.Send(new GetCategoriesQuery(pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<CategoryResponse>>.Ok(200, responses, "Categories retrieved"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetCategory([FromRoute] Guid id)
        {
            CategoryResponse? response = await _mediator.Send(new GetCategoryQuery(id));

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
                await _mediator.Send(new DeleteCategoryCommand(id));
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
                CategoryResponse response = await _mediator.Send(new UpdateCategoryCommand(
                    id, request.Name, request.ParentCategoryId));
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

        [HttpGet("{productId:Guid}/products")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductResponse>>>> GetProducts(
            [FromRoute] Guid categoryId,
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<ProductResponse> responses = await _mediator.Send(new GetCategoryProductsQuery(
                categoryId, pageNum, pageSize));
            return Ok(ApiResponse<PagedResult<ProductResponse>>.Ok(200, responses, "Products retrieved"));
        }

        [HttpGet("{categoryId:Guid}/children")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ApiResponse<PagedResult<CategoryResponse>>>> GetCategoryChildren(
            [FromRoute] Guid categoryId,
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            try
            {
                PagedResult<CategoryResponse> responses = await _mediator.Send(new GetCategoryChildrenQuery(
                    categoryId, pageNum, pageSize));
                return Ok(ApiResponse<PagedResult<CategoryResponse>>.Ok(200, responses, "Category retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }
    }
}
