using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Infrastructure.Persistence
{
    public class StoreDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
        public DbSet<MyFile> Files => Set<MyFile>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Image> Images => Set<Image>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1-to-1 relationship: Product <-> Document
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Document)
                .WithOne(d => d.Product)
                .HasForeignKey<Document>(d => d.ProductId);
        }
    }
}