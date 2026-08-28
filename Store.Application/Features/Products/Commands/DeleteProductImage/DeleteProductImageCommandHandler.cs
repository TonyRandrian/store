using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.Services;
using Store.Application.Settings;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.DeleteProductImage
{
    public class DeleteProductImageCommandHandler(
        IProductRepository productRepository,
        IFileStorageService fileStorageService,
        IOptions<FileStorageSettings> settings)
        : IRequestHandler<DeleteProductImageCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly FileStorageSettings _settings = settings.Value;

         
        public async Task Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId);
            Image? imageFound = null;
            foreach (Image image in product!.Images)
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
                Path.Combine(_settings.UploadDir, _settings.ProductImageFolder, imageFound.FileName).Replace("\\", "/"));
        }
    }
}
