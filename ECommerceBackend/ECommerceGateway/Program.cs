using ECommerceGateway;
using Microsoft.Extensions.ServiceDiscovery;
using System.Net.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServiceDiscovery();
builder.Services.AddConfigurationServiceEndpointProvider();

builder.Services.AddHttpClient("UserServiceClient", client =>
{
    client.BaseAddress = new Uri("http://user-service");
}).AddServiceDiscovery();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

// ??c endpoints t? file
var endpointsJson = File.ReadAllText("endpoints.json");
var endpointConfig = JsonSerializer.Deserialize<EndpointConfig>(endpointsJson);

//EnpointMapper.MapEndpoints(app);

// Map endpoints ??ng
foreach (var endpoint in endpointConfig.Endpoints)
{
    if (endpoint.Method == "POST")
    {
        app.MapPost(endpoint.UpstreamPath, async (HttpContext context, IHttpClientFactory clientFactory) =>
        {
            var client = clientFactory.CreateClient("UserServiceClient");
            var requestBody = endpoint.RequestModel switch
            {
                "SignInModel" => await context.Request.ReadFromJsonAsync<SignInModel>(),
                _ => null
            };

            var response = await client.PostAsJsonAsync(endpoint.DownstreamPath, requestBody);
            response.EnsureSuccessStatusCode();

            return endpoint.ResponseType switch
            {
                "string" => Results.Ok(await response.Content.ReadAsStringAsync()),
                "bool" => Results.Ok(await response.Content.ReadFromJsonAsync<bool>()),
                "object" => Results.Ok(await response.Content.ReadFromJsonAsync<object>()),
                _ => Results.Ok()
            };
        });
    }
    else if (endpoint.Method == "GET")
    {
        app.MapGet(endpoint.UpstreamPath, async (IHttpClientFactory clientFactory) =>
        {
            var client = clientFactory.CreateClient("UserServiceClient");
            var response = await client.GetAsync(endpoint.DownstreamPath);
            response.EnsureSuccessStatusCode();

            return endpoint.ResponseType switch
            {
                "string" => Results.Ok(await response.Content.ReadAsStringAsync()),
                "bool" => Results.Ok(await response.Content.ReadFromJsonAsync<bool>()),
                "object" => Results.Ok(await response.Content.ReadFromJsonAsync<object>()),
                _ => Results.Ok()
            };
        });
    }
}

app.MapGet("/health", () => Results.Ok("Gateway Healthy"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public record EndpointConfig(EndpointEntry[] Endpoints);
public record EndpointEntry(string UpstreamPath, string DownstreamPath, string Method, string? RequestModel, string ResponseType);