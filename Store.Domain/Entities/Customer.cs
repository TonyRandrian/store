namespace Store.Domain.Entities
{
    public class Customer
    {
        private string name;

        public Customer(string name, List<Invoice>? invoices = null)
        {
            Name = name;
            Invoices = invoices ?? [];
        }

        public Customer()
        {
            Invoices = [];
        }

        public int Id
        {
            get;

            set;
        }

        public string Name
        {
            get => name;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a customer with a null or empty name");
                name = value;
            }
        }

        public List<Invoice> Invoices
        {
            get;

            set;
        }
    }
}
