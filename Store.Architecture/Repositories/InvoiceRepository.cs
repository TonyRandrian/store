using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class InvoiceRepository(StoreDbContext context) : IInvoiceRepository
    {
        private readonly StoreDbContext _context = context;

            
        public async Task<PagedResult<Invoice>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Invoices.CountAsync();
            List<Invoice> invoices = await _context.Invoices
                .Include(i => i.Customer)
                .AsNoTracking()
                .OrderBy(i => i.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Invoice>()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                Data = invoices,
                TotalRecords = totalRecords
            };
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Invoice> UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task DeleteAsync(Guid id)
        {
            Invoice? invoice = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice with the id {id} found");

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
