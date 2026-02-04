using System.Net.Http.Headers;
using MES.Office.ApiClient.Services;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Handlers
{
    /// <summary>
    /// DelegatingHandler that automatically adds JWT authentication to HTTP requests.
    /// Automatically obtains and refreshes tokens as needed.
    /// </summary>
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IApiAuthenticationService _authService;
        private readonly ILogger<AuthenticationHandler>? _logger;
        private readonly long? _defaultCompanyId;

        /// <summary>
        /// Creates a new AuthenticationHandler
        /// </summary>
        /// <param name="authService">The authentication service for obtaining tokens</param>
        /// <param name="logger">Optional logger</param>
        /// <param name="defaultCompanyId">Optional default company ID to use for all requests</param>
        public AuthenticationHandler(
            IApiAuthenticationService authService,
            ILogger<AuthenticationHandler>? logger = null,
            long? defaultCompanyId = null)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger;
            _defaultCompanyId = defaultCompanyId;
        }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Skip authentication for the token endpoint itself
            if (IsTokenEndpoint(request.RequestUri))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            try
            {
                // Get company ID from request header if specified, otherwise use default
                long? companyId = _defaultCompanyId;
                if (request.Headers.TryGetValues("X-Company-Id", out var companyIdValues))
                {
                    var companyIdString = companyIdValues.FirstOrDefault();
                    if (!string.IsNullOrEmpty(companyIdString) && long.TryParse(companyIdString, out var parsedId))
                    {
                        companyId = parsedId;
                    }
                }

                // Get token and add to request
                var token = await _authService.GetTokenAsync(companyId, cancellationToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                _logger?.LogDebug(
                    "Added JWT authentication to request | Method: {Method} | Uri: {Uri} | CompanyId: {CompanyId}",
                    request.Method,
                    request.RequestUri,
                    companyId);
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex,
                    "Failed to add authentication to request | Method: {Method} | Uri: {Uri}",
                    request.Method,
                    request.RequestUri);
                // Continue without authentication - the API will return 401 if required
            }

            var response = await base.SendAsync(request, cancellationToken);

            // If we get 401, clear cached token and retry once
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger?.LogWarning(
                    "Received 401 Unauthorized, clearing cached token and retrying | Uri: {Uri}",
                    request.RequestUri);

                _authService.ClearCachedToken();

                try
                {
                    var token = await _authService.GetTokenAsync(_defaultCompanyId, cancellationToken);
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    response = await base.SendAsync(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Failed to re-authenticate after 401 | Uri: {Uri}", request.RequestUri);
                }
            }

            return response;
        }

        private static bool IsTokenEndpoint(Uri? uri)
        {
            if (uri == null) return false;
            var path = uri.AbsolutePath.ToLowerInvariant();
            return path.Contains("/api/auth/token");
        }
    }
}
