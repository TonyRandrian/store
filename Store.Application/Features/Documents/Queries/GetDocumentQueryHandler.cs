using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Documents.Queries
{
    public class GetDocumentQueryHandler(IDocumentRepository documentRepository)
        : IRequestHandler<GetDocumentsQuery, PagedResult<DocumentResponse>>
    {
        private readonly IDocumentRepository _documentRepository = documentRepository;


        public async Task<PagedResult<DocumentResponse>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Document> documents = await _documentRepository.GetAllAsync(request.PageNumber, request.PageSize);
            PagedResult<DocumentResponse> responses = new()
            {
                TotalRecords = documents.TotalRecords,
                PageNumber = documents.PageNumber,
                PageSize = documents.PageSize
            };

            foreach (Document doc in documents.Data)  
            {
                responses.Data.Add(new DocumentResponse(doc));
            }

            return responses;
        }
    }
}
