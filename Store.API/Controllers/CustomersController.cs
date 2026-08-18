using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.API.Commons;
using Store.Application.Commons;
using Store.Application.DTOs.Customers;
using Store.Application.UseCases.Customers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CustomersController(
        GetCustomerUseCase getCustomerUseCase,
        GetCustomersUseCase getCustomersUseCase,
        CreateCustomerUseCase createCustomerUseCase,
        DeleteCustomerUseCase deleteCustomerUseCase,
        UpdateCustomerUseCase updateCustomerUseCase) : ControllerBase
    {
        private readonly GetCustomerUseCase GetCustomerUseCase = getCustomerUseCase;
        private readonly GetCustomersUseCase GetCustomersUseCase = getCustomersUseCase;
        private readonly CreateCustomerUseCase CreateCustomerUseCase = createCustomerUseCase;
        private readonly DeleteCustomerUseCase DeleteCustomerUseCase = deleteCustomerUseCase;
        private readonly UpdateCustomerUseCase UpdateCustomerUseCase = updateCustomerUseCase;


        [HttpPost]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> Create(CreateCustomerRequest request)
        {
            CustomerResponse response = await CreateCustomerUseCase.Execute(request);
            return Ok(ApiResponse<CustomerResponse>.Ok(201, response, "Customer Created"));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> GetCustomer([FromRoute] Guid id)
        {
            CustomerResponse? response = await GetCustomerUseCase.Execute(id);

            if (response != null)
            {
                return Ok(ApiResponse<CustomerResponse>.Ok(200, response, "Data retrieved"));
            }

            return NotFound(ApiResponse<CustomerResponse>.Error(404, "Customer not found"));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<CustomerResponse>>>> GetCustomers(
            [FromQuery] int pageNum,
            [FromQuery] int pageSize)
        {
            PagedResult<CustomerResponse> responses = await GetCustomersUseCase.Execute(pageNum, pageSize);

            return Ok(ApiResponse<PagedResult<CustomerResponse>>.Ok(200, responses, "Customers retrieved"));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiResponse<object>>> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteCustomerUseCase.Execute(id);
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
                CustomerResponse response = await UpdateCustomerUseCase.Execute(id, request);
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
