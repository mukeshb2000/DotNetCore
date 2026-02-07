using System.Diagnostics;

namespace ShopManagement.API.Middleware;

/// <summary>
/// Request logging middleware demonstrating custom middleware implementation
/// Logs request details and response times for monitoring and debugging
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestId = Guid.NewGuid().ToString("N")[..8];

        // Log request start
        _logger.LogInformation(
            "[{RequestId}] Starting {Method} {Path} at {Timestamp}",
            requestId,
            context.Request.Method,
            context.Request.Path,
            DateTime.UtcNow);

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            // Log request completion
            _logger.LogInformation(
                "[{RequestId}] Completed {Method} {Path} with {StatusCode} in {ElapsedMs}ms",
                requestId,
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}