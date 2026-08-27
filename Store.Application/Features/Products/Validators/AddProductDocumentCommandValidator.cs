using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.Features.Products.Commands.AddProductDocument;
using Store.Application.Interfaces.Repositories;
using Store.Application.Settings;
using Store.Domain.Validators;

namespace Store.Application.Features.Products.Validators
{
    public class AddProductDocumentCommandValidator
        : AbstractValidator<AddProductDocumentCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly FileStorageSettings _settings;


        public AddProductDocumentCommandValidator(
            IProductRepository productRepository,
            IOptions<FileStorageSettings> settings)
        {
            _productRepository = productRepository;
            _settings = settings.Value;

            RuleFor(p => p.ProductId)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No product with the id {p.ProductId} found");
        }
    }
}
