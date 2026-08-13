namespace Store.Application.DTOs.Invoices
{
    public class UpdateInvoiceRequest
    {
        public string Reference { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
    }
}
