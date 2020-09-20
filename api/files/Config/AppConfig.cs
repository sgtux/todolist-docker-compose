using System;

namespace Api.Config
{
    public class AppConfig
    {
        public string ConnectionString { get; private set; }

        public string JwtKey { get; private set; }

        public AppConfig()
        {
            ConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            JwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        }
    }
}