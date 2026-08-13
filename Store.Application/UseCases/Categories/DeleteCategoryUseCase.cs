using Store.Application.Interfaces;

namespace Store.Application.UseCases.Categories
{
    public class DeleteCategoryUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<int> Execute(int id)
        {
            if (await CategoryRepository.IsUsed(id))
                throw new InvalidOperationException("Other Category or Product still use this category, cannot delete");

            await CategoryRepository.DeleteAsync(id);
            return id;
        }
    }
}
