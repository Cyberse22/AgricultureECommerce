
using Consul;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace ConsulServiceDiscovery.Configuration
{
    public class ConsulServiceRegistration : IHostedService
    {
        private readonly IConsulClient _consulClient;
        private readonly IServer _server;
        private string _regisetrationId;

        public ConsulServiceRegistration(IConsulClient consulClient, IServer server)
        {
            _consulClient = consulClient;
            _server = server;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        { 
            var address = _server.Features.Get<IServerAddressesFeature>()?.Addresses.First();
            var uri = new Uri(address);

            _regisetrationId = $"agri-{uri.Port}";

            var registration = new AgentServiceRegistration()
            {
                ID = _regisetrationId,
                Address = uri.Host,
                Port = uri.Port,
                Name = "agri",
                Tags = new[] { "agri", "my-api" },
                Check = new AgentServiceCheck
                {
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
                }
            };

            await _consulClient.Agent.ServiceRegister(registration, cancellationToken); 
            Console.WriteLine($"Service registered with ID: {_regisetrationId}");

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consulClient.Agent.ServiceDeregister(_regisetrationId, cancellationToken);
            Console.WriteLine($"Service deregistered with ID: {_regisetrationId}");
        }
    }
}
