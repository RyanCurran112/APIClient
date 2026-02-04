using Microsoft.JSInterop;

namespace MES.Office.WebApp.Services
{
    /// <summary>
    /// Service for managing seeding configuration preferences.
    /// Stores preferences in browser localStorage for persistence.
    /// </summary>
    public interface ISeedingConfigurationService
    {
        /// <summary>
        /// Gets the current seeding configuration.
        /// </summary>
        Task<SeedingPreferences> GetConfigurationAsync();

        /// <summary>
        /// Saves the seeding configuration.
        /// </summary>
        Task SaveConfigurationAsync(SeedingPreferences config);

        /// <summary>
        /// Resets configuration to defaults.
        /// </summary>
        Task ResetToDefaultsAsync();

        /// <summary>
        /// Tests the connection to the API.
        /// </summary>
        Task<ApiConnectionStatus> TestConnectionAsync();

        /// <summary>
        /// Gets the API base URL from appsettings.
        /// </summary>
        string GetApiBaseUrl();
    }

    /// <summary>
    /// Seeding configuration model.
    /// </summary>
    public class SeedingPreferences
    {
        /// <summary>
        /// Default batch size for seeding operations.
        /// </summary>
        public int DefaultBatchSize { get; set; } = 100;

        /// <summary>
        /// Timeout in seconds for API operations.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Enable parallel seeding for independent entities.
        /// </summary>
        public bool EnableParallelSeeding { get; set; } = false;

        /// <summary>
        /// Skip entities that already have data in the database.
        /// </summary>
        public bool SkipExistingData { get; set; } = true;

        /// <summary>
        /// Enable verbose logging during seeding operations.
        /// </summary>
        public bool VerboseLogging { get; set; } = false;

        /// <summary>
        /// Selected data source (if multiple exist).
        /// </summary>
        public string SelectedDataSource { get; set; } = "Default";

        /// <summary>
        /// Order entities by dependencies during seeding.
        /// </summary>
        public bool RespectDependencyOrder { get; set; } = true;

        /// <summary>
        /// Continue seeding even if some entities fail.
        /// </summary>
        public bool ContinueOnError { get; set; } = false;

        /// <summary>
        /// Maximum concurrent operations when parallel seeding is enabled.
        /// </summary>
        public int MaxParallelOperations { get; set; } = 4;
    }

    /// <summary>
    /// API connection status model.
    /// </summary>
    public class ApiConnectionStatus
    {
        /// <summary>
        /// Whether the API is reachable.
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Status message describing the connection state.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response time in milliseconds.
        /// </summary>
        public long ResponseTimeMs { get; set; }

        /// <summary>
        /// Last successful connection timestamp.
        /// </summary>
        public DateTime? LastConnected { get; set; }

        /// <summary>
        /// API version if available.
        /// </summary>
        public string? ApiVersion { get; set; }
    }

    /// <summary>
    /// Implementation of seeding configuration service.
    /// </summary>
    public class SeedingConfigurationService : ISeedingConfigurationService
    {
        private const string StorageKey = "mes-seeding-configuration";
        private const string LastConnectedKey = "mes-api-last-connected";

        private readonly IJSRuntime _jsRuntime;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SeedingConfigurationService> _logger;

        public SeedingConfigurationService(
            IJSRuntime jsRuntime,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<SeedingConfigurationService> logger)
        {
            _jsRuntime = jsRuntime;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <inheritdoc />
        public string GetApiBaseUrl()
        {
            return _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5002";
        }

        /// <inheritdoc />
        public async Task<SeedingPreferences> GetConfigurationAsync()
        {
            try
            {
                var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
                if (!string.IsNullOrEmpty(json))
                {
                    var config = System.Text.Json.JsonSerializer.Deserialize<SeedingPreferences>(json);
                    if (config != null)
                    {
                        return config;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to load seeding configuration from localStorage, using defaults");
            }

            return new SeedingPreferences();
        }

        /// <inheritdoc />
        public async Task SaveConfigurationAsync(SeedingPreferences config)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(config);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
                _logger.LogDebug("Seeding configuration saved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save seeding configuration to localStorage");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task ResetToDefaultsAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
                _logger.LogDebug("Seeding configuration reset to defaults");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reset seeding configuration");
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<ApiConnectionStatus> TestConnectionAsync()
        {
            var status = new ApiConnectionStatus();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                var client = _httpClientFactory.CreateClient("MesApi");
                client.Timeout = TimeSpan.FromSeconds(10);

                // Try to reach the health endpoint or a simple GET endpoint
                var response = await client.GetAsync("api/v1/health");
                stopwatch.Stop();

                if (response.IsSuccessStatusCode)
                {
                    status.IsConnected = true;
                    status.Message = "Connected successfully";
                    status.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                    status.LastConnected = DateTime.UtcNow;

                    // Try to get version from response headers
                    if (response.Headers.TryGetValues("X-Api-Version", out var versions))
                    {
                        status.ApiVersion = versions.FirstOrDefault();
                    }

                    // Save last connected timestamp
                    await SaveLastConnectedAsync(status.LastConnected.Value);
                }
                else
                {
                    status.IsConnected = false;
                    status.Message = $"API returned {(int)response.StatusCode} {response.ReasonPhrase}";
                    status.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                    status.LastConnected = await GetLastConnectedAsync();
                }
            }
            catch (TaskCanceledException)
            {
                stopwatch.Stop();
                status.IsConnected = false;
                status.Message = "Connection timed out";
                status.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                status.LastConnected = await GetLastConnectedAsync();
            }
            catch (HttpRequestException ex)
            {
                stopwatch.Stop();
                status.IsConnected = false;
                status.Message = $"Connection failed: {ex.Message}";
                status.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                status.LastConnected = await GetLastConnectedAsync();
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                status.IsConnected = false;
                status.Message = $"Unexpected error: {ex.Message}";
                status.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                status.LastConnected = await GetLastConnectedAsync();
                _logger.LogError(ex, "Failed to test API connection");
            }

            return status;
        }

        private async Task SaveLastConnectedAsync(DateTime timestamp)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LastConnectedKey, timestamp.ToString("o"));
            }
            catch
            {
                // Ignore localStorage errors
            }
        }

        private async Task<DateTime?> GetLastConnectedAsync()
        {
            try
            {
                var value = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", LastConnectedKey);
                if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out var timestamp))
                {
                    return timestamp;
                }
            }
            catch
            {
                // Ignore localStorage errors
            }

            return null;
        }
    }
}
