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
    /// API Client for Inter-Warehouse Transfer operations
    /// </summary>
    public class InterWarehouseTransfer_ApiClient : ApiClientBase, IInterWarehouseTransfer_ApiClient
    {
        private const string BaseRoute = "api/stock/warehouse-transfers";

        protected override string BaseEndpoint => BaseRoute;

        public InterWarehouseTransfer_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        // ========================================
        // CRUD Operations (from IApiClient)
        // ========================================

        /// <summary>
        /// Gets an inter-warehouse transfer by ID
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets all inter-warehouse transfers
        /// </summary>
        public async Task<Result<IEnumerable<InterWarehouseTransfer_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<InterWarehouseTransfer_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Creates a new inter-warehouse transfer
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> CreateAsync(InterWarehouseTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Edit_DTO, InterWarehouseTransfer_Response_DTO>(BaseRoute, dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing inter-warehouse transfer
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> UpdateAsync(long id, InterWarehouseTransfer_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<InterWarehouseTransfer_Edit_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }

        /// <summary>
        /// Deletes an inter-warehouse transfer
        /// </summary>
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }

        // ========================================
        // Additional Query Operations
        // ========================================

        /// <summary>
        /// Retrieves an inter-warehouse transfer by transaction number
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }

        /// <summary>
        /// Retrieves inter-warehouse transfer summaries for list views
        /// </summary>
        public async Task<Result<IEnumerable<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
        }

        /// <summary>
        /// Searches inter-warehouse transfers with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<InterWarehouseTransfer_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            if (!result.IsSuccess)
                return Result<IEnumerable<InterWarehouseTransfer_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<IEnumerable<InterWarehouseTransfer_Response_DTO>>.Success(result.Value);
        }

        // ========================================
        // Workflow Operations
        // ========================================

        /// <summary>
        /// Submits an inter-warehouse transfer for approval (workflow transition: DRAFT → PENDING_APPROVAL)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> SubmitForApprovalAsync(long id, InterWarehouseTransfer_Submit_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Submit_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Approves an inter-warehouse transfer (workflow transition: PENDING_APPROVAL → APPROVED)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ApproveTransferAsync(long id, InterWarehouseTransfer_Approve_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Approve_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Ships an inter-warehouse transfer (workflow transition: APPROVED → SHIPPED)
        /// Creates stock movements at source warehouse and updates inventory
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ShipTransferAsync(long id, InterWarehouseTransfer_Ship_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Ship_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/ship", request, cancellationToken);
        }

        /// <summary>
        /// Receives an inter-warehouse transfer (workflow transition: SHIPPED → RECEIVED)
        /// Creates stock movements at destination warehouse and updates inventory
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ReceiveTransferAsync(long id, InterWarehouseTransfer_Receive_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Receive_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/receive", request, cancellationToken);
        }

        /// <summary>
        /// Cancels an inter-warehouse transfer (workflow transition: Any → CANCELLED)
        /// Reverses any stock movements if applicable
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> CancelTransferAsync(long id, InterWarehouseTransfer_Cancel_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<InterWarehouseTransfer_Cancel_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }

        /// <summary>
        /// Retrieves workflow history for an inter-warehouse transfer
        /// </summary>
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }

        /// <summary>
        /// Approves an inter-warehouse transfer without additional data (simplified version)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ApproveAsync(long id, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterWarehouseTransfer_Approve_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/approve", new InterWarehouseTransfer_Approve_DTO(), cancellationToken);
        }

        /// <summary>
        /// Rejects an inter-warehouse transfer
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> RejectAsync(long id, InterWarehouseTransfer_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterWarehouseTransfer_Reject_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Completes an inter-warehouse transfer (workflow transition: RECEIVED → COMPLETED)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> CompleteAsync(long id, InterWarehouseTransfer_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<InterWarehouseTransfer_Complete_DTO, InterWarehouseTransfer_Response_DTO>($"{BaseRoute}/{id}/complete", request, cancellationToken);
        }

        /// <summary>
        /// Ships an inter-warehouse transfer (alias for ShipTransferAsync)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ShipAsync(long id, InterWarehouseTransfer_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await ShipTransferAsync(id, request, cancellationToken);
        }

        /// <summary>
        /// Receives an inter-warehouse transfer (alias for ReceiveTransferAsync)
        /// </summary>
        public async Task<Result<InterWarehouseTransfer_Response_DTO>> ReceiveAsync(long id, InterWarehouseTransfer_Receive_DTO request, CancellationToken cancellationToken = default)
        {
            return await ReceiveTransferAsync(id, request, cancellationToken);
        }
    }
}
