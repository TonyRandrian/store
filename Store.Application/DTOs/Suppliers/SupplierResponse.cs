using Store.Application.DTOs.Products;
using Store.Domain.Entities;

namespace Store.Application.DTOs.Suppliers
{
    public class SupplierResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductResponse> Products { get; set; } = [];


        public SupplierResponse(int id, string name, List<ProductResponse> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }

        public SupplierResponse(Supplier supplier)
        {
            Id = supplier.Id;
            Name = supplier.Name;
            Products = [];

            foreach (Product product in supplier.Products)
            {
                Products.Add(new ProductResponse(product));
            }
        }
    }
}
