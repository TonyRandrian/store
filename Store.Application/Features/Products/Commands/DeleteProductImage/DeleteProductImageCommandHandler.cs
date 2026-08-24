using MediatR;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.DeleteProductImage
{
    public class DeleteProductImageCommandHandler(
        IProductRepository productRepository,
        IImageRepository imageRepository,
        IFileStorageService fileStorageService)
        : IRequestHandler<DeleteProductImageCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IImageRepository _imageRepository = imageRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;


        public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            Image? imageFound = null;
            foreach (Image image in product.Images)
            {
                if (image.Id == request.ImageId)
                {
                    imageFound = image;
                    break;
                }
            }

            if (imageFound == null)
            {
                throw new KeyNotFoundException($"This product does not contain any image with the id {request.ImageId}");
            }

            // remove from product-images table
            product.RemoveImage(imageFound);
            await _productRepository.UpdateAsync(product);

            // remove from images table
            //await _imageRepository.DeleteAsync(imageFound.Id);

            // remove from storage
            await _fileStorageService.DeleteAsync(
                Path.Combine("uploads", "products/Images", imageFound.FileName).Replace("\\", "/"));
        }
    }
}
