using MediatR;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository)
        : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository = productRepository;


        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteAsync(request.Id);
        }
    }
}
