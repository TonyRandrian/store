using MediatR;
using Store.Application.Interfaces;

namespace Store.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (await _productRepository.IsUsed(request.Id))
                throw new InvalidOperationException("This product is used by one or many suppliers, cannot delete");

            await _productRepository.DeleteAsync(request.Id);
        }
    }
}
