using FluentValidation;
using Store.Application.Features.Categories.Commands.CreateCategory;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Categories.Validators
{
    public class CreateCategoryCommandValidator
        : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;


        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(c => c.ParentCategoryId)
                .MustAsync(ExistAsync)
                .WithMessage(c => $"No category with id {c.ParentCategoryId} found, cannot create parent")
                .When(c => c.ParentCategoryId.HasValue);                
        }

        private async Task<bool> ExistAsync(Guid? parentCategoryId, CancellationToken cancellationToken)
            => await _categoryRepository.Exists(parentCategoryId!.Value);
    }
}
