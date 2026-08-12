using Store.Application.DTOs.Categories;
using Store.Domain.Entities;

namespace Store.Application.DTOs.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse Category { get; set; }


        public ProductResponse(int id, string name, decimal price, CategoryResponse category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public ProductResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = new CategoryResponse(product.Category!);
        }
    }
}
