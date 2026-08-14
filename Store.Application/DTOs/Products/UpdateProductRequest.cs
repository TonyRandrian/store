namespace Store.Application.DTOs.Products
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public List<int> SuppliersIds { get; set; } = [];
    }
}
