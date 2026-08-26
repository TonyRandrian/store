using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Store.API.Commons;

namespace Store.API.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;


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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex is FluentValidation.ValidationException validationEx)
            {
                var errors = validationEx.Errors
                    .Select(e => e.ErrorMessage );

                var validationResponse = ApiResponse<object>.Error(
                    (int)HttpStatusCode.BadRequest,
                    "Validation failed",
                    errors);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
                return;
            }

            var (statusCode, message) = ex switch
            {
                KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(ex, "Unhandled exception");
            }

            var response = ApiResponse<object>.Error((int)statusCode, message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}