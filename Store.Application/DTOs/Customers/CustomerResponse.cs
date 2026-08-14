using Store.Domain.Entities;

namespace Store.Application.DTOs.Customers
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<int> InvoicesIds { get; set; }


        public CustomerResponse(int id, string name, HashSet<int> invoicesIds)
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
