using MediatR;

namespace Store.Application.Features.Products.Commands.RemoveProductDocument
{
    public record RemoveProductDocumentCommand(Guid ProductId) : IRequest;
}
