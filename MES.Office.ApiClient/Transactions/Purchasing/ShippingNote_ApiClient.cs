using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Purchasing
{
    /// <summary>
    /// ApiClient implementation for Shipping Note operations
    /// Route: api/purchasing/shipping-notes
    /// </summary>
    public class ShippingNote_ApiClient : ApiClientBase, IShippingNote_ApiClient
    {
        private const string BaseRoute = "api/purchasing/shipping-notes";
        protected override string BaseEndpoint => BaseRoute;
        public ShippingNote_ApiClient(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        #region Standard CRUD Operations
        /// <summary>
        /// Retrieves a shipping note by ID
        /// </summary>
        public async Task<Result<ShippingNote_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<ShippingNote_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// <summary>
        /// Retrieves all shipping notes (base interface implementation)
        /// </summary>
        public async Task<Result<IEnumerable<ShippingNote_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<ShippingNote_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Retrieves shipping notes with filtering
        /// </summary>
        public async Task<Result<IEnumerable<ShippingNote_Response_DTO>>> GetFilteredAsync(
            string statusCode = null,
            string customerId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (!string.IsNullOrEmpty(customerId))
                queryParams.Add($"customerId={Uri.EscapeDataString(customerId)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<ShippingNote_Response_DTO>($"{BaseRoute}{queryString}", cancellationToken);
        }

        /// Retrieves shipping notes with filtering and pagination
        public async Task<Result<PagedResult<ShippingNote_Summary_DTO>>> GetAllAsync(
            int page = 1,
            int pageSize = 50,
            string statusCode = null,
            string customerId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>
            {
                $"page={page}",
                $"pageSize={pageSize}"
            };
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (!string.IsNullOrEmpty(customerId))
                queryParams.Add($"customerId={Uri.EscapeDataString(customerId)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");
            var queryString = string.Join("&", queryParams);
            return await GetAsync<PagedResult<ShippingNote_Summary_DTO>>($"{BaseRoute}?{queryString}", cancellationToken);
        }
        /// Creates a new shipping note
        public async Task<Result<ShippingNote_Response_DTO>> CreateAsync(ShippingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ShippingNote_Edit_DTO, ShippingNote_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing shipping note
        public async Task<Result<ShippingNote_Response_DTO>> UpdateAsync(long id, ShippingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<ShippingNote_Edit_DTO, ShippingNote_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a shipping note
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        #endregion
        #region Bulk Operations
        /// Bulk deletes multiple shipping notes
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids, cancellationToken);
        }
        /// Changes workflow status for multiple shipping notes
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        #endregion
        #region Validation
        /// Validates a shipping note without saving
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(ShippingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ShippingNote_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        #endregion
        #region Workflow Operations
        /// <summary>
        /// Ships a shipping note (workflow transition: PENDING → SHIPPED)
        /// </summary>
        public async Task<Result<ShippingNote_Response_DTO>> ShipAsync(long id, ShippingNote_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<ShippingNote_Ship_DTO, ShippingNote_Response_DTO>($"{BaseRoute}/{id}/ship", request, cancellationToken);
        }

        /// <summary>
        /// Delivers a shipping note (workflow transition: SHIPPED → DELIVERED)
        /// </summary>
        public async Task<Result<ShippingNote_Response_DTO>> DeliverAsync(long id, ShippingNote_Deliver_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<ShippingNote_Deliver_DTO, ShippingNote_Response_DTO>($"{BaseRoute}/{id}/deliver", request, cancellationToken);
        }
        #endregion
        #region History
        /// Gets workflow history for a shipping note
        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
        #endregion
    }
}
