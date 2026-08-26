using FluentValidation;
using Store.Application.Features.Categories.Commands.DeleteCategory;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Categories.Validators
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;


        public DeleteCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Id)
                .MustAsync((id, cancellationToken) => _categoryRepository.Exists(id))
                    .WithMessage(c => $"No category with the id {c.Id} found")
                .MustAsync((id, cancellationToken) => _categoryRepository.IsUsed(id))
                    .WithMessage("Other Category or Product still use this category, cannot delete");
        }
    }
}
