using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class SupplierRepository(StoreDbContext store) : ISupplierRepository
    {
        private readonly StoreDbContext Context = store;
        

        public async Task<List<Supplier>> AGetAll()
        {
            return await Context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> AGetById(int id)
        {
            return await Context.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AAdd(Supplier supplier)
        {
            Context.Suppliers.Add(supplier);
            await Context.SaveChangesAsync();
        }

        public async Task AUpdate(Supplier supplier)
        {
            Context.Suppliers.Update(supplier);
            await Context.SaveChangesAsync();
        }

        public async Task ADelete(int id)
        {
            Supplier? supplier = await AGetById(id);

            if (supplier == null)
                return;

            Context.Suppliers.Remove(supplier);
            await Context.SaveChangesAsync();
        }
    }
}
