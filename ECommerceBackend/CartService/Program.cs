using CartService.Repositories;
using CartService.Repositories.Impl;
using CartService.Services;
using CartService.Services.Impl;
using StackExchange.Redis;
using Polly;
using MassTransit;
using Shared.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Lấy connection string từ appsettings.json
var redisConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__RedisConnection")
                            ?? builder.Configuration.GetConnectionString("RedisConnection");

// Cấu hình Redis với retry policy và connection options
var configurationOptions = new ConfigurationOptions
{
    EndPoints = { redisConnectionString },
    AbortOnConnectFail = false,  // Don't abort if initial connection fails
    ConnectTimeout = 5000,       // 5 seconds
    ConnectRetry = 5,            // Number of times to retry initial connection
    SyncTimeout = 5000,          // 5 seconds
    ReconnectRetryPolicy = new LinearRetry(1000), // Retry every second
};

var retryPolicy = Policy
    .Handle<RedisConnectionException>()
    .WaitAndRetry(
        retryCount: 5,
        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
        onRetry: (exception, timeSpan, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} connecting to Redis after {timeSpan.TotalSeconds} seconds");
        }
    );

ConnectionMultiplexer redis = null;
retryPolicy.Execute(() =>
{
    redis = ConnectionMultiplexer.Connect(configurationOptions);
    Console.WriteLine("Successfully connected to Redis");
});

// Add services to the container
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = configurationOptions;
});

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
builder.Services.AddScoped<ICartRepository, CartRepositoryImpl>();
builder.Services.AddScoped<ICartService, CartServiceImpl>();
builder.Services.AddScoped<OrderCreatedConsumer>();

//// MassTransit RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        //cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddRabbitMQ(builder.Configuration);

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Debug);
});

// CORS
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();