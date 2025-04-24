// using System.Text.Json;
// using Consul;
// using Ocelot.DependencyInjection;
// using Ocelot.Middleware;
// using Ocelot.Provider.Consul;
// using Configuration = OcelotGateway.Configuration;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Configuration Ocelot
// builder.Configuration
//     .SetBasePath(Directory.GetCurrentDirectory())
//     .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//
// // Add services to the container.
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddOcelot(builder.Configuration).AddConsul();
//
// var app = builder.Build();
//
// try
// {
//     // Load Ocelot
//     var configText = File.ReadAllText("ocelot.json");
//     var ocelotConfig = JsonSerializer.Deserialize<Configuration.OcelotConfig>(configText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//
//     if (ocelotConfig?.Routes != null && ocelotConfig.Routes.Any())
//     {
//         // Create Consul
//         var consulClient = new ConsulClient(c =>
//         {
//             c.Address = new Uri("http://127.0.0.1:8500");
//         });
//
//         // Register Service
//         foreach (var route in ocelotConfig.Routes)
//         {
//             if (route.DownstreamHostAndPorts == null || !route.DownstreamHostAndPorts.Any())
//             {
//                 Console.WriteLine($"Bỏ qua route: {route.ServiceName} - Không có DownstreamHostAndPorts");
//                 continue;
//             }
//             
//             var downstreamRoute = route.DownstreamHostAndPorts.FirstOrDefault();
//             if (downstreamRoute is null)
//             {
//                 Console.WriteLine($"Bỏ qua route: {route.ServiceName} - Không có DownstreamHostAndPort");
//                 continue;
//             }
//
//             var serviceId = $"{route.ServiceName}-{Guid.NewGuid()}";
//             var registration = new AgentServiceRegistration
//             {
//                 ID = serviceId,
//                 Name = route.ServiceName,
//                 Address = downstreamRoute.Host,
//                 Port = downstreamRoute.Port,
//                 Tags = new[] { "gateway-registered" },
//                 Check = new AgentServiceCheck
//                 {
//                     HTTP = $"http://{downstreamRoute.Host}:{downstreamRoute.Port}/health",
//                     Interval = TimeSpan.FromSeconds(10),
//                     Timeout = TimeSpan.FromSeconds(5),
//                     DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30),
//                 }
//             };
//             await consulClient.Agent.ServiceRegister(registration);
//             Console.WriteLine($"Đã đăng ký service: {serviceId} tại {downstreamRoute.Host}:{downstreamRoute.Port}");
//         }
//     }
//     else
//     {
//         Console.WriteLine("Cảnh báo: Không tìm thấy cấu hình Routes trong file ocelot.json");
//     }
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Lỗi khi xử lý cấu hình Ocelot: {ex.Message}");
// }
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseRouting();
//
// app.UseHttpsRedirection();
// app.UseAuthorization();
//
// // Đặt UseOcelot() sau UseAuthorization() và các middleware khác
// await app.UseOcelot();
//
// app.MapControllers();
//
// app.Run();
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

// Load Ocelot config
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Swagger + Controller + Consul
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot().AddConsul();

// CORS cho frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Swagger trong môi trường dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

await app.UseOcelot();

app.MapControllers();

app.Run();