using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    internal class GetCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<Category?> Excecute(int id)
        {
            return await CategoryRepository.GetByIdAsync(id);
        }
    }
}
