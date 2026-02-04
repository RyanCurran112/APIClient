using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Stock;
using MES.Office.WebAPI.Contracts.Enums;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Stock
{
    /// <summary>
    /// API Client for Stock Movement operations (READ-ONLY)
    /// Stock Movements are created automatically by other stock transactions
    /// </summary>
    public class StockMovement_ApiClient : ApiClientBase, IStockMovement_ApiClient
    {
        private const string BaseRoute = "api/stock/movements";
        protected override string BaseEndpoint => BaseRoute;
        public StockMovement_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // Query Operations (READ-ONLY)
        /// <summary>
        /// Retrieves a single stock movement by ID
        /// </summary>
        public async Task<Result<StockMovement_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<StockMovement_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Retrieves all stock movements
        public async Task<Result<IEnumerable<StockMovement_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Creates a new stock movement (not supported - use NotSupportedException pattern)
        /// </summary>
        public Task<Result<StockMovement_Response_DTO>> CreateAsync(StockMovement_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<StockMovement_Response_DTO>.Failure(
                "Stock movements cannot be created directly. They are created automatically by other stock transactions.",
                ErrorCodes.ValidationError,
                source: "StockMovement_ApiClient",
                context: new Dictionary<string, object>()));
        }

        /// <summary>
        /// Updates a stock movement (not supported - use NotSupportedException pattern)
        /// </summary>
        public Task<Result<StockMovement_Response_DTO>> UpdateAsync(long id, StockMovement_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<StockMovement_Response_DTO>.Failure(
                "Stock movements cannot be updated. They are immutable records created by other stock transactions.",
                ErrorCodes.ValidationError,
                source: "StockMovement_ApiClient",
                context: new Dictionary<string, object>()));
        }

        /// <summary>
        /// Deletes a stock movement (not supported - use NotSupportedException pattern)
        /// </summary>
        public Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<Unit>.Failure(
                "Stock movements cannot be deleted. They are immutable audit records.",
                ErrorCodes.ValidationError,
                source: "StockMovement_ApiClient",
                context: new Dictionary<string, object>()));
        }
        /// Retrieves stock movements with optional filters
        public async Task<Result<IEnumerable<StockMovement_Summary_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<Dictionary<string, object>, StockMovement_Summary_DTO>($"{BaseRoute}/search", filters, cancellationToken);
        }
        /// Retrieves stock movements for a specific product
        public async Task<Result<IEnumerable<StockMovement_Summary_DTO>>> GetByProductAsync(string productId, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Summary_DTO>($"{BaseRoute}/product/{productId}", cancellationToken);
        }
        /// Retrieves stock movements within a date range
        public async Task<Result<IEnumerable<StockMovement_Summary_DTO>>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Summary_DTO>($"{BaseRoute}/date-range?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}", cancellationToken);
        }
        /// Retrieves stock movements for a specific stock bin
        public async Task<Result<IEnumerable<StockMovement_Summary_DTO>>> GetByStockBinAsync(string stockBinId, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Summary_DTO>($"{BaseRoute}/stock-bin/{stockBinId}", cancellationToken);
        }
        /// Retrieves stock movements for a specific batch
        public async Task<Result<IEnumerable<StockMovement_Summary_DTO>>> GetByBatchAsync(string batchId, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Summary_DTO>($"{BaseRoute}/batch/{batchId}", cancellationToken);
        }
        /// Retrieves stock movements for a specific source entity
        public async Task<Result<IEnumerable<StockMovement_Response_DTO>>> GetByEntityAsync(int entityId, EntityType entityType, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockMovement_Response_DTO>($"{BaseRoute}/entity/{entityId}/{entityType}", cancellationToken);
        }
        /// Retrieves stock movement analytics for a date range with optional filters
        public async Task<Result<IEnumerable<StockMovement_Analytics_DTO>>> GetAnalyticsAsync(DateTime fromDate, DateTime toDate, string productId = null, string stockBinId = null, CancellationToken cancellationToken = default)
        {
            var query = $"fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}";
            if (!string.IsNullOrEmpty(productId))
                query += $"&productId={productId}";
            if (!string.IsNullOrEmpty(stockBinId))
                query += $"&stockBinId={stockBinId}";
            return await GetListAsync<StockMovement_Analytics_DTO>($"{BaseRoute}/analytics?{query}", cancellationToken);
        }
        /// Retrieves historical audit trail for a specific stock movement
        public async Task<Result<IEnumerable<object>>> GetHistoryAsync(long stockMovementId, Dictionary<string, object> filters = null, CancellationToken cancellationToken = default)
        {
            if (filters != null)
            {
                return await PostListAsync<Dictionary<string, object>, object>($"{BaseRoute}/{stockMovementId}/history", filters, cancellationToken);
            }
            return await GetListAsync<object>($"{BaseRoute}/{stockMovementId}/history", cancellationToken);
        }

        // TRANSACTION VIEW FILTER METHODS

        /// <summary>
        /// Get stock movements with filters for transaction views
        /// </summary>
        public async Task<Result<IEnumerable<StockMovement_Response_DTO>>> GetWithFiltersAsync(
            long? productId = null,
            long? warehouseId = null,
            long? locationId = null,
            long? siteId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            // Build filters dictionary for search endpoint
            var filters = new Dictionary<string, object>();

            if (productId.HasValue)
                filters.Add("ProductId", productId.Value);
            if (warehouseId.HasValue)
                filters.Add("WarehouseId", warehouseId.Value);
            if (locationId.HasValue)
                filters.Add("LocationId", locationId.Value);
            if (siteId.HasValue)
                filters.Add("SiteId", siteId.Value);
            if (dateFrom.HasValue)
                filters.Add("DateFrom", dateFrom.Value);
            if (dateTo.HasValue)
                filters.Add("DateTo", dateTo.Value);

            filters.Add("Page", page);
            filters.Add("PageSize", pageSize);

            // Use existing search method with dictionary filters
            var searchResult = await PostListAsync<Dictionary<string, object>, StockMovement_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            return searchResult;
        }
    }
}
