using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;

namespace Store.Application.Features.Documents.Queries
{
    public record GetDocumentsQuery(int PageNumber, int PageSize) : IRequest<PagedResult<DocumentResponse>>;
}
