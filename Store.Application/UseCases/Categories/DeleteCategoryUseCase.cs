using Store.Application.Interfaces;

namespace Store.Application.UseCases.Categories
{
    public class DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task Execute(int id)
        {
            await CategoryRepository.DeleteAsync(id);
        }
    }
}
