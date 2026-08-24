using MediatR;

namespace Store.Application.Features.Products.Commands.DeleteProductImage
{
    public record DeleteProductImageCommand(Guid ProductId, Guid ImageId)
        : IRequest;
}
