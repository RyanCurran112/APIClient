using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Stock
{
    /// <summary>
    /// API Client for Stock Issue Request operations
    /// </summary>
    public class StockIssueRequest_ApiClient : ApiClientBase, IStockIssueRequest_ApiClient
    {
        private const string BaseRoute = "api/stock/issue-requests";
        protected override string BaseEndpoint => BaseRoute;
        public StockIssueRequest_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock issue request by ID
        /// </summary>
        public async Task<Result<StockIssueRequest_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock issue requests
        public async Task<Result<IEnumerable<StockIssueRequest_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockIssueRequest_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock issue request
        public async Task<Result<StockIssueRequest_Response_DTO>> CreateAsync(StockIssueRequest_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockIssueRequest_Edit_DTO, StockIssueRequest_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock issue request
        public async Task<Result<StockIssueRequest_Response_DTO>> UpdateAsync(long id, StockIssueRequest_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockIssueRequest_Edit_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock issue request
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Bulk Operations
        /// Bulk deletes multiple stock issue requests
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids);
        }
        /// Changes workflow status for multiple stock issue requests
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        // Validation
        /// Validates a stock issue request without saving it
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(StockIssueRequest_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockIssueRequest_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        // Workflow Operations
        /// Submits a stock issue request for approval (workflow transition: DRAFT → PENDING_APPROVAL)
        public async Task<Result<StockIssueRequest_Response_DTO>> SubmitForApprovalAsync(long id, SubmitRequest_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SubmitRequest_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }
        /// Approves a stock issue request (workflow transition: PENDING_APPROVAL → APPROVED)
        public async Task<Result<StockIssueRequest_Response_DTO>> ApproveAsync(long id, ApprovalRequest_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ApprovalRequest_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }
        /// Rejects a stock issue request (workflow transition: PENDING_APPROVAL → REJECTED)
        public async Task<Result<StockIssueRequest_Response_DTO>> RejectAsync(long id, RejectionRequest_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<RejectionRequest_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }
        /// Issues stock from an approved request (workflow transition: APPROVED → ISSUED)
        /// Creates stock movements and updates inventory
        public async Task<Result<StockIssueRequest_Response_DTO>> IssueStockAsync(long id, IssueStockRequest_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<IssueStockRequest_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}/issue", request, cancellationToken);
        }
        /// Completes a stock issue request (workflow transition: ISSUED → COMPLETED)
        public async Task<Result<StockIssueRequest_Response_DTO>> CompleteAsync(long id, CompleteRequest_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<CompleteRequest_DTO, StockIssueRequest_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock issue request
        public async Task<Result<List<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Retrieves all approved stock issue requests
        public async Task<Result<IEnumerable<StockIssueRequest_Response_DTO>>> GetApprovedRequestsAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockIssueRequest_Response_DTO>($"{BaseRoute}/approved", cancellationToken);
        }

        // TRANSACTION VIEW FILTER METHODS

        /// <summary>
        /// Get stock issue requests with filters for transaction views
        /// </summary>
        public async Task<Result<IEnumerable<StockIssueRequest_Response_DTO>>> GetWithFiltersAsync(
            long? siteId = null,
            long? departmentId = null,
            long? projectId = null,
            long? machineId = null,
            long? productId = null,
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

            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (departmentId.HasValue)
                queryParams.Add($"departmentId={departmentId.Value}");
            if (projectId.HasValue)
                queryParams.Add($"projectId={projectId.Value}");
            if (machineId.HasValue)
                queryParams.Add($"machineId={machineId.Value}");
            if (productId.HasValue)
                queryParams.Add($"productId={productId.Value}");
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
            return await GetListAsync<StockIssueRequest_Response_DTO>($"{BaseRoute}{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get stock issue request lines by product with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<StockIssueRequest_Line_List_DTO>>> GetLinesAsync(
            long? productId = null,
            long? siteId = null,
            long? departmentId = null,
            long? machineId = null,
            long? projectId = null,
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
            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (departmentId.HasValue)
                queryParams.Add($"departmentId={departmentId.Value}");
            if (machineId.HasValue)
                queryParams.Add($"machineId={machineId.Value}");
            if (projectId.HasValue)
                queryParams.Add($"projectId={projectId.Value}");
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<StockIssueRequest_Line_List_DTO>($"{BaseRoute}/lines{queryString}", cancellationToken);
        }
    }
}
