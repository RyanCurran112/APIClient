using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Stock Bin operations
    /// </summary>
    public class StockBin_ApiClient : CrudApiClientBase<StockBin_Response_DTO, StockBin_Response_DTO, StockBin_Edit_DTO>, IStockBin_ApiClient
    {
        protected override string BaseEndpoint => "api/stock-bins";
        public StockBin_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a stock bin by its code
        /// </summary>
        public override async Task<Result<StockBin_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockBin_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
        /// Get all stock bins for a specific location
        public async Task<Result<List<StockBin_Response_DTO>>> GetByLocationAsync(long locationId, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-location/{locationId}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<StockBin_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }

        // Alias methods for domain-specific naming
        /// <summary>
        /// Gets a stock bin by ID (alias for GetByIdAsync)
        /// </summary>
        public Task<Result<StockBin_Response_DTO>> GetStockBinByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets all stock bins (alias for GetAllAsync)
        /// </summary>
        public Task<Result<IEnumerable<StockBin_Response_DTO>>> GetStockBinsAsync(CancellationToken cancellationToken = default)
        {
            return GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Creates a new stock bin (alias for CreateAsync)
        /// </summary>
        public Task<Result<StockBin_Response_DTO>> CreateStockBinAsync(StockBin_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return CreateAsync(dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing stock bin (alias for UpdateAsync)
        /// </summary>
        public Task<Result<StockBin_Response_DTO>> UpdateStockBinAsync(long id, StockBin_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return UpdateAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a stock bin (alias for DeleteAsync)
        /// </summary>
        public Task<Result<Unit>> DeleteStockBinAsync(long id, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(id, cancellationToken);
        }
    }
}
