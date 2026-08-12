namespace Store.Application.DTOs.Categories
{
    public class UpdateCategoryRequest
    {
        public string Name { set; get; } = string.Empty;
        public Int32? ParentCategoryId { set; get; }
    }
}
