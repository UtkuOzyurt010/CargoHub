public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "API_KEY";
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals($"/api/{Globals.Version}", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key was not provided.");
            return;
        }

        var configuredApiKey = _configuration.GetValue<string>("ApiKey");
        if (!string.Equals(extractedApiKey, configuredApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = 403; // Forbidden
            await context.Response.WriteAsync("Unauthorized client.");
            return;
        }

        await _next(context); // Proceed to the next middleware or request handler
    }
}