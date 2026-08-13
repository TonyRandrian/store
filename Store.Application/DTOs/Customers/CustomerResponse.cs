using Store.Domain.Entities;

namespace Store.Application.DTOs.Customers
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public CustomerResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CustomerResponse(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
        }
    }
}
