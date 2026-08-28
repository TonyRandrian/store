using FluentValidation;
using Store.Application.Features.Products.Commands.UpdateProduct;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Validators
{
    public class UpdateProductCommandValidator
        : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;


        public UpdateProductCommandValidator(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

            RuleFor(p => p.CategoryId)
                .MustAsync(async (id, cancellationToken) => await _categoryRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No category with the id {p.CategoryId} found");

            RuleFor(p => p.Id)
                .MustAsync(async (id, cancellationToken) => await _productRepository.GetByIdAsync(id) != null)
                .WithMessage(p => $"No product with the id {p.Id} found");
        }
    }
}
