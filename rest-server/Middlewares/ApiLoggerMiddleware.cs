using Serilog;
using Shared.config;
using ISeriLogger = Serilog.ILogger;

namespace rest_server.Middlewares
{
    public class ApiLoggerMiddleware : IMiddleware
    { 
        private readonly ISeriLogger _logger = Log.ForContext(typeof(ApiLoggerMiddleware));

        public ApiLoggerMiddleware()
        {
            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (ProfileLogger.Profile($"Time for execution Endpoint {context.Request.Method} {context.Request.Path}", "TIME EXECUTED"))
            {
                _logger.Information($"INIT ENDPOINT API REQUEST {context.Request.Method} {context.Request.Path}");
                
                await next(context);

                _logger.Information($"END ENDPOINT API REQUEST {context.Request.Method} {context.Request.Path}");

            }
        }
    }
}
