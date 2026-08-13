namespace Store.Application.DTOs.Categories
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public Int32? ParentCategoryId { get; set; }
    }
}
