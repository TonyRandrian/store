using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.InvoicesDetails;
using Store.Application.Features.InvoicesDetails.Commands.CreateInvoiceDetail;
using Store.Application.Features.InvoicesDetails.Commands.DeleteInvoiceDetail;
using Store.Application.Features.InvoicesDetails.Commands.UpdateInvoiceDetail;
using Store.Application.Features.InvoicesDetails.Queries.GetInvoiceDetail;
using Store.Application.Features.InvoicesDetails.Queries.GetInvoicesDetails;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices-details")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class InvoicesDetailsController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> Create(
            [FromBody] CreateInvoiceDetailRequest request)
        {
            try
            {
                InvoiceDetailResponse response = await _mediator.Send(new CreateInvoiceDetailCommand(
                    request.InvoiceId, request.ProductId, request.Quantity));
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(201, response, "Created Successfully"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteInvoiceDetailCommand(id));
                return Ok(ApiResponse<object>.Ok(204, null, "Invoice detail deleted"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> GetInvoiceDetail([FromRoute] Guid id)
        {
            try
            {
                InvoiceDetailResponse result = await _mediator.Send(new GetInvoiceDetailQuery(id));
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(200, result, "Invoice detail retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<object>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<InvoiceDetailResponse>>>> GetInvoicesDetails(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<InvoiceDetailResponse> response = await _mediator.Send(new GetInvoicesDetailsQuery(
                pageNum, pageSize));
            return Ok(ApiResponse<PagedResult<InvoiceDetailResponse>>.Ok(200, response, "Invoices details retrieved"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceDetailResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateInvoiceDetailRequest request)
        {
            try
            {
                InvoiceDetailResponse response = await _mediator.Send(new UpdateInvoiceDetailCommand(
                    id, request.ProductId, request.Quantity));
                return Ok(ApiResponse<InvoiceDetailResponse>.Ok(201, response, "Invoice detail updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<InvoiceDetailResponse>.Error(404, knf.Message));
            }
        }
    }
}
