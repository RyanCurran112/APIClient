using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for internal Language entity (MES database)
    /// Uses the Localization controller endpoints
    /// </summary>
    public class InternalLanguage_ApiClient : CrudApiClientBase<Language_Response_DTO, Language_Edit_DTO>, IInternalLanguage_ApiClient
    {
        protected override string BaseEndpoint => "api/configuration/localization/languages";

        public InternalLanguage_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<Language_Response_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetTestDataAsync(x => x.LanguageCode, cancellationToken);
        }
    }
}
