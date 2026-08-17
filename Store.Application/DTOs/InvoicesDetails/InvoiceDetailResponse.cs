using Store.Domain.Entities;

namespace Store.Application.DTOs.InvoicesDetails
{
    public class InvoiceDetailResponse(InvoiceDetail invoiceDetail)
    {
        public Guid Id { get; set; } = invoiceDetail.Id;
        public Guid InvoiceId { get; set; } = invoiceDetail.Invoice.Id;
        public Guid ProductId { get; set; } = invoiceDetail.Product.Id;
        public double Quantity { get; set; } = invoiceDetail.Quantity;
    }
}
