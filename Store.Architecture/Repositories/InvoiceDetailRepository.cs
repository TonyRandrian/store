using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class InvoiceDetailRepository(StoreDbContext context) : IInvoiceDetailsRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<PagedResult<InvoiceDetail>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await Context.InvoiceDetails.CountAsync();
            List<InvoiceDetail> invoicesDetails = await Context.InvoiceDetails
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
            return await Context.InvoiceDetails
                .Include(i => i.Product)
                .Include(i => i.Invoice)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<InvoiceDetail> AddAsync(InvoiceDetail invoiceDetail)
        {
            await Context.InvoiceDetails.AddAsync(invoiceDetail);
            await Context.SaveChangesAsync();

            return invoiceDetail;
        }

        public async Task<InvoiceDetail> UpdateAsync(InvoiceDetail invoiceDetail)
        {
            Context.InvoiceDetails.Update(invoiceDetail);
            await Context.SaveChangesAsync();

            return invoiceDetail;
        }

        public async Task DeleteAsync(Guid id)
        {
            InvoiceDetail? invoiceDetail = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No invoice detail with the id {id} found");

            Context.InvoiceDetails.Remove(invoiceDetail);
            await Context.SaveChangesAsync();
        }
    }
}
