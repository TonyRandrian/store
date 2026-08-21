namespace Store.Domain.Entities
{
    public class Product
    {
        private string _name = null!;
        private Document? _document;
        private List<Image> _images;


        public Product(string name,
            decimal price,
            Category? category,
            List<Supplier>? suppliers = null,
            Document? document = null,
            List<Image>? images = null)
        {
            Name = name;
            Category = category;
            Suppliers = suppliers ?? [];
            Price = price;
            Document = document;
            Images = images ?? [];
        }

        public Product()
        {
            Suppliers = [];
        }

        public Guid Id
        {
            get;

            set;
        }

        public string Name
        {
            get => _name;

            set
            {
                ArgumentNullException.ThrowIfNullOrEmpty(value, "Cannot create a product with a null or empty name");
                _name = value;
            }
        }

        public Category Category
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

        public Document? Document
        {
            get => _document;
            set
            {
                _document = value;
            }
        }

        public List<Image> Images
        {
            get => _images;
            set
            {
                _images = value;
            }
        }

        public void AddImage(Image image)
        {
            Images.Add(image);
        }
    }
}
