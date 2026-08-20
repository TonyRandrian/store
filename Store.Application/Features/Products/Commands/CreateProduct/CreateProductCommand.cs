using MediatR;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, decimal Price, Guid CategoryId, List<Guid> SuppliersIds)
        : IRequest<ProductResponse>;
}
