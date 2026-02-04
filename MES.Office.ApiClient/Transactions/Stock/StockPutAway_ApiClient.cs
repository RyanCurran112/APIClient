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
    /// API Client for Stock Put-Away operations
    /// </summary>
    public class StockPutAway_ApiClient : ApiClientBase, IStockPutAway_ApiClient
    {
        private const string BaseRoute = "api/stock/put-aways";

        protected override string BaseEndpoint => BaseRoute;

        public StockPutAway_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        // ========================================
        // CRUD Operations (from IApiClient)
        // ========================================

        /// <summary>
        /// Gets a stock put-away by ID
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockPutAway_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets all stock put-aways
        /// </summary>
        public async Task<Result<IEnumerable<StockPutAway_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockPutAway_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Creates a new stock put-away
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> CreateAsync(StockPutAway_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_Edit_DTO, StockPutAway_Response_DTO>(BaseRoute, dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing stock put-away
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> UpdateAsync(long id, StockPutAway_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockPutAway_Edit_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a stock put-away
        /// </summary>
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }

        // ========================================
        // Additional Query Operations
        // ========================================

        /// <summary>
        /// Retrieves a stock put-away by transaction number
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockPutAway_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }

        /// <summary>
        /// Retrieves stock put-away summaries for list views
        /// </summary>
        public async Task<Result<IEnumerable<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
        }

        /// <summary>
        /// Searches stock put-aways with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<StockPutAway_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, StockPutAway_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            if (!result.IsSuccess)
                return Result<IEnumerable<StockPutAway_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<IEnumerable<StockPutAway_Response_DTO>>.Success(result.Value);
        }

        // ========================================
        // Workflow Operations
        // ========================================

        /// <summary>
        /// Submits a stock put-away for assignment (workflow transition: DRAFT → PENDING)
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> SubmitAsync(long id, StockPutAway_Submit_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_Submit_Workflow_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Assigns a stock put-away to a warehouse operator (workflow transition: PENDING → ASSIGNED)
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> AssignAsync(long id, StockPutAway_Assign_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_Assign_Workflow_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}/assign", request, cancellationToken);
        }

        /// <summary>
        /// Starts a stock put-away execution (workflow transition: ASSIGNED → IN_PROGRESS)
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> StartAsync(long id, StockPutAway_Start_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_Start_Workflow_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}/start", request, cancellationToken);
        }

        /// <summary>
        /// Completes a stock put-away (workflow transition: IN_PROGRESS → COMPLETED)
        /// All lines must be completed or skipped before completion
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> CompleteAsync(long id, StockPutAway_Complete_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_Complete_Workflow_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }

        /// <summary>
        /// Puts away an individual line to a destination bin
        /// Creates stock movements and updates inventory
        /// Can be used while status is ASSIGNED or IN_PROGRESS
        /// </summary>
        public async Task<Result<StockPutAway_Response_DTO>> PutAwayLineAsync(long id, StockPutAway_PutAwayLine_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockPutAway_PutAwayLine_DTO, StockPutAway_Response_DTO>($"{BaseRoute}/{id}/put-away-line", request, cancellationToken);
        }

        /// <summary>
        /// Retrieves workflow history for a stock put-away
        /// </summary>
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }
    }
}
