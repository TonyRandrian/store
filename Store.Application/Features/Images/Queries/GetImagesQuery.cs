using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;

namespace Store.Application.Features.Images.Queries
{
    public record GetImagesQuery(int PageNumber, int PageSize) : IRequest<PagedResult<ImageResponse>>;
}
