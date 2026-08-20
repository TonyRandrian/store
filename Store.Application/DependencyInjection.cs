using Microsoft.Extensions.DependencyInjection;
using Store.Application.UseCases.Customers;
using Store.Application.UseCases.Invoices;
using Store.Application.UseCases.InvoicesDetails;
using Store.Application.UseCases.Products;
using Store.Application.UseCases.Suppliers;

namespace Store.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<GetInvoiceDetailUseCase>();
            services.AddScoped<CreateInvoiceDetailUseCase>();
            services.AddScoped<GetInvoicesDetailsUseCase>();
            services.AddScoped<DeleteInvoiceDetailUseCase>();
            services.AddScoped<UpdateInvoiceDetailUseCase>();

            services.AddScoped<CreateProductUseCase>();
            services.AddScoped<GetProductsUseCase>();
            services.AddScoped<GetProductUseCase>();
            services.AddScoped<DeleteProductUseCase>();
            services.AddScoped<UpdateProductUseCase>();
            services.AddScoped<GetProductCategoryUseCase>();

            return services;
        }
    }
}
