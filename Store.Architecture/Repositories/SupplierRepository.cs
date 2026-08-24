using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class SupplierRepository(StoreDbContext context) : ISupplierRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<Supplier>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Suppliers.CountAsync();
            List<Supplier> suppliers = await _context.Suppliers
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
            return await _context.Suppliers
                .Include(s => s.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        public async Task DeleteAsync(Guid id)
        {
            Supplier? supplier = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No supplier with the id {id} found");

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<Product>> GetSupplierProducts(Guid supplierId, int pageNum, int pageSize)
        {
            IQueryable<Product> query = _context.Suppliers
                .Where(s => s.Id == supplierId)
                .SelectMany(s => s.Products);

            int totalRecords = await query.CountAsync();
            List<Product> products = await query
                .Include(p => p.Category)
                .ToListAsync();

            return new PagedResult<Product>()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Data = products
            };
        }
    }
}
