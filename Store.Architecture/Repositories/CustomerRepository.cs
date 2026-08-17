using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CustomerRepository(StoreDbContext context) : ICustomerRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<PagedResult<Customer>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await Context.Customers.CountAsync();
            List<Customer> data = await Context.Customers
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
            return await Context.Customers
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await Context.Customers.AddAsync(customer);
            await Context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            Context.Customers.Update(customer);
            await Context.SaveChangesAsync();

            return customer;
        }

        public async Task DeleteAsync(Guid id)
        {
            Customer? customer = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No customer with the id {id} found");

            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsUsed(Guid id)
        {
            return await Context.Invoices.AnyAsync(i => i.Customer.Id == id);
        }
    }
}
