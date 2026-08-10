namespace Store.Domain.Entities
{
    public class Invoice
    {

        public Invoice(int id, string reference, Customer customer, decimal total)
        {
            Id = id;
            Reference = reference;
            Customer = customer;
            Total = total;
        }

        public int Id
        {
            get;

            set;
        }

        public string Reference
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create an invoice with a null or empty reference");
                field = value;
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
