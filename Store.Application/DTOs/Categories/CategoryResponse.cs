using Store.Domain.Entities;

namespace Store.Application.DTOs.Categories
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int32? ParentId { get; set; }
        public List<int> ProductsIds { get; set; }
        public List<int> ChildrenIds { get; set; }


        public CategoryResponse(
            int id, 
            string name, 
            Int32? parentId, 
            List<int> productsIds, 
            List<int> childrenIds)
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
