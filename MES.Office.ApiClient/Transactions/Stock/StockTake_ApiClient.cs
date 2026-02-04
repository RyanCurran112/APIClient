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
    /// API Client for Stock Take operations
    /// </summary>
    public class StockTake_ApiClient : ApiClientBase, IStockTake_ApiClient
    {
        private const string BaseRoute = "api/stock/takes";
        protected override string BaseEndpoint => BaseRoute;
        public StockTake_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock take by ID
        /// </summary>
        public async Task<Result<StockTake_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockTake_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock takes
        public async Task<Result<IEnumerable<StockTake_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockTake_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock take
        public async Task<Result<StockTake_Response_DTO>> CreateAsync(StockTake_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockTake_Edit_DTO, StockTake_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock take
        public async Task<Result<StockTake_Response_DTO>> UpdateAsync(long id, StockTake_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockTake_Edit_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock take
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await base.DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock take by transaction number
        public async Task<Result<StockTake_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await GetAsync<StockTake_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock take summaries for list views
        public async Task<Result<List<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Searches stock takes with optional filters
        public async Task<Result<List<StockTake_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)
        {
            var result = await PostListAsync<Dictionary<string, object>, StockTake_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            return result.Map(items => items.ToList());
        }
        // Bulk Operations
        /// Bulk deletes multiple stock takes
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids, cancellationToken);
        }
        /// Changes workflow status for multiple stock takes
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        // Validation
        /// Validates a stock take without saving it
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(StockTake_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        // Workflow Operations
        /// Schedules a stock take (workflow transition: DRAFT → SCHEDULED)
        public async Task<Result<StockTake_Response_DTO>> ScheduleStockTakeAsync(long id, StockTake_Schedule_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Schedule_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/schedule", request, cancellationToken);
        }
        /// Starts a stock take (workflow transition: SCHEDULED → IN_PROGRESS)
        public async Task<Result<StockTake_Response_DTO>> StartStockTakeAsync(long id, StockTake_Start_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Start_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/start", request, cancellationToken);
        }
        /// Completes a stock take (workflow transition: IN_PROGRESS → COMPLETED)
        /// Records final counted quantities
        public async Task<Result<StockTake_Response_DTO>> CompleteStockTakeAsync(long id, StockTake_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Complete_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }
        /// Posts stock take results to inventory (workflow transition: COMPLETED → POSTED)
        /// Creates stock adjustments based on variances
        public async Task<Result<StockTake_Response_DTO>> PostStockTakeAsync(long id, StockTake_Post_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Post_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/post", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock take
        public async Task<Result<List<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
        // Workflow Alias Methods
        /// Starts the stock count process (alias for StartStockTakeAsync)
        public Task<Result<StockTake_Response_DTO>> StartCountAsync(long id, StockTake_Start_DTO request, CancellationToken cancellationToken = default)
        {
            return StartStockTakeAsync(id, request, cancellationToken);
        }
        /// Completes the stock count process (alias for CompleteStockTakeAsync)
        public Task<Result<StockTake_Response_DTO>> CompleteCountAsync(long id, StockTake_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return CompleteStockTakeAsync(id, request, cancellationToken);
        }
        /// <summary>
        /// Approves a stock take and its variances
        /// </summary>
        public async Task<Result<StockTake_Response_DTO>> ApproveAsync(long id, StockTake_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Approve_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a stock take
        /// </summary>
        public async Task<Result<StockTake_Response_DTO>> RejectAsync(long id, StockTake_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_Reject_DTO, StockTake_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Generates stock adjustments from stock take variances
        /// </summary>
        public async Task<Result<List<long>>> GenerateAdjustmentsAsync(long id, StockTake_GenerateAdjustments_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockTake_GenerateAdjustments_DTO, List<long>>($"{BaseRoute}/{id}/generate-adjustments", request, cancellationToken);
        }

        /// <summary>
        /// Searches stock takes with typed filter
        /// </summary>
        public async Task<Result<IEnumerable<StockTake_Response_DTO>>> SearchAsync(StockTake_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<StockTake_Filter_DTO, StockTake_Response_DTO>($"{BaseRoute}/search", filter, cancellationToken);
        }
    }
}
