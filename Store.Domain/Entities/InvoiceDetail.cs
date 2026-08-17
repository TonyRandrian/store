namespace Store.Domain.Entities
{
    public class InvoiceDetail
    {
        private Product product;
        private Invoice invoice;


        public InvoiceDetail(Invoice invoice, Product product, double quantity)
        {
            Invoice = invoice;
            Product = product;
            Quantity = quantity;
        }

        public InvoiceDetail()
        {
            Quantity = 0;
        }


        public Guid Id
        {
            get;

            set;
        }

        public Product Product
        {
            get => product;

            set
            {
                ArgumentNullException.ThrowIfNull(value, "Product should not be null");
                product = value;
            }
        }

        public Invoice Invoice
        {
            get => invoice;

            set
            {
                ArgumentNullException.ThrowIfNull(value, "Invoice details should be link to an invoice");
                invoice = value;
            }
        }

        public double Quantity
        {
            get;

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, "Quantity should be greater than 0");
                field = value;
            }
        }

    }
}
