using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Common;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Common
{
    /// <summary>
    /// API Client for Partner operations
    /// </summary>
    public class Partner_ApiClient : CrudApiClientBase<Partner_Response_DTO, Partner_Response_DTO, Partner_Edit_DTO>, IPartner_ApiClient
    {
        protected override string BaseEndpoint => "api/partners";
        public Partner_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary> 
        /// Get a partner by its code
        /// </summary>
        public override async Task<Result<Partner_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Partner_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
