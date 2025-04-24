using System.Text.Json;
using Consul;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace OcelotGateway;

public class Consul
{
    public async Task RegisterServicesWithConsulAsync()
    {
        try
        {
            var configText = await File.ReadAllTextAsync("ocelot.json");
            var ocelotConfig = JsonSerializer.Deserialize<Configuration.OcelotConfig>(
                configText,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (ocelotConfig?.Routes is null || !ocelotConfig.Routes.Any())
            {
                Console.WriteLine("⚠️ Không tìm thấy cấu hình Routes trong ocelot.json");
                return;
            }

            using var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500");
            });

            foreach (var route in ocelotConfig.Routes)
            {
                if (route.DownstreamHostAndPorts is null || !route.DownstreamHostAndPorts.Any())
                {
                    Console.WriteLine($"⛔ Bỏ qua route '{route.ServiceName}': thiếu DownstreamHostAndPorts.");
                    continue;
                }

                var downstream = route.DownstreamHostAndPorts.First();
                if (downstream is null) continue;

                var serviceId = $"{route.ServiceName}-{Guid.NewGuid()}";

                var registration = new AgentServiceRegistration
                {
                    ID = serviceId,
                    Name = route.ServiceName,
                    Address = downstream.Host,
                    Port = downstream.Port,
                    Tags = new[] { "gateway-registered" },
                    Check = new AgentServiceCheck
                    {
                        HTTP = $"http://{downstream.Host}:{downstream.Port}/health",
                        Interval = TimeSpan.FromSeconds(10),
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30),
                    }
                };

                await consulClient.Agent.ServiceRegister(registration);
                Console.WriteLine($"✅ Đã đăng ký service: {serviceId} tại {downstream.Host}:{downstream.Port}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Lỗi khi đăng ký với Consul: {ex.Message}");
        }
    }
}