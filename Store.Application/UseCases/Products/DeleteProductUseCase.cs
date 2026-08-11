using Store.Application.Interfaces;

namespace Store.Application.UseCases.Products
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task Excecute(int id)
        {
            await ProductRepository.DeleteAsync(id);
        }
    }
}
