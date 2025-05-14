using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repositories;
using OrderService.Repositories.Impl;
using OrderService.Services;
using OrderService.Models;
using VNPAY.NET;
using MassTransit;
using OrderService.Services.Impl;
using Shared.Messaging;



var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.

// Momo 
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoServiceImpl>();

// Data
builder.Services.AddDbContext<OrderDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
// Mapper
builder.Services.AddAutoMapper(typeof(OrderModel));

// HttpClient
builder.Services.AddHttpClient();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<IOrderRepository, OrderRepositoryImpl>();
builder.Services.AddScoped<IVnpay, Vnpay>();
//builder.Services.AddScoped<OrderQueryConsumer>();
builder.Services.AddScoped<CartConsumer>();

//// MassTransit RabbitMQ
builder.Services.AddMassTransit(x =>
{
    // Comment out CartConsumer to keep messages in cart-queue
    x.AddConsumer<CartConsumer>();
    //x.AddConsumer<OrderQueryConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        // Comment out cart-queue endpoint
        cfg.ReceiveEndpoint("cart-queue", e =>
        {
            //e.Durable = true;
            //e.Consumer<CartConsumer>(context);
            //e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
            e.ConfigureConsumer<CartConsumer>(context);
        });
        //cfg.ReceiveEndpoint("order-query-queue", e =>
        //{
        //    e.Durable = true;
        //    e.Consumer<OrderQueryConsumer>(context);
        //    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        //});
        //cfg.ReceiveEndpoint("order-error-queue", e =>
        //{
        //    e.ConfigureConsumeTopology = false;
        //});
    });
});

//builder.Services.AddRabbitMQ(builder.Configuration, x =>
//{
//    x.AddConsumer<CartConsumer>();
//    x.AddConsumer<OrderQueryConsumer>();
//});


builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});

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
