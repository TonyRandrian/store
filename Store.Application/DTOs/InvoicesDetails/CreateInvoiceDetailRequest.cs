namespace Store.Application.DTOs.InvoicesDetails
{
    public class CreateInvoiceDetailRequest
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }
    }
}
