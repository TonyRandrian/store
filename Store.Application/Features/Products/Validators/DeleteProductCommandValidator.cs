using FluentValidation;
using Store.Application.Features.Products.Commands.DeleteProduct;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class DeleteProductCommandValidator
        : AbstractValidator<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;


        public DeleteProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.Id)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                    .WithMessage(p => $"No product with the id {p.Id} found")
                .MustAsync(async (id, cancellationToken) => !await _productRepository.IsUsed(id))
                    .WithMessage("This product is used by one or many suppliers, cannot delete");
        }
    }
}
