namespace Store.Domain.Entities
{
    public class Category
    {

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
            Name = "";
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
            get;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a category with a null or empty name");
                field = value;
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
