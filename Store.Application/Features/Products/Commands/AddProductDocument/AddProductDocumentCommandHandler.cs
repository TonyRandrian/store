using MediatR;
using Microsoft.Extensions.Options;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.Services;
using Store.Application.Settings;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.AddProductDocument
{
    public class AddProductDocumentCommandHandler(
        IProductRepository productRepository,
        IFileStorageService fileStorageService,
        IOptions<FileStorageSettings> settings)
        : IRequestHandler<AddProductDocumentCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly FileStorageSettings _settings = settings.Value;


        public async Task<ProductResponse> Handle(AddProductDocumentCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.ProductId)
                ?? throw new KeyNotFoundException($"No product with the id {request.ProductId} found");

            string extension = Path.GetExtension(request.File.FileName).TrimStart('.').ToLowerInvariant();

            if (!_settings.AllowedDocumentExtensions.Contains(extension))
            {
                throw new ArgumentException($"Extension {extension} not valid");
            }

            string savedPath = string.Empty;
            try
            {
                savedPath = await _fileStorageService.SaveAsync(
                request.File.Content, request.File.FileName, "products/docs");

                Document doc = new()
                {
                    Extension = extension,
                    FileName = request.File.FileName,
                    OriginalFileName = request.File.FileName,
                    Path = savedPath,
                    Size = request.File.Size,
                    Product = product
                };

                product.Document = doc;
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
