using MediatR;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Commands.UpdateProductImage
{
    public record UpdateProductImageCommand(
        Guid ProductId,
        Guid ImageId,
        CreateProductFile File) : IRequest<ProductResponse>;
}
