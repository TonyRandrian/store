using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Commands.UpdateProductImage
{
    public class UpdateProductImageCommandHandler(
        IProductRepository productRepository,
        IImageRepository imageRepository)
    {
    }
}
