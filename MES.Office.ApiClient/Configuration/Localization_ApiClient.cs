using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for WMS Localization operations
    /// Provides methods for retrieving localization bundles and language options
    /// </summary>
    public class Localization_ApiClient : ApiClientBase, ILocalization_ApiClient
    {
        protected override string BaseEndpoint => "api/configuration/localization";

        public Localization_ApiClient(
            HttpClient httpClient,
            ILogger<Localization_ApiClient> logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <inheritdoc />
        public async Task<Result<WMS_LocalizationBundle_DTO>> GetBundleAsync(
            string languageCode,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                languageCode = "en";
            }

            return await GetAsync<WMS_LocalizationBundle_DTO>(
                $"{BaseEndpoint}/bundle/{languageCode}",
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Result<string[]>> GetSupportedLanguagesAsync(
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<string[]>(
                $"{BaseEndpoint}/languages/supported",
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Result<IEnumerable<WMS_LanguageOption_DTO>>> GetAvailableLanguagesAsync(
            CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<LanguageInfo_DTO[]>(
                $"{BaseEndpoint}/languages",
                cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<IEnumerable<WMS_LanguageOption_DTO>>.Failure(
                    result.ErrorCode,
                    result.ErrorMessage);
            }

            // Map LanguageInfo_DTO to WMS_LanguageOption_DTO
            var languageOptions = result.Value?.Select(lang => new WMS_LanguageOption_DTO
            {
                LanguageCode = lang.LanguageCode,
                LanguageName = lang.LanguageName,
                NativeName = lang.NativeName,
                FlagIcon = lang.FlagIcon,
                IsDefault = lang.IsDefault
            }) ?? Enumerable.Empty<WMS_LanguageOption_DTO>();

            return Result<IEnumerable<WMS_LanguageOption_DTO>>.Success(languageOptions);
        }

        /// <inheritdoc />
        public async Task<Result<WMS_LanguageOption_DTO>> GetDefaultLanguageAsync(
            CancellationToken cancellationToken = default)
        {
            var result = await GetAsync<LanguageInfo_DTO>(
                $"{BaseEndpoint}/languages/default",
                cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<WMS_LanguageOption_DTO>.Failure(
                    result.ErrorCode,
                    result.ErrorMessage);
            }

            if (result.Value == null)
            {
                // Return English as fallback if no default configured
                return Result<WMS_LanguageOption_DTO>.Success(new WMS_LanguageOption_DTO
                {
                    LanguageCode = "en",
                    LanguageName = "English",
                    NativeName = "English",
                    IsDefault = true
                });
            }

            var languageOption = new WMS_LanguageOption_DTO
            {
                LanguageCode = result.Value.LanguageCode,
                LanguageName = result.Value.LanguageName,
                NativeName = result.Value.NativeName,
                FlagIcon = result.Value.FlagIcon,
                IsDefault = result.Value.IsDefault
            };

            return Result<WMS_LanguageOption_DTO>.Success(languageOption);
        }

        /// <inheritdoc />
        public async Task<Result<string>> GetMessageAsync(
            string key,
            string languageCode,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return Result<string>.Failure(
                    ErrorCodes.ValidationError,
                    "Message key is required");
            }

            languageCode ??= "en";

            return await GetAsync<string>(
                $"{BaseEndpoint}/message/{Uri.EscapeDataString(key)}/language/{languageCode}",
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Result<WMS_TranslateMessages_Response_DTO>> GetMessagesAsync(
            WMS_TranslateMessages_Request_DTO request,
            CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return Result<WMS_TranslateMessages_Response_DTO>.Failure(
                    ErrorCodes.ValidationError,
                    "Request is required");
            }

            if (request.MessageKeys == null || !request.MessageKeys.Any())
            {
                return Result<WMS_TranslateMessages_Response_DTO>.Success(
                    new WMS_TranslateMessages_Response_DTO
                    {
                        LanguageCode = request.LanguageCode,
                        Translations = new Dictionary<string, string>(),
                        MissingKeys = new List<string>()
                    });
            }

            // Build query string for batch retrieval
            var keysParam = string.Join(",", request.MessageKeys.Select(Uri.EscapeDataString));
            return await GetAsync<WMS_TranslateMessages_Response_DTO>(
                $"{BaseEndpoint}/messages?keys={keysParam}&lang={request.LanguageCode ?? "en"}",
                cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Result<LocalizationCacheInfo_DTO>> GetCacheStatisticsAsync(
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<LocalizationCacheInfo_DTO>(
                $"{BaseEndpoint}/cache/statistics",
                cancellationToken);
        }
    }
}
