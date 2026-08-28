using FluentValidation;
using MediatR;
using Store.Application.Features.Products.Commands.UpdateProductImage;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class UpdateProductImageValidator
        : AbstractValidator<UpdateProductImageCommand>
    {
        private readonly IProductRepository _productRepository;


        public UpdateProductImageValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.ProductId)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No product with the id {p.ProductId} found");
        }
    }
}
