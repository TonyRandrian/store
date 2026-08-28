using FluentValidation;
using Store.Application.Features.Categories.Commands.UpdateCategory;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Categories.Validators
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;


        public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(c => c.Id)
                .MustAsync((id, cancellationToken) => _categoryRepository.Exists(id))
                    .WithMessage(c => $"No category with the id {c.Id} found");

            RuleFor(c => c.ParentCategoryId)
                .MustAsync((parentId, cancellationToken) => _categoryRepository.Exists(parentId!.Value))
                    .WithMessage(c => $"No category with the id {c.ParentCategoryId} found")
                .Must((command, parentId) => parentId != command.Id)
                    .WithMessage("Cannot be a parent of itself")
                .When(c => c.ParentCategoryId.HasValue);
        }
    }
}
