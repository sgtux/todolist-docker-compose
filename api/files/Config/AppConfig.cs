using System;

namespace Site.Config
{
    public class AppConfig
    {
        public string ConnectionString { get; private set; }

        public AppConfig()
        {
            ConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        }
    }
}