using Store.Application.Interfaces;

namespace Store.Application.UseCases.Customers
{
    public class DeleteCustomerUseCase(ICustomerRepository customerRepository)
    {
        private readonly ICustomerRepository CustomerRepository = customerRepository;


        public async Task Execute(Guid id)
        {
            if (await CustomerRepository.IsUsed(id))
            {
                throw new InvalidOperationException("This customer is attributed to invoices, cannot delete");
            }

            await CustomerRepository.DeleteAsync(id);
        }
    }
}
