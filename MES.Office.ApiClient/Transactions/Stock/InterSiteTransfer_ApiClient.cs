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
    /// API Client for Inter-Site Transfer operations
    /// </summary>
    public class InterSiteTransfer_ApiClient : ApiClientBase, IInterSiteTransfer_ApiClient
    {
        private const string BaseRoute = "api/stock/site-transfers";

        protected override string BaseEndpoint => BaseRoute;

        public InterSiteTransfer_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        // ========================================
        // CRUD Operations (from IApiClient)
        // ========================================

        /// <summary>
        /// Gets an inter-site transfer by ID
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets all inter-site transfers
        /// </summary>
        public async Task<Result<IEnumerable<InterSiteTransfer_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<InterSiteTransfer_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Creates a new inter-site transfer
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> CreateAsync(InterSiteTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Edit_DTO, InterSiteTransfer_Response_DTO>(BaseRoute, dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing inter-site transfer
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> UpdateAsync(long id, InterSiteTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<InterSiteTransfer_Edit_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }

        /// <summary>
        /// Deletes an inter-site transfer
        /// </summary>
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }

        // ========================================
        // Additional Query Operations
        // ========================================

        /// <summary>
        /// Retrieves an inter-site transfer by transaction number
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<InterSiteTransfer_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }

        /// <summary>
        /// Retrieves inter-site transfer summaries for list views
        /// </summary>
        public async Task<Result<IEnumerable<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
        }

        /// <summary>
        /// Searches inter-site transfers with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<InterSiteTransfer_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, InterSiteTransfer_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            if (!result.IsSuccess)
                return Result<IEnumerable<InterSiteTransfer_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<IEnumerable<InterSiteTransfer_Response_DTO>>.Success(result.Value);
        }

        // ========================================
        // Workflow Operations
        // ========================================

        /// <summary>
        /// Submits an inter-site transfer for approval (workflow transition: DRAFT → PENDING_APPROVAL)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> SubmitForApprovalAsync(long id, InterSiteTransfer_Submit_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Submit_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Approves an inter-site transfer (workflow transition: PENDING_APPROVAL → APPROVED)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ApproveTransferAsync(long id, InterSiteTransfer_Approve_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Approve_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Ships an inter-site transfer (workflow transition: APPROVED → SHIPPED)
        /// Creates stock movements at source site and updates inventory
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ShipTransferAsync(long id, InterSiteTransfer_Ship_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Ship_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/ship", request, cancellationToken);
        }

        /// <summary>
        /// Receives an inter-site transfer (workflow transition: SHIPPED → RECEIVED)
        /// Creates stock movements at destination site and updates inventory
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ReceiveTransferAsync(long id, InterSiteTransfer_Receive_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Receive_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/receive", request, cancellationToken);
        }

        /// <summary>
        /// Cancels an inter-site transfer (workflow transition: Any → CANCELLED)
        /// Reverses any stock movements if applicable
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> CancelTransferAsync(long id, InterSiteTransfer_Cancel_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterSiteTransfer_Cancel_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }

        /// <summary>
        /// Retrieves workflow history for an inter-site transfer
        /// </summary>
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }

        /// <summary>
        /// Approves an inter-site transfer without additional data (simplified version)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ApproveAsync(long id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterSiteTransfer_Approve_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/approve", new InterSiteTransfer_Approve_DTO(), cancellationToken);
        }

        /// <summary>
        /// Rejects an inter-site transfer
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> RejectAsync(long id, InterSiteTransfer_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterSiteTransfer_Reject_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Completes an inter-site transfer (workflow transition: RECEIVED → COMPLETED)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> CompleteAsync(long id, InterSiteTransfer_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterSiteTransfer_Complete_DTO, InterSiteTransfer_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }

        /// <summary>
        /// Ships an inter-site transfer (alias for ShipTransferAsync)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ShipAsync(long id, InterSiteTransfer_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await ShipTransferAsync(id, request, cancellationToken);
        }

        /// <summary>
        /// Receives an inter-site transfer (alias for ReceiveTransferAsync)
        /// </summary>
        public async Task<Result<InterSiteTransfer_Response_DTO>> ReceiveAsync(long id, InterSiteTransfer_Receive_DTO request, CancellationToken cancellationToken = default)
        {
            return await ReceiveTransferAsync(id, request, cancellationToken);
        }
    }
}
