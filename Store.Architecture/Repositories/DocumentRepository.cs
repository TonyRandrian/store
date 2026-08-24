using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class DocumentRepository(StoreDbContext context) : IDocumentRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<Document>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Documents.CountAsync();

            List<Document> data = await _context.Documents
                .Include(d => d.Product)
                .AsNoTracking()
                .OrderBy(d => d.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Document>()
            {
                TotalRecords = totalRecords,
                Data = data,
                PageNumber = pageNum,
                PageSize = pageSize
            };
        }
    }
}
