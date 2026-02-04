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
    /// API Client for Stock Quarantine operations
    /// </summary>
    public class StockQuarantine_ApiClient : ApiClientBase, IStockQuarantine_ApiClient
    {
        private const string BaseRoute = "api/stock/quarantines";
        protected override string BaseEndpoint => BaseRoute;
        public StockQuarantine_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock quarantine by ID
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockQuarantine_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock quarantines
        public async Task<Result<IEnumerable<StockQuarantine_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockQuarantine_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock quarantine
        public async Task<Result<StockQuarantine_Response_DTO>> CreateAsync(StockQuarantine_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockQuarantine_Edit_DTO, StockQuarantine_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock quarantine
        public async Task<Result<StockQuarantine_Response_DTO>> UpdateAsync(long id, StockQuarantine_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockQuarantine_Edit_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock quarantine
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await base.DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock quarantine by transaction number
        public async Task<Result<StockQuarantine_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await GetAsync<StockQuarantine_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock quarantine summaries for list views
        public async Task<Result<List<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Searches stock quarantines with optional filters
        public async Task<Result<List<StockQuarantine_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)
        {
            var result = await PostListAsync<Dictionary<string, object>, StockQuarantine_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            return result.Map(items => items.ToList());
        }
        // Bulk Operations
        /// Bulk deletes multiple stock quarantines
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids, cancellationToken);
        }
        /// Changes workflow status for multiple stock quarantines
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        // Validation
        /// Validates a stock quarantine without saving it
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(StockQuarantine_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        // Workflow Operations
        /// Releases stock from quarantine (workflow transition: QUARANTINED → RELEASED)
        /// Returns stock to available inventory after quality approval
        public async Task<Result<StockQuarantine_Response_DTO>> ReleaseQuarantineAsync(long id, StockQuarantine_Release_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Release_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/release", request, cancellationToken);
        }
        /// Rejects quarantined stock (workflow transition: QUARANTINED → REJECTED)
        /// Scraps or disposes of non-conforming stock
        public async Task<Result<StockQuarantine_Response_DTO>> RejectQuarantineAsync(long id, StockQuarantine_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Reject_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock quarantine
        public async Task<Result<List<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }

        // Workflow Operations

        /// <summary>
        /// Passes inspection for quarantined stock
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> PassInspectionAsync(long id, StockQuarantine_PassInspection_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_PassInspection_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/pass-inspection", request, cancellationToken);
        }

        /// <summary>
        /// Fails inspection for quarantined stock
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> FailInspectionAsync(long id, StockQuarantine_FailInspection_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_FailInspection_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/fail-inspection", request, cancellationToken);
        }

        /// <summary>
        /// Releases stock from quarantine
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> ReleaseAsync(long id, StockQuarantine_Release_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Release_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/release", request, cancellationToken);
        }

        /// <summary>
        /// Scraps quarantined stock
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> ScrapAsync(long id, StockQuarantine_Scrap_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Scrap_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/scrap", request, cancellationToken);
        }

        /// <summary>
        /// Approves a quarantine
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> ApproveAsync(long id, StockQuarantine_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Approve_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a quarantine
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> RejectAsync(long id, StockQuarantine_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Reject_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Activates a quarantine
        /// </summary>
        public async Task<Result<StockQuarantine_Response_DTO>> ActivateAsync(long id, StockQuarantine_Activate_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockQuarantine_Activate_DTO, StockQuarantine_Response_DTO>($"{BaseRoute}/{id}/activate", request, cancellationToken);
        }
    }
}
