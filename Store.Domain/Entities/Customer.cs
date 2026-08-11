namespace Store.Domain.Entities
{
    public class Customer
    {

        public Customer(string name, List<Invoice>? invoices = null)
        {
            Name = name;
            Invoices = invoices ?? [];
        }

        public Customer()
        {
            Name = "";
            Invoices = [];
        }

        public int Id
        {
            get;

            set;
        }

        public string Name
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a customer with a null or empty name");
                field = value;
            }
        }

        public List<Invoice> Invoices
        {
            get;

            set;
        }
    }
}
