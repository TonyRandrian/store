using Store.Application.DTOs.Categories;

namespace Store.Application.DTOs.Products
{
    public class ProductResponse(int id, string name, decimal price, CategoryResponse category)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public decimal Price { get; set; } = price;
        public CategoryResponse Category { get; set; } = category;
    }
}
