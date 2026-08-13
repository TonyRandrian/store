using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Customers;
using Store.Application.UseCases.Customers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController(
        GetCustomerUseCase getCustomerUseCase,
        GetCustomersUseCase getCustomersUseCase) : ControllerBase
    {
        private readonly GetCustomerUseCase GetCustomerUseCase = getCustomerUseCase;
        private readonly GetCustomersUseCase GetCustomersUseCase = getCustomersUseCase;




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            CustomerResponse? response = await GetCustomerUseCase.Execute(id);

            if (response != null)
            {
                return Ok(response);
            }

            return NotFound(new { Message = $"No customer with the id {id} found"});
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            List<CustomerResponse> responses = await GetCustomersUseCase.Execute();

            return Ok(responses);
        }
    }
}
