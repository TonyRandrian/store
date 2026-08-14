namespace Store.Application.DTOs.Invoices
{
    public class CreateInvoiceRequest
    {
        public string Reference { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public Guid CustomerId { get; set; }
    }
}
