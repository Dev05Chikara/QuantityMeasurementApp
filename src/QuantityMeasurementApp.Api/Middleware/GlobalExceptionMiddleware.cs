using System.Text.Json;
using QuantityMeasurementApp.Business.Exceptions;

namespace QuantityMeasurementApp.Middleware
{
    /// <summary>
    /// Converts unhandled exceptions into consistent JSON error responses.
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                QuantityMeasurementException => StatusCodes.Status400BadRequest,
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var payload = JsonSerializer.Serialize(new
            {
                message = exception.Message,
                statusCode
            });

            return context.Response.WriteAsync(payload);
        }
    }
}



