using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class SupplierRepository(StoreDbContext store) : ISupplierRepository
    {
        private readonly StoreDbContext Context = store;
        

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await Context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await Context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Supplier supplier)
        {
            Context.Suppliers.Add(supplier);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            Context.Suppliers.Update(supplier);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Supplier? supplier = await GetByIdAsync(id);

            if (supplier == null)
                return;

            Context.Suppliers.Remove(supplier);
            await Context.SaveChangesAsync();
        }
    }
}
