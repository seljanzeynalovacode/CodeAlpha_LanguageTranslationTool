using System.Text.Json;

namespace TranslationTool.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                error = ex.Message,
                type = ex.GetType().Name,
                innerError = ex.InnerException?.Message,
                stackTrace = ex.StackTrace
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var jsonResult = JsonSerializer.Serialize(errorResponse, options);
            await context.Response.WriteAsync(jsonResult);
        }
    }
}