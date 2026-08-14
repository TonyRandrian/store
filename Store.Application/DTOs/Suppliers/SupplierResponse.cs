using Store.Application.DTOs.Products;
using Store.Domain.Entities;

namespace Store.Application.DTOs.Suppliers
{
    public class SupplierResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public HashSet<ProductResponse> Products { get; set; } = [];


        public SupplierResponse(Guid id, string name, HashSet<ProductResponse> products)
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
