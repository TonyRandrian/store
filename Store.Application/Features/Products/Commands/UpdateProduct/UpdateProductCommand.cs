using MediatR;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        decimal Price,
        Guid CategoryId,
        List<Guid> SuppliersIds) : IRequest<ProductResponse>;
}
