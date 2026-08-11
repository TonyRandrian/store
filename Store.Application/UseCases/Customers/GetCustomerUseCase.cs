using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Customers
{
    public class GetCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task<Customer?> Execute(int id)
        {
            return await CustomerRepository.GetByIdAsync(id);
        }
    }
}
