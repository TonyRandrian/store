using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class SupplierRepository(StoreDbContext store) : ISupplierRepository
    {
        private readonly StoreDbContext Context = store;
        

        public async Task<PagedResult<Supplier>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await Context.Suppliers.CountAsync();
            List<Supplier> suppliers = await Context.Suppliers
                .Include(s => s.Products)
                .ThenInclude(p => p.Category)
                .AsNoTracking()
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Supplier>()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                Data = suppliers,
                TotalRecords = totalRecords
            };
        }

        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            return await Context.Suppliers
                .Include(s => s.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            await Context.Suppliers.AddAsync(supplier);
            await Context.SaveChangesAsync();

            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            Context.Suppliers.Update(supplier);
            await Context.SaveChangesAsync();

            return supplier;
        }

        public async Task DeleteAsync(Guid id)
        {
            Supplier? supplier = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No supplier with the id {id} found");

            Context.Suppliers.Remove(supplier);
            await Context.SaveChangesAsync();
        }
    }
}
