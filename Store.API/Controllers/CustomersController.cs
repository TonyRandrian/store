using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Customers;
using Store.Application.UseCases.Customers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
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
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            CustomerResponse response = await CreateCustomerUseCase.Execute(request);

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = response.Id },
                response
                );
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            CustomerResponse? response = await GetCustomerUseCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(new { Message = $"No customer with the id {id} found" });
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            List<CustomerResponse> responses = await GetCustomersUseCase.Execute();

            return Ok(responses);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await DeleteCustomerUseCase.Execute(id);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                CustomerResponse response = await UpdateCustomerUseCase.Execute(id, request);
                return Ok(response);
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(new { knf.Message });
            }
            catch (InvalidOperationException ioe)
            {
                return BadRequest(new { ioe.Message });
            }
        }
    }
}
