using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Logistics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace MES.Office.ApiClient.Objects.Logistics
{
    /// <summary>
    /// API Client for Carrier operations
    /// </summary>
    public class Carrier_ApiClient : CrudApiClientBase<Carrier_Response_DTO, Carrier_Response_DTO, Carrier_Edit_DTO>, ICarrier_ApiClient
    {
        protected override string BaseEndpoint => "api/carriers";
        public Carrier_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a carrier by its code
        /// </summary>
        public override async Task<Result<Carrier_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Carrier_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
