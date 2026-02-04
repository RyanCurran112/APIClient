using MES.Office.ApiClient.Extensions;
using MES.Office.ApiClient.Handlers;
using MES.Office.ApiClient.Services;
using MES.Office.Application.Common.Logging;
using MES.Office.Application.DataSeeding;
using MES.Office.Application.Interfaces.Services;
using MES.Office.Application.Services.Testing;
using MES.Office.WebApp.Components;
using MES.Office.WebApp.Extensions;
using MES.Office.WebApp.Services;
using Microsoft.Extensions.Http;
using MudBlazor.Services;
using Serilog;

// Application name for logging
const string ApplicationName = "WebApp";

var builder = WebApplication.CreateBuilder(args);

// Disable service validation on build - WebApp only uses subset of services
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = false;
    options.ValidateOnBuild = false;
});

// Create bootstrap logger for startup logging
LoggingExtensions.CreateBootstrapLogger(
    builder.Environment.ContentRootPath,
    ApplicationName,
    builder.Configuration);

try
{
    Log.Information("Starting MES Office WebApp for API Seeding and Testing");
    Log.Information("Environment: {Environment}", builder.Environment.EnvironmentName);

    // Configure Serilog with shared MES logging configuration
    builder.Host.ConfigureMesLogging(builder.Configuration, ApplicationName);

    // Add Blazor Server services with Interactive Server Components
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    // Add MudBlazor services
    builder.Services.AddMudServices();

    // Keep Razor Pages for backward compatibility during migration
    builder.Services.AddRazorPages();

    // Configure API base URL
    var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7272";
    Log.Information("API Base URL: {ApiBaseUrl}", apiBaseUrl);

    // Register authentication services for API access with SYSDEV role
    // NOTE: This client is registered FIRST without the AuthenticationHandler
    // to avoid circular dependency (AuthenticationHandler needs IApiAuthenticationService)
    builder.Services.AddHttpClient<IApiAuthenticationService, ApiAuthenticationService>(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
    });

    // Add a filter that automatically adds AuthenticationHandler to all HttpClients
    // EXCEPT for the ApiAuthenticationService's own client (to avoid circular dependency)
    builder.Services.AddSingleton<IHttpMessageHandlerBuilderFilter, AuthenticationHandlerFilter>();

    // Register all MES API clients from shared library
    // These are used by the DataSeeding framework to seed data via API endpoints
    builder.Services.AddMesApiClients(apiBaseUrl);

    // Register DataSeeding framework
    // This includes IDataSeedingService, ISeeding_Service (ApiBasedSeedingService),
    // and all entity seeding strategies that use API clients
    builder.Services.AddDataSeedingFramework(builder.Configuration);

    // Add caching services (includes MemoryCache, DistributedMemoryCache, and HybridCacheService)
    // Required for API clients that use caching
    builder.Services.AddCachingServices(builder.Configuration);

    // Register seeding configuration service for managing seeding preferences
    builder.Services.AddScoped<ISeedingConfigurationService, SeedingConfigurationService>();

    // Configure HttpClient for API testing
    builder.Services.AddHttpClient<IEndpointTesting_Service, EndpointTestingService>(client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.Timeout = TimeSpan.FromSeconds(30);
    });

    // Configure named HttpClient for direct API calls (used by Entity detail page)
    // Uses AddHttpMessageHandler to properly inject AuthenticationHandler with DI
    builder.Services.AddHttpClient("MesApi", client =>
    {
        client.BaseAddress = new Uri(apiBaseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.Timeout = TimeSpan.FromSeconds(30);
    })
    .AddHttpMessageHandler(sp => new AuthenticationHandler(sp.GetRequiredService<IApiAuthenticationService>()));

    // Add session for UI state management
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Add authorization services for seeding pages
    builder.Services.AddAuthorization(options =>
    {
        // Policy for general seeding operations
        options.AddPolicy("SeedingAdminPolicy", policy =>
            policy.RequireAssertion(_ => true)); // Allow all for development - configure properly for production

        // Policy for destructive operations (clear data)
        options.AddPolicy("DataDestructionPolicy", policy =>
            policy.RequireAssertion(_ => true)); // Allow all for development - configure properly for production
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    // Use MapStaticAssets for .NET 9+ optimized static file serving (includes _content from NuGet packages)
    app.MapStaticAssets();

    app.UseRouting();
    app.UseAntiforgery();
    app.UseAuthorization();
    app.UseSession();

    // Add Serilog request logging
    app.UseSerilogRequestLogging();

    // Map Blazor components with Interactive Server mode
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .WithStaticAssets();

    // Keep Razor Pages mapped for backward compatibility during migration
    app.MapRazorPages();

    Log.Information("MES Office WebApp configured successfully (Blazor Server with MudBlazor)");
    Log.Information("WebApp uses API clients for all seeding operations - no direct database access");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
