using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Application.Settings;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.AddProductImage
{
    public class AddProductImageCommandHandler(
        IProductRepository productRepository,
        IFileStorageService fileStorageService,
        IOptions<FileStorageSettings> settings)
        : IRequestHandler<AddProductImageCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly FileStorageSettings _settings = settings.Value;


        public async Task<ProductResponse> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No product with the id {request.Id} found");

            List<(CreateProductFile File, string Extension)> validatedFiles = [];
            foreach (CreateProductFile file in request.Uploads)
            {
                string extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();

                if (!_settings.AllowedImageExtensions.Contains(extension))
                {
                    throw new ArgumentException($"Extension {extension} not valid");
                }

                validatedFiles.Add((file, extension));
            }

            List<string> savedPaths = [];
            try
            {
                foreach ((CreateProductFile file, string extension) in validatedFiles)
                {
                    string savedPath = await _fileStorageService.SaveAsync(
                        file.Content, file.FileName, "products/images");

                    savedPaths.Add(savedPath);

                    Image image = new()
                    {
                        Extension = extension,
                        FileName = file.FileName,
                        OriginalFileName = file.FileName,
                        Path = savedPath,
                        Size = file.Size,
                        Product = product
                    };

                    product.AddImage(image);
                }
            } 
            catch 
            {
                foreach (string path in savedPaths)
                {
                    await _fileStorageService.DeleteAsync(path);
                }

                throw;
            }

            product = await _productRepository.UpdateAsync(product);

            return new ProductResponse(product);
        }
    }
}
