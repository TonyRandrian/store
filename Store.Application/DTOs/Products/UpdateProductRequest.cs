namespace Store.Application.DTOs.Products
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid> SuppliersIds { get; set; } = [];
    }
}
