using Store.Domain.Entities;

namespace Store.Application.DTOs.Customers
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HashSet<Guid> InvoicesIds { get; set; }


        public CustomerResponse(Guid id, string name, HashSet<Guid> invoicesIds)
        {
            Id = id;
            Name = name;
            InvoicesIds = invoicesIds;
        }

        public CustomerResponse(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            InvoicesIds = [];

            foreach (Invoice invoice in customer.Invoices)
            {
                InvoicesIds.Add(invoice.Id);
            }
        }
    }
}
