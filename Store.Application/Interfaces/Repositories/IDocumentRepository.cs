using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<PagedResult<Document>> GetAllAsync(int pageNum, int pageSize);
    }
}
