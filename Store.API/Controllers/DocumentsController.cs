using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;
using Store.Application.Features.Documents.Queries;

namespace Store.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/documents")]
    public class DocumentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<DocumentResponse>>>> GetDocuments(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<DocumentResponse> responses = await _mediator.Send(new GetDocumentsQuery(pageNum, pageSize));
            return Ok(ApiResponse<PagedResult<DocumentResponse>>.Ok(200, responses, "Documents retrieve"));
        }
    }
}
