using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repositories;
using OrderService.Repositories.Impl;
using OrderService.Services;
using OrderService.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Momo 
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoServiceImpl>();

// Data
builder.Services.AddDbContext<OrderDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
// Mapper
builder.Services.AddAutoMapper(typeof(OrderModel));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<IOrderRepository, OrderRepositoryImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
