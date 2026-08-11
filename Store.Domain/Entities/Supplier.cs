namespace Store.Domain.Entities
{
    public class Supplier
    {

        public Supplier(string name, List<Product>? products = null)
        {
            Name = name;
            Products = products ?? [];
        }

        public Supplier()
        {
            Name = "";
            Products = [];
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
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a supplier with a null or empty name");
                field = value;
            }
        }

        public List<Product> Products
        {
            get;

            set;
        }
    }
}
