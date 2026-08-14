namespace Store.Application.DTOs.Categories
{
    public class UpdateCategoryRequest
    {
        public string Name { set; get; } = string.Empty;
        public Guid? ParentCategoryId { set; get; }
    }
}
