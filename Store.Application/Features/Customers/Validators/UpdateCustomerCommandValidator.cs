using FluentValidation;
using Store.Application.Features.Customers.Commands.UpdateCustomer;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Customers.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;


        public UpdateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
    
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(c => c.Id)
                .MustAsync(async (id, cancellationToken) => await _customerRepository.GetByIdAsync(id) != null)
                .WithMessage(c => $"No customer with the id {c.Id} found");
        }
    }
}
