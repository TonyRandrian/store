using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class InvoiceRepository(StoreDbContext context) : IInvoiceRepository
    {
        private readonly StoreDbContext Context = context;

            
        public async Task<List<Invoice>> GetAllAsync()
        {
            return await Context.Invoices.ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await Context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task DeleteAsync(int id)
        {
            Invoice? invoice = await GetByIdAsync(id);

            if (invoice == null)
                return;

            Context.Invoices.Remove(invoice);
            await Context.SaveChangesAsync();
        }
    }
}
