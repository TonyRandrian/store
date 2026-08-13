namespace Store.Application.DTOs.Suppliers
{
    public class CreateSupplierRequest
    {
        public string Name { get; set; } = string.Empty;
        public List<int> ProductsIds { get; set; } = [];
    }
}
