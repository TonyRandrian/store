using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class InvoiceRepository(StoreDbContext context) : IInvoiceRepository
    {
        private readonly StoreDbContext Context = context;

            
        public async Task<PagedResult<Invoice>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await Context.Invoices.CountAsync();
            List<Invoice> invoices = await Context.Invoices
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
            return await Context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await Context.Invoices.AddAsync(invoice);
            await Context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Invoice> UpdateAsync(Invoice invoice)
        {
            Context.Invoices.Update(invoice);
            await Context.SaveChangesAsync();

            return invoice;
        }

        public async Task DeleteAsync(Guid id)
        {
            Invoice? invoice = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice with the id {id} found");

            Context.Invoices.Remove(invoice);
            await Context.SaveChangesAsync();
        }
    }
}
