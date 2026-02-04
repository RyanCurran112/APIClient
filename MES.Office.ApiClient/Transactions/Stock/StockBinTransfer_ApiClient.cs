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
    /// API Client for Stock Bin Transfer operations
    /// </summary>
    public class StockBinTransfer_ApiClient : ApiClientBase, IStockBinTransfer_ApiClient
    {
        private const string BaseRoute = "api/stock/bin-transfers";
        protected override string BaseEndpoint => BaseRoute;
        public StockBinTransfer_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock bin transfer by ID
        /// </summary>
        public async Task<Result<StockBinTransfer_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock bin transfers
        public async Task<Result<IEnumerable<StockBinTransfer_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockBinTransfer_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock bin transfer
        public async Task<Result<StockBinTransfer_Response_DTO>> CreateAsync(StockBinTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockBinTransfer_Edit_DTO, StockBinTransfer_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock bin transfer
        public async Task<Result<StockBinTransfer_Response_DTO>> UpdateAsync(long id, StockBinTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockBinTransfer_Edit_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock bin transfer
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock bin transfer by transaction number
        public async Task<Result<StockBinTransfer_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockBinTransfer_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock bin transfer summaries for list views
        public async Task<Result<IEnumerable<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
        }
        /// Searches stock bin transfers with optional filters
        public async Task<Result<IEnumerable<StockBinTransfer_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            return await PostListAsync<Dictionary<string, object>, StockBinTransfer_Response_DTO>($"{BaseRoute}/search", filters);
        }
        // Bulk Operations
        /// Bulk deletes multiple stock bin transfers
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids);
        }
        /// Changes workflow status for multiple stock bin transfers
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        // Validation
        /// Validates a stock bin transfer without saving it
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(StockBinTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockBinTransfer_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        // Workflow Operations
        /// Starts a stock bin transfer (workflow transition: DRAFT → IN_PROGRESS)
        /// Initiates the physical movement of stock between bins
        public async Task<Result<StockBinTransfer_Response_DTO>> StartTransferAsync(long id, StockBinTransfer_Start_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockBinTransfer_Start_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}/start", request, cancellationToken);
        }
        /// Completes a stock bin transfer (workflow transition: IN_PROGRESS → COMPLETED)
        /// Creates stock movements and updates bin quantities
        public async Task<Result<StockBinTransfer_Response_DTO>> CompleteTransferAsync(long id, StockBinTransfer_Complete_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockBinTransfer_Complete_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock bin transfer
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }

        /// <summary>
        /// Approves a stock bin transfer (workflow transition: PENDING_APPROVAL → APPROVED)
        /// </summary>
        public async Task<Result<StockBinTransfer_Response_DTO>> ApproveAsync(long id, StockBinTransfer_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransfer_Approve_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a stock bin transfer (workflow transition: PENDING_APPROVAL → REJECTED)
        /// </summary>
        public async Task<Result<StockBinTransfer_Response_DTO>> RejectAsync(long id, StockBinTransfer_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransfer_Reject_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Completes a stock bin transfer (uses requested quantities as actual quantities)
        /// </summary>
        public async Task<Result<StockBinTransfer_Response_DTO>> CompleteAsync(long id, StockBinTransfer_CompleteSimple_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransfer_CompleteSimple_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }

        /// <summary>
        /// Searches stock bin transfers with typed filter
        /// </summary>
        public async Task<Result<IEnumerable<StockBinTransfer_Response_DTO>>> SearchAsync(StockBinTransfer_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<StockBinTransfer_Filter_DTO, StockBinTransfer_Response_DTO>($"{BaseRoute}/search", filter, cancellationToken);
        }
    }
}
