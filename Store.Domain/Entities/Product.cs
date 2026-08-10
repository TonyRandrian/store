namespace Store.Domain.Entities
{
    internal class Product
    {

        public Product(int id, string name, Category? category, List<Supplier>? suppliers = null)
        {
            Id = id;
            Name = name;
            Category = category;
            Suppliers = suppliers ?? [];
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
                ArgumentNullException.ThrowIfNullOrEmpty(value, "Cannot create a product with a null or empty name");
                field = value;
            }
        }

        public Category? Category
        {
            get;

            set;
        }

        public decimal Price
        {
            get;

            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, 0, "Product's price should not be negative");
                field = value;
            }
        }

        public List<Supplier> Suppliers
        {
            get;

            set;
        }
    }
}
