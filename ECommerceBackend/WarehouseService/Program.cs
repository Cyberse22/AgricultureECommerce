using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using WarehouseService.Data.MongoDB;
using WarehouseService.Repositories.Impl;
using WarehouseService.Repositories;
using WarehouseService.Services.Impl;
using WarehouseService.Services;
using WarehouseService.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// MongoDB
builder.Services.AddSingleton<IMongoDbSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<MongoDbServices>();

// Product
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddSingleton(new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
)));

// Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();

// Inventory
builder.Services.AddScoped<IInventoryService, InventoryServiceImpl>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepositoryImpl>();

builder.Services.AddAutoMapper(typeof(Mapper));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
