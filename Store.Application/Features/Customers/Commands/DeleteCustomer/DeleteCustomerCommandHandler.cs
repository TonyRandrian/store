using MediatR;
using Store.Application.Interfaces;

namespace Store.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _customerRepository.IsUsed(request.Id))
            {
                throw new InvalidOperationException("This customer is attributed to invoices, cannot delete");
            }

            await _customerRepository.DeleteAsync(request.Id);
        }
    }
}
