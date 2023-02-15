using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Core;

namespace API.Middleware
{
    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /*
         * This method is call InvokeAsync not anything
         * when middleware runs will look for this name 
         */
        public async Task InvokeAsync(HttpContext context)
        {
            //This guys job is to catch exception
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // this will yield 500

                var response = _env.IsDevelopment()
                    ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace.ToString())
                    : new AppException(context.Response.StatusCode, "Internal Server Error");

                //outside of controller we've to specif Json as the application wont have control here 
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);

                //test this by deleting user which doesn't exist 

            }
        }

    }
}
