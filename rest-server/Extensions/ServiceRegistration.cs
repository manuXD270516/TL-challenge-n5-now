using Microsoft.AspNetCore.Mvc;
using rest_server.Middlewares;
using Shared.config;

namespace rest_server.Extensions
{
    public static class ServiceRegistration
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void UseApiLoggerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiLoggerMiddleware>();
        }

        public static void UseKafkaIntegrationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<KafkaManagementMiddleware>();
        }

        public static IServiceCollection AddRestApiServices(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddScoped<ApiLoggerMiddleware>();
            services.AddScoped<KafkaManagementMiddleware>();

            return services;

            
        }

        
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
