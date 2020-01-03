using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gateway.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        { 
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An exception: {ex}");
                await SendResponse(httpContext);
            }
        }

        private static Task SendResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var result = new
            {
                context.Response.StatusCode,
                Message = "Internal Server Error"
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}