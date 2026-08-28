using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.Services;
using Store.Application.Settings;
using Store.Domain.Entities;
using Store.Domain.Validators;

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
            // validation
            Product? product = await _productRepository.GetByIdAsync(request.Id);
            List<(CreateProductFile File, string Extension)> validatedFiles = [];
            foreach (CreateProductFile file in request.Uploads)
            {
                string extension = FileValidator.ValidateAndGetExtension(file.FileName, _settings.AllowedImageExtensions);
                validatedFiles.Add((file, extension));
            }

            // creation & attribution
            List<string> savedPaths = [];
            try
            {
                foreach ((CreateProductFile file, string extension) in validatedFiles)
                {
                    string savedPath = await _fileStorageService.SaveAsync(
                        file.Content, file.FileName, _settings.ProductImageFolder);

                    savedPaths.Add(savedPath);
                    product!.AddImage(file.FileName, extension, savedPath, file.Size);
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

            // persistence
            product = await _productRepository.UpdateAsync(product);
            return new ProductResponse(product);
        }
    }
}
