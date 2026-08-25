using FluentValidation;
using Store.Application.Features.Categories.Commands.CreateCategory;
using Store.Application.Features.Products.Commands.CreateProduct;

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
