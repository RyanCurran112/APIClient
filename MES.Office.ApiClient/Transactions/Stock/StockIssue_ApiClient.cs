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
    /// API Client for Stock Issue operations
    /// </summary>
    public class StockIssue_ApiClient : ApiClientBase, IStockIssue_ApiClient
    {
        private const string BaseRoute = "api/stock/issues";
        protected override string BaseEndpoint => BaseRoute;
        public StockIssue_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock issue by ID
        /// </summary>
        public async Task<Result<StockIssue_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockIssue_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock issues
        public async Task<Result<IEnumerable<StockIssue_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockIssue_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock issue
        public async Task<Result<StockIssue_Response_DTO>> CreateAsync(StockIssue_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockIssue_Edit_DTO, StockIssue_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock issue
        public async Task<Result<StockIssue_Response_DTO>> UpdateAsync(long id, StockIssue_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockIssue_Edit_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock issue
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock issue by transaction number
        public async Task<Result<StockIssue_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockIssue_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock issue summaries for list views
        public async Task<Result<IEnumerable<StockIssue_Summary_DTO>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockIssue_Summary_DTO>($"{BaseRoute}/summaries", cancellationToken);
        }
        /// Searches stock issues with optional filters
        public async Task<Result<IEnumerable<StockIssue_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            return await PostListAsync<Dictionary<string, object>, StockIssue_Response_DTO>($"{BaseRoute}/search", filters);
        }
        // Workflow Operations
        /// <summary>
        /// Approves a stock issue (workflow transition: PENDING_APPROVAL → APPROVED)
        /// </summary>
        public async Task<Result<StockIssue_Response_DTO>> ApproveAsync(long id, StockIssue_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockIssue_Approve_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a stock issue (workflow transition: PENDING_APPROVAL → REJECTED)
        /// </summary>
        public async Task<Result<StockIssue_Response_DTO>> RejectAsync(long id, StockIssue_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockIssue_Reject_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Issues stock without specifying quantities (workflow transition: APPROVED → ISSUED)
        /// </summary>
        public async Task<Result<StockIssue_Response_DTO>> IssueAsync(long id, StockIssue_IssueStock_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockIssue_IssueStock_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/issue", request, cancellationToken);
        }

        /// <summary>
        /// Cancels a stock issue
        /// </summary>
        public async Task<Result<StockIssue_Response_DTO>> CancelAsync(long id, StockIssue_Cancel_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockIssue_Cancel_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }
        /// Issues stock (creates actual stock movements) - workflow transition: DRAFT → ISSUED
        /// Updates inventory and creates stock movement records
        public async Task<Result<StockIssue_Response_DTO>> IssueStockAsync(long id, StockIssue_Issue_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockIssue_Issue_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/issue", request, cancellationToken);
        }
        /// Completes a stock issue (workflow transition: ISSUED → COMPLETED)
        public async Task<Result<StockIssue_Response_DTO>> CompleteAsync(long id, StockIssue_Complete_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockIssue_Complete_DTO, StockIssue_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock issue
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }
    }
}
