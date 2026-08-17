namespace Store.Application.DTOs.InvoicesDetails
{
    public class UpdateInvoiceDetailRequest
    {
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
