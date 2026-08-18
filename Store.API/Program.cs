using Store.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Infrastructure.Repositories;
using Store.Application.UseCases.Products;
using Store.Application.UseCases.Categories;
using Store.Application.UseCases.Customers;
using Store.Application.UseCases.Invoices;
using Store.Application.UseCases.Suppliers;
using Store.Application.UseCases.InvoicesDetails;
using Asp.Versioning;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // API v1 by default
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Controllers
builder.Services.AddControllers();

builder.Services.AddOpenApi();

// PostgreSQL + EF Core
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IInvoiceDetailsRepository, InvoiceDetailRepository>();

// UseCases
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<GetProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<GetProductCategoryUseCase>();

builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<GetCategoryProductsUseCase>();
builder.Services.AddScoped<GetCategoryChildrenUseCase>();

builder.Services.AddScoped<GetCustomerUseCase>();
builder.Services.AddScoped<GetCustomersUseCase>();
builder.Services.AddScoped<CreateCustomerUseCase>();
builder.Services.AddScoped<DeleteCustomerUseCase>();
builder.Services.AddScoped<UpdateCustomerUseCase>();

builder.Services.AddScoped<CreateInvoiceUseCase>();
builder.Services.AddScoped<GetInvoiceUseCase>();
builder.Services.AddScoped<GetInvoicesUseCase>();
builder.Services.AddScoped<DeleteInvoiceUseCase>();
builder.Services.AddScoped<UpdateInvoiceUseCase>();

builder.Services.AddScoped<GetSupplierUseCase>();
builder.Services.AddScoped<CreateSupplierUseCase>();
builder.Services.AddScoped<GetSuppliersUseCase>();
builder.Services.AddScoped<DeleteSupplierUseCase>();
builder.Services.AddScoped<UpdateSupplierUseCase>();

builder.Services.AddScoped<GetInvoiceDetailUseCase>();
builder.Services.AddScoped<CreateInvoiceDetailUseCase>();
builder.Services.AddScoped<GetInvoicesDetailsUseCase>();
builder.Services.AddScoped<DeleteInvoiceDetailUseCase>();
builder.Services.AddScoped<UpdateInvoiceDetailUseCase>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Store API",
        Version = "v1"
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Store API",
        Version ="v2"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Store API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Store API V2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
