using MediatR;
using Store.Application.DTOs.Categories;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;


        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? categoryParent = null;
            if (request.ParentCategoryId != null)
            {
                Guid id = request.ParentCategoryId.Value;

                if (!await _categoryRepository.Exists(id))
                {
                    throw new KeyNotFoundException($"No category with id {id} found, cannot create parent");
                }

                categoryParent = await _categoryRepository.GetByIdAsync(id);
            }

            Category category = new(request.Name, categoryParent);
            await _categoryRepository.AddAsync(category);

            return new CategoryResponse(category);
        }
    }
}
