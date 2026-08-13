using Store.Domain.Entities;

namespace Store.Application.DTOs.Categories
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int32? ParentId { get; set; }
        public List<int> ProductsIds { get; set; }


        public CategoryResponse(int id, string name, Int32? parentId, List<int> productsIds)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            ProductsIds = productsIds;
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
        }
    }
}
