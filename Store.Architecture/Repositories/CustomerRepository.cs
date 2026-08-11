using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class CustomerRepository(StoreDbContext context) : ICustomerRepository
    {
        private readonly StoreDbContext Context = context;


        public async Task<List<Customer>> AGetAll()
        {
            return await Context.Customers.ToListAsync();
        }

        public async Task<Customer?> AGetById(int id)
        {
            return await Context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AAdd(Customer customer)
        {
            Context.Customers.Add(customer);
            await Context.SaveChangesAsync();
        }

        public async Task AUpdate(Customer customer)
        {
            Context.Customers.Update(customer);
            await Context.SaveChangesAsync();
        }

        public async Task ADelete(int id)
        {
            Customer? customer = await AGetById(id);

            if (customer == null)
                return;

            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
        }
    }
}
