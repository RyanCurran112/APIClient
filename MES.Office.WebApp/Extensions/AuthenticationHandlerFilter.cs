using MES.Office.ApiClient.Handlers;
using MES.Office.ApiClient.Services;
using Microsoft.Extensions.Http;

namespace MES.Office.WebApp.Extensions;

/// <summary>
/// HTTP message handler filter that adds AuthenticationHandler to all HTTP clients
/// except for the ApiAuthenticationService's own client (to avoid circular dependency).
/// </summary>
public class AuthenticationHandlerFilter : IHttpMessageHandlerBuilderFilter
{
    private readonly IServiceProvider _serviceProvider;
    private IApiAuthenticationService? _cachedAuthService;
    private readonly object _lock = new();

    // The name used by AddHttpClient<IApiAuthenticationService, ApiAuthenticationService>
    private const string AuthServiceClientName = "IApiAuthenticationService";

    // Named clients that explicitly configure their own AuthenticationHandler
    private static readonly string[] ExplicitlyConfiguredClients = { "MesApi" };

    public AuthenticationHandlerFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        return builder =>
        {
            // Call the next filter first
            next(builder);

            // Skip the authentication service's own HttpClient to avoid circular dependency
            // The AuthenticationHandler needs IApiAuthenticationService, so if we add the handler
            // to the auth service's client, we get infinite recursion
            // Also skip clients that explicitly configure their own AuthenticationHandler
            var shouldSkip = builder.Name == null ||
                             builder.Name.Contains(AuthServiceClientName, StringComparison.OrdinalIgnoreCase) ||
                             ExplicitlyConfiguredClients.Contains(builder.Name, StringComparer.OrdinalIgnoreCase);

            if (!shouldSkip)
            {
                // Lazily resolve and cache the auth service to share token cache across all handlers
                var authService = GetOrCreateAuthService();
                if (authService != null)
                {
                    // Create a new handler instance for this client (DelegatingHandler can only be used once)
                    // but share the same IApiAuthenticationService instance to share the token cache
                    var authHandler = new AuthenticationHandler(authService);
                    builder.AdditionalHandlers.Add(authHandler);
                }
            }
        };
    }

    private IApiAuthenticationService? GetOrCreateAuthService()
    {
        if (_cachedAuthService != null)
            return _cachedAuthService;

        lock (_lock)
        {
            if (_cachedAuthService != null)
                return _cachedAuthService;

            // Resolve once and cache - all handlers share this instance and its token cache
            _cachedAuthService = _serviceProvider.GetService<IApiAuthenticationService>();
            return _cachedAuthService;
        }
    }
}
