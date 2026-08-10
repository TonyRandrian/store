namespace Store.Domain.Entities
{
    internal class Category
    {

        public Category(int id, string name, Category? parentCategory)
        {
            Id = id;
            Name = name;
            ParentCategory = parentCategory;
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
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Can't create a product with a null or empty name")
                field = value;
            }
        }

        public Category? ParentCategory
        {
            get;

            set;
        }
    }
}
