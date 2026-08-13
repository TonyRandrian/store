using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CustomerRepository(StoreDbContext context) : ICustomerRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<List<Customer>> GetAllAsync()
        {
            return await Context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await Context.Customers.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task DeleteAsync(int id)
        {
            Customer? customer = await GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"No customer with the id {id} found");

            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
        }
    }
}
