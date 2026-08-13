namespace Store.Domain.Entities
{
    public class Invoice
    {
        private string reference;

        public Invoice(string reference, Customer customer, decimal total)
        {
            Reference = reference;
            Customer = customer;
            Total = total;
        }

        public Invoice()
        {
            Total = 0;
        }

        public int Id
        {
            get;

            set;
        }

        public string Reference
        {
            get => reference;

            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create an invoice with a null or empty reference");
                reference = value;
            }
        }

        public Customer Customer
        {
            get;

            set
            {
                ArgumentNullException.ThrowIfNull(value, "A customer is mandatory to create an invoice");
                field = value;
            }
        }

        public decimal Total
        {
            get;

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, "The total price should be positive");
                field = value;
            }
        }
    }
}
