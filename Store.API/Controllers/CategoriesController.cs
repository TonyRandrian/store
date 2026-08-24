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
            CategoryResponse response = await _mediator.Send(
                new CreateCategoryCommand(request.Name, request.ParentCategoryId));

            return Ok(ApiResponse<CategoryResponse>.Ok(201, response, "Category created successfully"));
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
            CategoryResponse response = await _mediator.Send(new GetCategoryQuery(id));

            return Ok(ApiResponse<CategoryResponse>.Ok(200, response, "Category retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));

            return Ok(ApiResponse<object>.Ok(204, null, "Category deleted"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCategoryRequest request)
        {
            CategoryResponse response = await _mediator.Send(new UpdateCategoryCommand(
                id, request.Name, request.ParentCategoryId));

            return Ok(ApiResponse<CategoryResponse>.Ok(201, response, "Category updated"));
        }

        [HttpGet("{categoryId:Guid}/products")]
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
            PagedResult<CategoryResponse> responses = await _mediator.Send(new GetCategoryChildrenQuery(
                categoryId, pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<CategoryResponse>>.Ok(200, responses, "Category retrieved"));
        }
    }
}
