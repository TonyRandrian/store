using Microsoft.AspNetCore.Mvc;
using Store.Application.DTOs.Customers;
using Store.Application.UseCases.Customers;

namespace Store.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController(GetCustomerUseCase getCustomerUseCase) : ControllerBase
    {
        private readonly GetCustomerUseCase GetCustomerUseCase = getCustomerUseCase;


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
    }
}
