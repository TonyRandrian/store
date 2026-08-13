using Store.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces;
using Store.Infrastructure.Repositories;
using Store.Application.UseCases.Products;
using Store.Application.UseCases.Categories;
using Store.Application.UseCases.Customers;
using Store.Application.UseCases.Invoices;
using Store.Application.UseCases.Suppliers;

var builder = WebApplication.CreateBuilder(args);

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

// UseCases
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetProductsUseCase>();
builder.Services.AddScoped<GetProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();

builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();

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

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
