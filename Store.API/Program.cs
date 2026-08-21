using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Store.Application;
using Store.Application.Interfaces;
using Store.Application.Settings;
using Store.Infrastructure.Persistence;
using Store.Infrastructure.Repositories;
using Store.Infrastructure.Services;

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

builder.Services.AddApplicationDI();

builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();

builder.Services.Configure<FileStorageSettings>(
    builder.Configuration.GetSection("FileStorage"));

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
