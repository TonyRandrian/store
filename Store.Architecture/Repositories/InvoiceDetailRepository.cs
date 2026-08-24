using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class InvoiceDetailRepository(StoreDbContext context) : IInvoiceDetailsRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<InvoiceDetail>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.InvoiceDetails.CountAsync();
            List<InvoiceDetail> invoicesDetails = await _context.InvoiceDetails
                .Include(i => i.Product)
                .Include(i => i.Invoice)
                .AsNoTracking()
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<InvoiceDetail>
            {
                TotalRecords = totalRecords,
                Data = invoicesDetails,
                PageNumber = pageNum,
                PageSize = pageSize
            };
        }

        public async Task<InvoiceDetail?> GetByIdAsync(Guid id)
        {
            return await _context.InvoiceDetails
                .Include(i => i.Product)
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<InvoiceDetail> AddAsync(InvoiceDetail invoiceDetail)
        {
            await _context.InvoiceDetails.AddAsync(invoiceDetail);
            await _context.SaveChangesAsync();

            return invoiceDetail;
        }

        public async Task<InvoiceDetail> UpdateAsync(InvoiceDetail invoiceDetail)
        {
            _context.InvoiceDetails.Update(invoiceDetail);
            await _context.SaveChangesAsync();

            return invoiceDetail;
        }

        public async Task DeleteAsync(Guid id)
        {
            InvoiceDetail? invoiceDetail = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {id} found");

            _context.InvoiceDetails.Remove(invoiceDetail);
            await _context.SaveChangesAsync();
        }
    }
}
