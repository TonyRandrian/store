using MediatR;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.AddProductImage
{
    public class AddProductImageCommandHandler(
        IProductRepository productRepository,
        IFileStorageService fileStorageService)
        : IRequestHandler<AddProductImageCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IFileStorageService _fileStorageService = fileStorageService;


        public async Task<ProductResponse> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"No product with the id {request.Id} found");

            // TODO: fetch allowed extension from settings files
            string[] allowedExtension = { "png", "jpg", "jpeg", "webp" };

            List<(FileUpload File, string Extension)> validatedFiles = [];
            foreach (FileUpload file in request.Uploads)
            {
                string extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();

                if (!allowedExtension.Contains(extension))
                {
                    throw new ArgumentException($"Extension {extension} not valid");
                }

                validatedFiles.Add((file, extension));
            }

            List<string> savedPaths = [];
            try
            {
                foreach ((FileUpload file, string extension) in validatedFiles)
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
