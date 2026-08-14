namespace Store.Application.DTOs.Suppliers
{
    public class UpdateSupplierRequest
    {
        public string Name { get; set; } = string.Empty;
        public HashSet<Guid> ProductsIds { get; set; } = [];
    }
}
