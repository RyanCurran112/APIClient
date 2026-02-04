using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using MES.Office.WebAPI.Contracts.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Core
{
    /// <summary>
    /// Base class for all API clients providing common HTTP operations with Result pattern
    /// </summary>
    public abstract class ApiClientBase
    {
        protected readonly HttpClient HttpClient;
        protected readonly ILogger Logger;
        protected readonly IConfiguration Configuration;
        protected readonly string TestPrefix;

        protected abstract string BaseEndpoint { get; }

        protected ApiClientBase(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            Logger = logger;
            Configuration = configuration;
            TestPrefix = configuration?["DataSeeding:GlobalSettings:TestDataPrefix"] ?? "APITEST_";
        }

        #region HTTP Operations with Result Pattern

        /// <summary>
        /// GET request returning a single item wrapped in Result
        /// </summary>
        protected async Task<Result<T>> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("GET {Endpoint}", endpoint);

                var response = await HttpClient.GetAsync(endpoint, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("GET {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<T>(response.StatusCode, errorContent, endpoint);
                }

                var data = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
                Logger?.LogInformation("GET {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<T>.Success(data);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "GET {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<T>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// GET request returning a list wrapped in Result
        /// </summary>
        protected async Task<Result<IEnumerable<T>>> GetListAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("GET {Endpoint}", endpoint);

                var response = await HttpClient.GetAsync(endpoint, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("GET {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<IEnumerable<T>>(response.StatusCode, errorContent, endpoint);
                }

                var data = await response.Content.ReadFromJsonAsync<List<T>>(cancellationToken: cancellationToken) ?? new List<T>();
                Logger?.LogInformation("GET {Endpoint} returned {Count} items in {ElapsedMs}ms",
                    endpoint, data.Count, stopwatch.ElapsedMilliseconds);

                return Result<IEnumerable<T>>.Success(data);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "GET {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<IEnumerable<T>>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// POST request with body wrapped in Result
        /// </summary>
        protected async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("POST {Endpoint}", endpoint);

                var response = await HttpClient.PostAsJsonAsync(endpoint, data, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("POST {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<TResponse>(response.StatusCode, errorContent, endpoint);
                }

                var responseData = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
                Logger?.LogInformation("POST {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Success(responseData);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "POST {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// POST request returning list wrapped in Result
        /// </summary>
        protected async Task<Result<IEnumerable<T>>> PostListAsync<TRequest, T>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("POST {Endpoint}", endpoint);

                var response = await HttpClient.PostAsJsonAsync(endpoint, data, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("POST {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<IEnumerable<T>>(response.StatusCode, errorContent, endpoint);
                }

                var responseData = await response.Content.ReadFromJsonAsync<List<T>>(cancellationToken: cancellationToken) ?? new List<T>();
                Logger?.LogInformation("POST {Endpoint} returned {Count} items in {ElapsedMs}ms",
                    endpoint, responseData.Count, stopwatch.ElapsedMilliseconds);

                return Result<IEnumerable<T>>.Success(responseData);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "POST {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<IEnumerable<T>>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// POST request for operations without response data (returns Unit wrapped in Result)
        /// </summary>
        protected async Task<Result<Unit>> PostUnitAsync<TRequest>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("POST {Endpoint}", endpoint);

                var response = await HttpClient.PostAsJsonAsync(endpoint, data, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("POST {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<Unit>(response.StatusCode, errorContent, endpoint);
                }

                Logger?.LogInformation("POST {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "POST {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// PUT request with body wrapped in Result
        /// </summary>
        protected async Task<Result<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("PUT {Endpoint}", endpoint);

                var response = await HttpClient.PutAsJsonAsync(endpoint, data, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("PUT {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<TResponse>(response.StatusCode, errorContent, endpoint);
                }

                var responseData = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
                Logger?.LogInformation("PUT {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Success(responseData);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "PUT {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// DELETE request wrapped in Result
        /// </summary>
        protected async Task<Result<Unit>> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("DELETE {Endpoint}", endpoint);

                var response = await HttpClient.DeleteAsync(endpoint, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("DELETE {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<Unit>(response.StatusCode, errorContent, endpoint);
                }

                Logger?.LogInformation("DELETE {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "DELETE {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// DELETE request with body returning typed response wrapped in Result
        /// </summary>
        protected async Task<Result<TResponse>> DeleteAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("DELETE {Endpoint}", endpoint);

                var request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await HttpClient.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("DELETE {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<TResponse>(response.StatusCode, errorContent, endpoint);
                }

                var responseData = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
                Logger?.LogInformation("DELETE {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Success(responseData);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "DELETE {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// PATCH request for partial updates (returns Unit wrapped in Result)
        /// </summary>
        protected async Task<Result<Unit>> PatchAsync<TRequest>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("PATCH {Endpoint}", endpoint);

                var request = new HttpRequestMessage(HttpMethod.Patch, endpoint)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await HttpClient.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("PATCH {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<Unit>(response.StatusCode, errorContent, endpoint);
                }

                Logger?.LogInformation("PATCH {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "PATCH {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<Unit>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        /// <summary>
        /// PATCH request for partial updates returning typed response wrapped in Result
        /// </summary>
        protected async Task<Result<TResponse>> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Logger?.LogInformation("PATCH {Endpoint}", endpoint);

                var request = new HttpRequestMessage(HttpMethod.Patch, endpoint)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await HttpClient.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Logger?.LogWarning("PATCH {Endpoint} failed with {StatusCode}: {Error}",
                        endpoint, response.StatusCode, errorContent);

                    return CreateFailureResult<TResponse>(response.StatusCode, errorContent, endpoint);
                }

                var responseData = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
                Logger?.LogInformation("PATCH {Endpoint} completed in {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Success(responseData);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Logger?.LogError(ex, "PATCH {Endpoint} failed after {ElapsedMs}ms",
                    endpoint, stopwatch.ElapsedMilliseconds);

                return Result<TResponse>.Failure(
                    errorMessage: $"API request failed: {ex.Message}",
                    errorCode: ErrorCodes.INT_API_UNAVAILABLE,
                    source: "ApiClient",
                    context: new Dictionary<string, object>
                    {
                        ["Endpoint"] = endpoint,
                        ["ElapsedMs"] = stopwatch.ElapsedMilliseconds
                    });
            }
        }

        #endregion

        #region Test Data Management

        /// <summary>
        /// Filters test data from a collection based on test prefix
        /// </summary>
        protected List<T> FilterTestData<T>(IEnumerable<T> data, Func<T, string> codeSelector)
        {
            if (data == null) return new List<T>();
            return data.Where(x => codeSelector(x)?.StartsWith(TestPrefix, StringComparison.OrdinalIgnoreCase) == true).ToList();
        }

        /// <summary>
        /// Checks if an identifier indicates test data
        /// </summary>
        protected bool IsTestData(string identifier)
        {
            return !string.IsNullOrEmpty(identifier) &&
                   identifier.StartsWith(TestPrefix, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Error Handling Helpers

        /// <summary>
        /// Creates a failure result based on HTTP status code
        /// </summary>
        private Result<T> CreateFailureResult<T>(HttpStatusCode statusCode, string errorContent, string endpoint)
        {
            var (errorCode, errorMessage) = MapHttpStatusToError(statusCode, errorContent);

            return Result<T>.Failure(
                errorMessage: errorMessage,
                errorCode: errorCode,
                source: "ApiClient",
                context: new Dictionary<string, object>
                {
                    ["Endpoint"] = endpoint,
                    ["StatusCode"] = (int)statusCode,
                    ["ResponseContent"] = errorContent
                });
        }

        /// <summary>
        /// Maps HTTP status codes to domain error codes
        /// </summary>
        private (string ErrorCode, string ErrorMessage) MapHttpStatusToError(HttpStatusCode statusCode, string errorContent)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => (ErrorCodes.ValidationError, $"Bad request: {errorContent}"),
                HttpStatusCode.Unauthorized => (ErrorCodes.AUTH_FAILED, "Authentication required"),
                HttpStatusCode.Forbidden => (ErrorCodes.FORBIDDEN_RESOURCE, "Access forbidden"),
                HttpStatusCode.NotFound => (ErrorCodes.RESOURCE_NOT_FOUND, "Resource not found"),
                HttpStatusCode.Conflict => (ErrorCodes.Conflict, $"Conflict: {errorContent}"),
                HttpStatusCode.UnprocessableEntity => (ErrorCodes.VALIDATION_FAILED, $"Validation failed: {errorContent}"),
                HttpStatusCode.InternalServerError => (ErrorCodes.INT_EXTERNAL_SYNC_FAILED, $"Server error: {errorContent}"),
                HttpStatusCode.BadGateway => (ErrorCodes.INT_API_UNAVAILABLE, "API unavailable"),
                HttpStatusCode.ServiceUnavailable => (ErrorCodes.INT_API_UNAVAILABLE, "Service unavailable"),
                HttpStatusCode.GatewayTimeout => (ErrorCodes.INT_TIMEOUT, "Request timeout"),
                _ => (ErrorCodes.INTEGRATION_EXTERNAL_SERVICE_ERROR, $"HTTP {(int)statusCode}: {errorContent}")
            };
        }

        #endregion
    }
}
