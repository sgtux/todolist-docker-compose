using System;

namespace Api.Config
{
    public class AppConfig
    {
        public string ConnectionString { get; private set; }

        public string JwtKey { get; private set; }

        public int TokenExpiryMinutes { get; private set; }

        public string[] AllowedOrigins { get; private set; }

        public AppConfig()
        {
            ConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            JwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            TokenExpiryMinutes = Convert.ToInt32(Environment.GetEnvironmentVariable("TOKEN_EXPIRY_MINUTES"));
            AllowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS").Split(";");
        }
    }
}