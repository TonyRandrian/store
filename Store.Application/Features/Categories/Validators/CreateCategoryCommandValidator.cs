using FluentValidation;
using Store.Application.Features.Categories.Commands.CreateCategory;

namespace Store.Application.Features.Categories.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {

        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
