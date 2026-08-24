using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Invoices;
using Store.Application.Features.Invoices.Commands.CreateInvoice;
using Store.Application.Features.Invoices.Commands.DeleteInvoice;
using Store.Application.Features.Invoices.Commands.UpdateInvoice;
using Store.Application.Features.Invoices.Queries.GetInvoice;
using Store.Application.Features.Invoices.Queries.GetInvoices;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class InvoicesController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<InvoiceResponse>>> Create([FromBody] CreateInvoiceRequest request)
        {
            InvoiceResponse response = await _mediator.Send(new CreateInvoiceCommand(
                request.Reference, request.Total, request.CustomerId));

            return Ok(ApiResponse<InvoiceResponse>.Ok(201, response, "Invoice created"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceResponse>>> GetInvoice([FromRoute] Guid id)
        {
            InvoiceResponse response = await _mediator.Send(new GetInvoiceQuery(id));

            return Ok(ApiResponse<InvoiceResponse>.Ok(200, response, "Invoice retrieved"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<InvoiceResponse>>>> GetInvoices(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<InvoiceResponse> responses = await _mediator.Send(new GetInvoicesQuery(pageNum, pageSize));

            return Ok(ApiResponse<PagedResult<InvoiceResponse>>.Ok(200, responses, "Invoices retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteInvoiceCommand(id));

            return Ok(ApiResponse<object>.Ok(204, null, "Invoice deleted"));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<InvoiceResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateInvoiceRequest request)
        {
            InvoiceResponse response = await _mediator.Send(new UpdateInvoiceCommand(
                id, request.Reference, request.Total, request.CustomerId));

            return Ok(ApiResponse<InvoiceResponse>.Ok(201, response, "Invoice udpdated"));
        }
    }
}
