using Microsoft.Extensions.DependencyInjection;
using Store.Application.Interfaces.Repositories;
using Store.Application.Interfaces.Services;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Services;

namespace Store.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IInvoiceDetailsRepository, InvoiceDetailRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            return services;
        }
    }
}
