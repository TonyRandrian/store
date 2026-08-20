using MediatR;
using Store.Application.DTOs.Products;

namespace Store.Application.Features.Products.Queries.GetProduct
{
    public record GetProductQuery(Guid Id) : IRequest<ProductResponse>;
}
