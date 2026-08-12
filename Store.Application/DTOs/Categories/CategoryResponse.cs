using Store.Domain.Entities;

namespace Store.Application.DTOs.Categories
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int32? ParentId { get; set; }


        public CategoryResponse(int id, string name, Int32? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public CategoryResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.Parent?.Id;
        }
    }
}
