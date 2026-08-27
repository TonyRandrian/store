using FluentValidation;
using MediatR;
using Store.Application.Features.Products.Commands.RemoveProductDocument;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class RemoveProductDocumentCommandValidator
        : AbstractValidator<RemoveProductDocumentCommand>
    {
        private readonly IProductRepository _productRepository;


        public RemoveProductDocumentCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(p => p.ProductId)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No product with the id {p.ProductId} found");
        }
    }
}
