using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;
using Store.Application.Features.Images.Queries;

namespace Store.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/images")]
    public class ImagesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<ImageResponse>>>> GetImages(int pageNum, int pageSize)
        {
            PagedResult<ImageResponse> responses = await _mediator.Send(new GetImagesQuery(pageNum, pageSize));
            return Ok(ApiResponse<PagedResult<ImageResponse>>.Ok(200, responses, "Images retrieved"));
        }
    }
}
