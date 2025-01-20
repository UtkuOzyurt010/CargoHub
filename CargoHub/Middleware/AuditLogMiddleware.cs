public class AuditLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _targetDirectory;

    public AuditLogMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _targetDirectory = Path.Combine(environment.ContentRootPath, "AuditLogs");
    }

    public async Task Invoke(HttpContext context)
    {
        var apiKey = context.Request.Headers["API_KEY"].FirstOrDefault();
        var endpoint = context.Request.Path;
        var method = context.Request.Method;
        var timestamp = DateTime.Now;

        string message = $"API Key: {apiKey}, Endpoint: {endpoint}, Method: {method}, Timestamp: {timestamp}";
        var logFilePath = Path.Combine(_targetDirectory, $"auditlog.txt");
        await File.AppendAllTextAsync(logFilePath, message + Environment.NewLine);

        await _next(context);
    }
}