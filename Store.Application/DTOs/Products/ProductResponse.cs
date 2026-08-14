using Store.Application.DTOs.Categories;
using Store.Domain.Entities;

namespace Store.Application.DTOs.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse Category { get; set; }
        public HashSet<Guid> SuppliersIds { get; set; }


        public ProductResponse(Guid id, string name, decimal price, CategoryResponse category, HashSet<Guid> suppliersIds)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            SuppliersIds = suppliersIds;
        }

        public ProductResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = new CategoryResponse(product.Category!);
            SuppliersIds = [];

            foreach (Supplier supplier in product.Suppliers)
            {
                SuppliersIds.Add(supplier.Id);
            }
        }
    }
}
