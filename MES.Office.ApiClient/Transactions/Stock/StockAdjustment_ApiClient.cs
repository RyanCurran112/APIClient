using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common.Workflow;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Stock
{
    /// <summary>
    /// API Client for Stock Adjustment operations
    /// </summary>
    public class StockAdjustment_ApiClient : ApiClientBase, IStockAdjustment_ApiClient
    {
        private const string BaseRoute = "api/stock/adjustments";
        protected override string BaseEndpoint => BaseRoute;
        public StockAdjustment_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock adjustment by ID
        /// </summary>
        public async Task<Result<StockAdjustment_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockAdjustment_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock adjustments
        public async Task<Result<IEnumerable<StockAdjustment_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockAdjustment_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock adjustment
        public async Task<Result<StockAdjustment_Response_DTO>> CreateAsync(StockAdjustment_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockAdjustment_Edit_DTO, StockAdjustment_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock adjustment
        public async Task<Result<StockAdjustment_Response_DTO>> UpdateAsync(long id, StockAdjustment_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockAdjustment_Edit_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock adjustment
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock adjustment by transaction number
        public async Task<Result<StockAdjustment_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockAdjustment_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock adjustment summaries for list views
        public async Task<Result<List<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Searches stock adjustments with optional filters
        public async Task<Result<List<StockAdjustment_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, StockAdjustment_Response_DTO>($"{BaseRoute}/search", filters);
            return result.Map(items => items.ToList());
        }
        // Bulk Operations
        /// Bulk deletes multiple stock adjustments
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids);
        }
        /// Changes workflow status for multiple stock adjustments
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        // Validation
        /// Validates a stock adjustment without saving it
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(StockAdjustment_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockAdjustment_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        // Workflow Operations
        /// Submits a stock adjustment for approval (workflow transition: DRAFT → PENDING_APPROVAL)
        public async Task<Result<StockAdjustment_Response_DTO>> SubmitForApprovalAsync(long id, WorkflowSubmit_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<WorkflowSubmit_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }
        /// Approves a stock adjustment (workflow transition: PENDING_APPROVAL → APPROVED)
        public async Task<Result<StockAdjustment_Response_DTO>> ApproveAsync(long id, WorkflowApprove_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<WorkflowApprove_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }
        /// Posts a stock adjustment to inventory (workflow transition: APPROVED → POSTED)
        /// Creates stock movements and updates inventory levels
        public async Task<Result<StockAdjustment_Response_DTO>> PostAsync(long id, WorkflowComplete_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<WorkflowComplete_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}/post", request, cancellationToken);
        }
        /// Rejects a stock adjustment (workflow transition: PENDING_APPROVAL → REJECTED)
        public async Task<Result<StockAdjustment_Response_DTO>> RejectAsync(long id, string reason, CancellationToken cancellationToken = default)

        {
            var request = new WorkflowReject_DTO
            {
                PerformedBy = "System", // Should be populated from user context
                RejectionReason = reason
            };
            return await PostAsync<WorkflowReject_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }
        /// Cancels a stock adjustment (workflow transition: any status → CANCELLED)
        public async Task<Result<StockAdjustment_Response_DTO>> CancelAsync(long id, CancellationToken cancellationToken = default)

        {
            var request = new WorkflowCancel_DTO
            {
                PerformedBy = "System", // Should be populated from user context
                CancellationReason = "Cancelled by user"
            };
            return await PostAsync<WorkflowCancel_DTO, StockAdjustment_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock adjustment
        public async Task<Result<List<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
    }
}
