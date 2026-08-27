using FluentValidation;
using Store.Application.Features.Products.Commands.CreateProduct;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class CreateProductCommandValidator
        : AbstractValidator<CreateProductCommand>
    {
        private readonly ICategoryRepository _categoryRepository;


        public CreateProductCommandValidator(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(p => p.CategoryId)
                .MustAsync(async (id, cancellationToken) => await _categoryRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No category with the id {p.CategoryId} found");
        }
    }
}
