namespace Store.Domain.Entities
{
    internal class Category
    {

        public Category(
            int id,
            string name,
            Category? parent = null,
            List<Product>? products = null,
            List<Category>? children = null)
        {
            Id = id;
            Name = name;
            Parent = parent;
            Products = products ?? [];
            Children = children ?? [];
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
