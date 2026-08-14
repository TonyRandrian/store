namespace Store.Application.DTOs.Categories
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
    }
}
