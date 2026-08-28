using FluentValidation;
using Store.Application.Features.Products.Commands.AddProductImage;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class AddProductImageCommandValidator
        : AbstractValidator<AddProductImageCommand>
    {
        private readonly IProductRepository _productRepository;

        public AddProductImageCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Id)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No product with the id {p.Id} found");
        }
    }
}
 