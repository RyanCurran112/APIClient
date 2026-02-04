using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Product Batch operations
    /// </summary>
    public class ProductBatch_ApiClient : CrudApiClientBase<ProductBatch_Response_DTO, ProductBatch_Response_DTO, ProductBatch_Edit_DTO>, IProductBatch_ApiClient
    {
        protected override string BaseEndpoint => "api/product-batches";
        public ProductBatch_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a product batch by its batch number
        /// </summary>
        public async Task<Result<ProductBatch_Response_DTO>> GetByBatchNumberAsync(string batchNumber, CancellationToken cancellationToken = default)

        {
            return await GetAsync<ProductBatch_Response_DTO>($"{BaseEndpoint}/by-batch-number/{batchNumber}", cancellationToken);
        }
        /// Get all product batches for a specific product
        public async Task<Result<List<ProductBatch_Response_DTO>>> GetByProductAsync(long productId, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-product/{productId}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<ProductBatch_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get product batches that are expiring soon
        public async Task<Result<List<ProductBatch_Response_DTO>>> GetExpiringAsync(int days, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/expiring/{days}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<ProductBatch_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get product batches that have already expired
        public async Task<Result<List<ProductBatch_Response_DTO>>> GetExpiredAsync(bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/expired";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<ProductBatch_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get product batches by status
        public async Task<Result<List<ProductBatch_Response_DTO>>> GetByStatusAsync(string status, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-status/{status}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<ProductBatch_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get all active product batches
        public async Task<Result<List<ProductBatch_Response_DTO>>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<ProductBatch_Response_DTO>($"{BaseEndpoint}/active", cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Update the status of a product batch
        public async Task<Result<ProductBatch_Response_DTO>> UpdateStatusAsync(long id, string status, CancellationToken cancellationToken = default)

        {
            return await PatchAsync<string, ProductBatch_Response_DTO>($"{BaseEndpoint}/{id}/status", status, cancellationToken);
        }
        /// Update the quantity of a product batch
        public async Task<Result<ProductBatch_Response_DTO>> UpdateQuantityAsync(long id, decimal quantity, CancellationToken cancellationToken = default)

        {
            return await PatchAsync<decimal, ProductBatch_Response_DTO>($"{BaseEndpoint}/{id}/quantity", quantity, cancellationToken);
        }
    }
}
