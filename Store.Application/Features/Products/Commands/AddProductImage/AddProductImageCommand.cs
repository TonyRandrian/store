using MediatR;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Commands.AddProductImage
{
    public record AddProductImageCommand(Guid Id, List<FileUpload> Uploads)
        : IRequest<ProductResponse>;
}
