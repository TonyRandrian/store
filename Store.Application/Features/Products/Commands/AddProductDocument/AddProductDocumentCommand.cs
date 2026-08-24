using MediatR;
using Store.Application.DTOs.Files;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Commands.AddProductDocument
{
    public record AddProductDocumentCommand(Guid ProductId, CreateProductFile File)
        : IRequest<ProductResponse>;
}
