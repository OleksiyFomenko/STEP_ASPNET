using ClientManagementAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problem));
        }
    }

    private static async Task HandleExceptionAsync(
    HttpContext context,
    Exception exception)
    {
        context.Response.Clear();

        var statusCode = exception switch
        {
            ClientNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                400 => "Bad Request",
                404 => "Not Found",
                _ => "Internal Server Error"
            },
            Detail = statusCode switch
            {
                400 => exception.Message,
                404 => exception.Message,
                _ => "An unexpected error occurred."
            },
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problem));
    }

}

