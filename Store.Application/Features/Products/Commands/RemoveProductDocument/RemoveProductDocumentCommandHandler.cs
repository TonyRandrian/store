using MediatR;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Products.Commands.RemoveProductDocument
{
    public class RemoveProductDocumentCommandHandler(IProductRepository productRepository)
        : IRequestHandler<RemoveProductDocumentCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task Handle(RemoveProductDocumentCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId);

            product!.Document = null;
            await _productRepository.UpdateAsync(product);
        }
    }
}
