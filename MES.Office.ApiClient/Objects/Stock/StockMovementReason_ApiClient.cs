using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Stock Movement Reason operations
    /// </summary>
    public class StockMovementReason_ApiClient : CrudApiClientBase<StockMovementReason_Response_DTO, StockMovementReason_Response_DTO, StockMovementReason_Edit_DTO>, IStockMovementReason_ApiClient
    {
        protected override string BaseEndpoint => "api/stock/movement-reasons";
        public StockMovementReason_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get all active stock movement reasons
        /// </summary>
        public async Task<Result<IEnumerable<StockMovementReason_Response_DTO>>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovementReason_Response_DTO>($"{BaseEndpoint}/active", cancellationToken);
        }
    }
}
