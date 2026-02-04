using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for Languages
    /// </summary>
    public class Language_ApiClient : CrudApiClientBase<Language_Localization_DTO, Language_Localization_DTO>, ILanguage_ApiClient
    {
        protected override string BaseEndpoint => "api/configuration/localization/languages";

        public Language_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<Language_Localization_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            return await base.GetTestDataAsync(x => x.LanguageId, cancellationToken);
        }
    }
}
