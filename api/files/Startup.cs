using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Config;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public class Startup
    {
        private readonly HostEnvironment _hostEnvironment;

        private const string APP_CORS_POLICY = "CORS_POLICY";

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            _hostEnvironment = new HostEnvironment(env);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appConfig = new AppConfig();

            if (_hostEnvironment.IsProduction)
                services.AddHttpsRedirection(options => options.HttpsPort = 443);

            services.AddCors(options =>
            {
                options.AddPolicy(APP_CORS_POLICY, builder =>
                {
                    var origins = string.Join(";", appConfig.AllowedOrigins);
                    System.Console.WriteLine($"Using cors origins: {origins}");
                    builder.WithOrigins(appConfig.AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(appConfig.JwtKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();

            services.AddSingleton<AppConfig>(appConfig);
            services.AddScoped<TodoService>();
            services.AddScoped<UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(APP_CORS_POLICY);

            // app.UseHsts();

            app.UseAuthentication();
            app.UseAuthorization();

            if (_hostEnvironment.IsProduction)
                app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}