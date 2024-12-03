using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using CargoHub.Models;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private const string ApiKeyHeaderName = "API_KEY";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory)
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

        var apiKey = context.Request.Headers["API_KEY"].ToString();

        // Validate the API key (check it against your database or configuration)
        var validApiKey = await ValidateApiKeyAsync(apiKey, serviceScopeFactory);

        if (!validApiKey)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API key");
            return;
        }

        await _next(context); // Proceed to the next middleware or request handler
    }

    private async Task<bool> ValidateApiKeyAsync(string apiKey, IServiceScopeFactory serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            bool found = await context.ApiKeyInfo.AnyAsync(k => k.ApiKey == Globals.EncryptApiKey(apiKey));
            return found;
        }
    }
}