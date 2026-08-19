using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Customers;
using Store.Application.Features.Categories.Commands.UpdateCategory;
using Store.Application.Features.Customers.Commands.CreateCustomer;
using Store.Application.Features.Customers.Commands.DeleteCustomer;
using Store.Application.Features.Customers.Commands.UpdateCustomer;
using Store.Application.Features.Customers.Queries.GetCustomer;
using Store.Application.Features.Customers.Queries.GetCustomers;
using Store.Application.UseCases.Customers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CustomersController(
        IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> Create(CreateCustomerRequest request)
        {
            CustomerResponse response = await _mediator.Send(new CreateCustomerCommand(request.Name));
            return Ok(ApiResponse<CustomerResponse>.Ok(201, response, "Customer Created"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> GetCustomer([FromRoute] Guid id)
        {
            try
            {
                CustomerResponse? response = await _mediator.Send(new GetCustomerQuery(id));
                return Ok(ApiResponse<CustomerResponse>.Ok(200, response, "Customer retrieved"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<CustomerResponse>.Error(404, knf.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<CustomerResponse>>>> GetCustomers(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<CustomerResponse> responses = await _mediator.Send(new GetCustomersQuery(pageNum, pageSize));
            return Ok(ApiResponse<PagedResult<CustomerResponse>>.Ok(200, responses, "Customers retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCustomerCommand(id));
                return Ok(ApiResponse<object>.Ok(204, null, "Customer deleted"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<CustomerResponse>.Error(404, knf.Message));
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                CustomerResponse response = await _mediator.Send(new UpdateCustomerCommand(
                    id, request.Name));
                return Ok(ApiResponse<CustomerResponse>.Ok(201, response, "Customer Updated"));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(ApiResponse<CustomerResponse>.Error(404, knf.Message));
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(ApiResponse<CustomerResponse>.Error(400, ioe.Message));
            }
        }
    }
}
