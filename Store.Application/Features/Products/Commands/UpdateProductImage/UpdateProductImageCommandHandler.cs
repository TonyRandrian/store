using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.Services;
using Store.Application.Settings;
using Store.Domain.Entities;
using Store.Domain.Validators;

namespace Store.Application.Features.Products.Commands.UpdateProductImage
{
    public class UpdateProductImageCommandHandler(
        IProductRepository productRepository,
        IImageRepository imageRepository,
        IFileStorageService fileStorageService,
        IOptions<FileStorageSettings> settings)
        : IRequestHandler<UpdateProductImageCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IImageRepository _imageRepository = imageRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly FileStorageSettings _settings = settings.Value;


        public async Task<ProductResponse> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            // validation
            Product product = await _productRepository.GetByIdAsync(request.ProductId)
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

            // set null in te file table to remove the link between product-file
            product.RemoveImage(imageFound);

            string extension = ImageValidator.ValidateAndGetExtension(request.File.FileName, _settings.AllowedDocumentExtensions);

            string savedPath = string.Empty;
            try
            {
                savedPath = await _fileStorageService.SaveAsync(
                    request.File.Content, request.File.FileName, "products/images");

                product.AddImage(request.File.FileName, extension, savedPath, request.File.Size);
            }
            catch
            {
                await _fileStorageService.DeleteAsync(savedPath);
                throw;
            }

            product = await _productRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
