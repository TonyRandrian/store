namespace Store.Domain.Entities
{
    public class Product
    {

        public Product(string name, decimal price, Category? category, List<Supplier>? suppliers = null)
        {
            Name = name;
            Category = category;
            Suppliers = suppliers ?? [];
            Price = price;
        }

        public Product()
        {
            Name = "";
            Category = null;
            Suppliers = [];
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
