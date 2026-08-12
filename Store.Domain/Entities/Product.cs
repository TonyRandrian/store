namespace Store.Domain.Entities
{
    public class Product
    {
        private string name = null!;


        public Product(string name, decimal price, Category? category, List<Supplier>? suppliers = null)
        {
            Name = name;
            Category = category;
            Suppliers = suppliers ?? [];
            Price = price;
        }

        public Product()
        {
            Suppliers = [];
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
                ArgumentNullException.ThrowIfNullOrEmpty(value, "Cannot create a product with a null or empty name");
                name = value;
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
