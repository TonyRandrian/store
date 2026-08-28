using FluentValidation;
using Store.Application.Features.Products.Commands.DeleteProductImage;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Validators
{
    public class DeleteProductImageCommandValidator
        : AbstractValidator<DeleteProductImageCommand>
    {
        private readonly IProductRepository _productRepository;


        public DeleteProductImageCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.ProductId)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                    .WithMessage(p => $"No product with the id {p.ProductId} found");
        }
    }
}
