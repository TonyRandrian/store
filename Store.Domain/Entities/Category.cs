namespace Store.Domain.Entities
{
    public class Category
    {
        private string name = null!;


        public Category(
            string name,
            Category? parent = null,
            List<Product>? products = null,
            List<Category>? children = null)
        {
            Name = name;
            Parent = parent;
            Products = products ?? [];
            Children = children ?? [];
        }

        public Category()
        {
            Parent = null;
            Products = [];
            Children = [];
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
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a category with a null or empty name");
                name = value;
            }
        }

        public Category? Parent
        {
            get;

            set;
        }

        public List<Product> Products
        {
            get;

            set;
        }

        public List<Category> Children
        {
            get;

            set;
        }
    }
}
