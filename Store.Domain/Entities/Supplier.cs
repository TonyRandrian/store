namespace Store.Domain.Entities
{
    public class Supplier
    {
        private string name;

        public Supplier(string name, List<Product>? products = null)
        {
            Name = name;
            Products = products ?? [];
        }

        public Supplier()
        {
            Products = [];
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
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a supplier with a null or empty name");
                name = value;
            }
        }

        public List<Product> Products
        {
            get;

            set;
        }
    }
}
