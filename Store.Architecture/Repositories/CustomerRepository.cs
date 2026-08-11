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

        public async Task AddAsync(Customer customer)
        {
            Context.Customers.Add(customer);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            Context.Customers.Update(customer);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Customer? customer = await GetByIdAsync(id);

            if (customer == null)
                return;

            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
        }
    }
}
