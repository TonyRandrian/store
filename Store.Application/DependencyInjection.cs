using Microsoft.Extensions.DependencyInjection;
using Store.Application.UseCases.InvoicesDetails;
using Store.Application.UseCases.Products;

namespace Store.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

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
