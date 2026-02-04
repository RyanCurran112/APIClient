using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Warehouse operations
    /// </summary>
    public class Warehouse_ApiClient : CrudApiClientBase<Warehouse_Response_DTO, Warehouse_Response_DTO, Warehouse_Edit_DTO>, IWarehouse_ApiClient
    {
        protected override string BaseEndpoint => "api/warehouses";
        public Warehouse_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a warehouse by its code
        /// </summary>
        public override async Task<Result<Warehouse_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Warehouse_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
