using Store.Domain.Entities;

namespace Store.Application.DTOs.Categories
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public HashSet<Guid> ProductsIds { get; set; }
        public HashSet<Guid> ChildrenIds { get; set; }


        public CategoryResponse(
            Guid id, 
            string name, 
            Guid? parentId,
            HashSet<Guid> productsIds,
            HashSet<Guid> childrenIds)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            ProductsIds = productsIds;
            ChildrenIds = childrenIds;
        }

        public CategoryResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.Parent?.Id;

            ProductsIds = [];
            foreach (Product product in category.Products)
            {
                ProductsIds.Add(product.Id);
            }

            ChildrenIds = [];
            foreach (Category c in category.Children)
            {
                ChildrenIds.Add(c.Id);
            }
        }
    }
}
