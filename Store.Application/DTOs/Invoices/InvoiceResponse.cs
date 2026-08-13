using Store.Application.DTOs.Customers;
using Store.Domain.Entities;

namespace Store.Application.DTOs.Invoices
{
    public class InvoiceResponse
    {
        public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public CustomerResponse Customer { get; set; }

        
        public InvoiceResponse(int id, string reference, decimal total, CustomerResponse customer)
        {
            Id = id;
            Reference = reference;
            Total = total;
            Customer = customer;
        }

        public InvoiceResponse(Invoice invoice)
        {
            Id = invoice.Id;
            Reference = invoice.Reference;
            Total = invoice.Total;
            Customer = new CustomerResponse(invoice.Customer);
        }
    }
}
