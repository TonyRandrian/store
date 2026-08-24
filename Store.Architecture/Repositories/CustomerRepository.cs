using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CustomerRepository(StoreDbContext context) : ICustomerRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<PagedResult<Customer>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Customers.CountAsync();
            List<Customer> data = await _context.Customers
                .Include(c => c.Invoices)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Customer>
            {
                TotalRecords = totalRecords,
                Data = data,
                PageNumber = pageNum,
                PageSize = pageSize
            };
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteAsync(Guid id)
        {
            Customer? customer = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No customer with the id {id} found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            return await _context.Invoices.AnyAsync(i => i.Customer.Id == id);
        }
    }
}
