namespace TestCarAPI.Models
{
    public class ServiceConfiguration
    {
        public class Service
        {
            public string Name { get; set; } = string.Empty;
            public string BaseUri { get; set; } = string.Empty;
            public string? FunctionAuthCode { get; set;}
        }

        public List<Service> Services { get; set; } = new();
    }
}
