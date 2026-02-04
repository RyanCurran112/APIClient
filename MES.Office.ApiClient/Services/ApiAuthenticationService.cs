using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Services
{
    /// <summary>
    /// Interface for API authentication service
    /// </summary>
    public interface IApiAuthenticationService
    {
        /// <summary>
        /// Gets a JWT token for API authentication
        /// </summary>
        /// <param name="companyId">Optional company ID for multi-tenancy</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>JWT token string</returns>
        Task<string> GetTokenAsync(long? companyId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the authorization header on an HttpClient
        /// </summary>
        void SetAuthorizationHeader(HttpClient client, string token);

        /// <summary>
        /// Clears the cached token (forces re-authentication on next request)
        /// </summary>
        void ClearCachedToken();
    }

    /// <summary>
    /// Service that handles API authentication by obtaining and caching JWT tokens.
    /// Used by API clients to authenticate with the MES Office API.
    /// </summary>
    public class ApiAuthenticationService : IApiAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiAuthenticationService>? _logger;
        private readonly SemaphoreSlim _tokenLock = new(1, 1);

        private string? _cachedToken;
        private DateTime _tokenExpiry = DateTime.MinValue;
        private long? _cachedCompanyId;

        // Buffer time before token expiry to refresh proactively
        private static readonly TimeSpan TokenRefreshBuffer = TimeSpan.FromMinutes(5);

        public ApiAuthenticationService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<ApiAuthenticationService>? logger = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<string> GetTokenAsync(long? companyId = null, CancellationToken cancellationToken = default)
        {
            // Fast path: return cached token if still valid and for same company
            if (IsCachedTokenValid(companyId))
            {
                return _cachedToken!;
            }

            // Acquire lock for token refresh
            await _tokenLock.WaitAsync(cancellationToken);
            try
            {
                // Double-check after acquiring lock
                if (IsCachedTokenValid(companyId))
                {
                    return _cachedToken!;
                }

                _logger?.LogDebug("Requesting new JWT token for API authentication | CompanyId: {CompanyId}", companyId);

                var token = await RequestNewTokenAsync(companyId, cancellationToken);
                return token;
            }
            finally
            {
                _tokenLock.Release();
            }
        }

        /// <inheritdoc />
        public void SetAuthorizationHeader(HttpClient client, string token)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Token cannot be null or empty", nameof(token));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <inheritdoc />
        public void ClearCachedToken()
        {
            _cachedToken = null;
            _tokenExpiry = DateTime.MinValue;
            _cachedCompanyId = null;
            _logger?.LogDebug("Cleared cached JWT token");
        }

        private bool IsCachedTokenValid(long? companyId)
        {
            return _cachedToken != null
                && DateTime.UtcNow < _tokenExpiry.Subtract(TokenRefreshBuffer)
                && _cachedCompanyId == companyId;
        }

        private async Task<string> RequestNewTokenAsync(long? companyId, CancellationToken cancellationToken)
        {
            var seedingUserId = _configuration["ApiSettings:SeedingUserId"] ?? "SEEDING_SERVICE";
            var seedingRoles = _configuration.GetSection("ApiSettings:SeedingRoles").Get<string[]>()
                ?? new[] { "SYSDEV" };

            var tokenRequest = new
            {
                userId = seedingUserId,
                userName = "API Client Service",
                roles = seedingRoles,
                companyId = companyId
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/auth/token", tokenRequest, cancellationToken);
                response.EnsureSuccessStatusCode();

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);

                if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.Token))
                {
                    throw new InvalidOperationException("Received empty token from authentication endpoint");
                }

                // Cache the token
                _cachedToken = tokenResponse.Token;
                _tokenExpiry = tokenResponse.ExpiresAt;
                _cachedCompanyId = companyId;

                _logger?.LogInformation(
                    "Obtained JWT token for API authentication | UserId: {UserId} | CompanyId: {CompanyId} | ExpiresAt: {ExpiresAt}",
                    seedingUserId,
                    companyId,
                    _tokenExpiry);

                return _cachedToken;
            }
            catch (HttpRequestException ex)
            {
                _logger?.LogError(ex, "Failed to obtain JWT token from API | Endpoint: /api/auth/token");
                throw new InvalidOperationException("Failed to authenticate with API. Ensure the API is running and accessible.", ex);
            }
        }

        /// <summary>
        /// Response from the token endpoint
        /// </summary>
        private class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
            public string TokenType { get; set; } = "Bearer";
            public string UserId { get; set; } = string.Empty;
            public long? CompanyId { get; set; }
        }
    }
}
