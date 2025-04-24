namespace OcelotGateway.Configuration
{
    public class OcelotConfig
    {
        public List<Route> Routes { get; set; } = new List<Route>();
        public GlobalConfiguration GlobalConfiguration { get; set; } = new GlobalConfiguration();
    }

    public class Route
    {
        public string ServiceName { get; set; } = string.Empty;
        public string DownstreamPathTemplate { get; set; } = string.Empty;
        public string UpstreamPathTemplate { get; set; } = string.Empty;
        public List<string> UpstreamHttpMethod { get; set; } = new List<string>();
        public List<DownstreamHostAndPort> DownstreamHostAndPorts { get; set; } = new List<DownstreamHostAndPort>();
        public string DownstreamScheme { get; set; } = string.Empty;
        public Dictionary<string, string>? UpstreamHeaderTransform { get; set; } 
        public bool? RouteIsCaseSensitive { get; set; }
        public ServiceDiscoveryProvider? ServiceDiscoveryProvider { get; set; }
    }

    public class GlobalConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;
        public ServiceDiscoveryProvider? ServiceDiscoveryProvider { get; set; }
    }

    public class DownstreamHostAndPort
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }

    public class ServiceDiscoveryProvider
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Scheme { get; set; } = string.Empty;
    }
}