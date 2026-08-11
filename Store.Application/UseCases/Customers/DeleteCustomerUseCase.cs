using Store.Application.Interfaces;

namespace Store.Application.UseCases.Customers
{
    public class DeleteCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task Execute(int id)
        {
            await CustomerRepository.DeleteAsync(id);
        }
    }
}
