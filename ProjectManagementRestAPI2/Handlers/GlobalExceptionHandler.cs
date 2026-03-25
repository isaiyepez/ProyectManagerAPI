using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementRestAPI.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
      
            _logger.LogError(exception, "Error captured by the global controller: {Message}", exception.Message);

            (int statusCode, string title) = exception switch
            {
              
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),

                ArgumentException or ArgumentNullException => (StatusCodes.Status400BadRequest, "Invalid application data"),

                InvalidOperationException => (StatusCodes.Status400BadRequest, "Transaction not permitted by business rules"),

                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

   
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = httpContext.Request.Path
            };

            if (_env.IsDevelopment() || statusCode < 500)
            {
                problemDetails.Detail = exception.Message;
            }
            else
            {
                problemDetails.Detail = "An unexpected error has occurred. Please contact support.";
            }

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; 
        }
    }
}
