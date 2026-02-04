using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Production;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Production;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Production
{
    /// <summary>
    /// API Client for Machine operations
    /// </summary>
    public class Machine_ApiClient : CrudApiClientBase<Machine_Response_DTO, Machine_Response_DTO, Machine_Edit_DTO>, IMachine_ApiClient
    {
        protected override string BaseEndpoint => "api/machines";
        public Machine_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a machine by its code
        /// </summary>
        public override async Task<Result<Machine_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Machine_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
