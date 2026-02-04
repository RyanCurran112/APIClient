using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.Transactions.Purchasing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Purchasing
{
    /// <summary>
    /// ApiClient implementation for Purchase Order operations
    /// Route: api/purchasing/purchase-orders
    /// </summary>
    public class PurchaseOrder_ApiClient : ApiClientBase, IPurchaseOrder_ApiClient
    {
        private const string BaseRoute = "api/purchasing/purchase-orders";
        protected override string BaseEndpoint => BaseRoute;
        public PurchaseOrder_ApiClient(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        #region Standard CRUD Operations
        /// <summary>
        /// Retrieves a purchase order by ID
        /// </summary>
        public async Task<Result<PurchaseOrder_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Retrieves all purchase orders
        public async Task<Result<IEnumerable<PurchaseOrder_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<PurchaseOrder_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> CreateAsync(PurchaseOrder_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<PurchaseOrder_Edit_DTO, PurchaseOrder_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> UpdateAsync(long id, PurchaseOrder_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<PurchaseOrder_Edit_DTO, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a purchase order
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        #endregion
        #region Bulk Operations
        /// Bulk deletes multiple purchase orders
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids, cancellationToken);
        }
        /// Changes workflow status for multiple purchase orders
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        #endregion
        #region Validation
        /// Validates a purchase order without saving
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(PurchaseOrder_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<PurchaseOrder_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        #endregion
        #region Workflow Operations
        /// Submits a purchase order for approval
        public async Task<Result<PurchaseOrder_Response_DTO>> SubmitForApprovalAsync(long id, SubmitPurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SubmitPurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }
        /// Approves a purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> ApproveAsync(long id, ApprovePurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ApprovePurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }
        /// Rejects a purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> RejectAsync(long id, RejectPurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<RejectPurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }
        /// Sends purchase order to supplier
        public async Task<Result<PurchaseOrder_Response_DTO>> SendToSupplierAsync(long id, SendPurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SendPurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/send", request, cancellationToken);
        }
        /// Receives goods from purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> ReceiveGoodsAsync(long id, ReceivePurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ReceivePurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/receive", request, cancellationToken);
        }
        /// Closes a purchase order
        public async Task<Result<PurchaseOrder_Response_DTO>> CloseAsync(long id, ClosePurchaseOrderDto request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ClosePurchaseOrderDto, PurchaseOrder_Response_DTO>($"{BaseRoute}/{id}/close", request, cancellationToken);
        }
        #endregion
        #region History
        /// Gets workflow history for a purchase order
        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
        #endregion

        #region Transaction View Filter Methods

        /// <summary>
        /// Get purchase orders with filters for transaction views
        /// </summary>
        public async Task<Result<IEnumerable<PurchaseOrder_Response_DTO>>> GetWithFiltersAsync(
            long? supplierId = null,
            long? productId = null,
            long? siteId = null,
            string? statusCode = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 50,
            string? sortBy = null,
            string sortDirection = "desc",
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();

            if (supplierId.HasValue)
                queryParams.Add($"supplierId={supplierId.Value}");
            if (productId.HasValue)
                queryParams.Add($"productId={productId.Value}");
            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            if (!string.IsNullOrEmpty(sortBy))
                queryParams.Add($"sortBy={Uri.EscapeDataString(sortBy)}");
            queryParams.Add($"sortDirection={Uri.EscapeDataString(sortDirection)}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<PurchaseOrder_Response_DTO>($"{BaseRoute}{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get purchase order lines by product with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<PurchaseOrder_Line_List_DTO>>> GetLinesAsync(
            long? productId = null,
            long? supplierId = null,
            long? siteId = null,
            string? statusCode = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();

            if (productId.HasValue)
                queryParams.Add($"productId={productId.Value}");
            if (supplierId.HasValue)
                queryParams.Add($"supplierId={supplierId.Value}");
            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<PurchaseOrder_Line_List_DTO>($"{BaseRoute}/lines{queryString}", cancellationToken);
        }

        #endregion
    }
}
