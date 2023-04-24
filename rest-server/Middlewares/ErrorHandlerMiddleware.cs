using Application.exceptions;
using Application.wrappers;
using System.Net;
using System.Text.Json;

namespace rest_server.Middlewares
{
    public class ErrorHandlerMiddleware: IMiddleware
    {
        
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";

                var result = new Response<string>()
                {
                    successed = false,
                    message = error?.Message
                };
                switch (error)
                {
                    case ApiException apiException:

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case ValidationException validationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result.errors = validationException._errors;
                        break;

                    case KeyNotFoundException keyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
