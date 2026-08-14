namespace Store.Application.DTOs.Products
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<int> SuppliersIds { get; set; } = [];
    }
}
