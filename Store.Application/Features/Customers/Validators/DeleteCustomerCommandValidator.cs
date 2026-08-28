using FluentValidation;
using Store.Application.Features.Customers.Commands.DeleteCustomer;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Customers.Validators
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;


        public DeleteCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(c => c.Id)
                .MustAsync(async (id, cancellationToken) => await _customerRepository.GetByIdAsync(id) != null)
                    .WithMessage(c => $"No customer with the id {c.Id} found")
                .MustAsync(async (id, cancellationToken) => !await _customerRepository.IsUsed(id))
                    .WithMessage("This customer is attributed to invoices, cannot be deleted");
        }
    }
}
