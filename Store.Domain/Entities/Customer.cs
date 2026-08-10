namespace Store.Domain.Entities
{
    internal class Customer
    {

        public Customer(int id, string name, List<Invoice>? invoices = null)
        {
            Id = id;
            Name = name;
            Invoices = invoices ?? [];
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
