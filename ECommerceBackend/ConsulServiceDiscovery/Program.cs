using Consul;
using ConsulServiceDiscovery.Configuration;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.UseUrls("http://localhost:2001");

builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(cfg =>
{
    cfg.Address = new Uri("http://localhost:8500");
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<ConsulServiceRegistration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/health", () => Results.Ok("Healthy"))
    .WithName("HealthCheck")
    .Produces(StatusCodes.Status200OK);

app.MapControllers();

// app.Lifetime.ApplicationStarted.Register(async () =>
// {
//     var server = app.Services.GetRequiredService<IServer>();
//     var feature = server.Features.Get<IServerAddressesFeature>();
//     var address = feature?.Addresses.FirstOrDefault();
//
//     if (address != null)
//     {
//         var uri = new Uri(address);
//         var consul = app.Services.GetRequiredService<IConsulClient>();
//
//         var registration = new AgentServiceRegistration
//         {
//             ID = $"agri-{uri.Port}",
//             Name = "agri",
//             Address = uri.Host,
//             Port = uri.Port,
//             Tags = new[] { "agri", "my-api" },
//             Check = new AgentServiceCheck
//             {
//                 HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/health",
//                 Interval = TimeSpan.FromSeconds(10),
//                 Timeout = TimeSpan.FromSeconds(5),
//                 DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
//             }
//         };
//
//         await consul.Agent.ServiceRegister(registration);
//         Console.WriteLine($" Registered to Consul: {registration.ID}");
//     }
// });

app.Run();
