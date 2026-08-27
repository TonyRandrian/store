using MediatR;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;


        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteAsync(request.Id);
        }
    }
}
