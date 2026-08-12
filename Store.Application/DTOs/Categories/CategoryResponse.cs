namespace Store.Application.DTOs.Categories
{
    public class CategoryResponse(int id, string name, Int32? parentId)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public Int32? parentId { get; set; } = parentId;
    }
}
