using Store.Application.Interfaces;

namespace Store.Application.UseCases.Products
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<Guid> Execute(Guid id)
        {
            if (await ProductRepository.IsUsed(id))
                throw new InvalidOperationException("This product is used by one or many suppliers, cannot delete");

            await ProductRepository.DeleteAsync(id);
            return id;
        }
    }
}
