using Microsoft.AspNetCore.Hosting;

namespace Api.Config
{
    public class HostEnvironment
    {
        private readonly IWebHostEnvironment _env;

        public HostEnvironment(IWebHostEnvironment env) => _env = env;

        public bool IsDevelopment => _env.EnvironmentName == "Development";

        public bool IsProduction => _env.EnvironmentName == "Production";
    }
}