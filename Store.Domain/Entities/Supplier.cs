namespace Store.Domain.Entities
{
    internal class Supplier
    {
        
        public Supplier(int id, string name, List<Product>? products)
        {
            Id = id;
            Name = name;
            Products = products ?? [];
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
