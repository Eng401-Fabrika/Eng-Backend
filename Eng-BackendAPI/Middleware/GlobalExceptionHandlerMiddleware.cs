using System.Net;
using System.Text.Json;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.DtoLayer.Common;

namespace Eng_BackendAPI.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            HttpException httpEx => new ApiResponse
            {
                Success = false,
                StatusCode = httpEx.StatusCode,
                Message = httpEx.Message,
                Errors = httpEx.Errors
            },
            _ => new ApiResponse
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = _env.IsDevelopment() 
                    ? exception.Message 
                    : "An internal server error occurred. Please try again later.",
                Errors = _env.IsDevelopment() && exception.StackTrace != null
                    ? new List<string> { exception.StackTrace }
                    : null
            }
        };

        context.Response.StatusCode = response.StatusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _env.IsDevelopment()
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}
