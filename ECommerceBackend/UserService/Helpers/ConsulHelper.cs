using Consul;
namespace UserService.Helpers
{
    public static class ConsulHelper
    {
        public static async Task RegisterService(
            WebApplication app,
            string serviceName,
            string serviceAddress,
            int servicePort,
            string healthCheckUrl)
        {
            var consul = new ConsulClient(c => c.Address = new Uri("http://localhost:8500"));
            string serviceId = $"{serviceName}-dev";

            await consul.Agent.ServiceDeregister(serviceId);

            var registration = new AgentServiceRegistration
            {
                ID = serviceId,
                Name = serviceName,
                Address = serviceAddress, // Sử dụng tham số serviceAddress
                Port = servicePort,
                Tags = new[] { serviceName },
                Check = new AgentServiceCheck
                {
                    HTTP = healthCheckUrl, // Sử dụng tham số healthCheckUrl
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30)
                }
            };

            await consul.Agent.ServiceRegister(registration);

            app.Lifetime.ApplicationStopping.Register(() =>
            {
                consul.Agent.ServiceDeregister(serviceId).Wait();
            });
        }
    }
}