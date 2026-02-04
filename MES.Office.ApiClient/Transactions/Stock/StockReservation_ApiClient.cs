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
    /// API Client for Stock Reservation operations
    /// </summary>
    public class StockReservation_ApiClient : ApiClientBase, IStockReservation_ApiClient
    {
        private const string BaseRoute = "api/stock/reservations";
        protected override string BaseEndpoint => BaseRoute;
        public StockReservation_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // ========================================
        // CRUD Operations (from IApiClient)
        /// <summary>
        /// Gets a stock reservation by ID
        /// </summary>
        public async Task<Result<StockReservation_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockReservation_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Gets all stock reservations
        public async Task<Result<IEnumerable<StockReservation_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockReservation_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Creates a new stock reservation
        public async Task<Result<StockReservation_Response_DTO>> CreateAsync(StockReservation_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockReservation_Edit_DTO, StockReservation_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing stock reservation
        public async Task<Result<StockReservation_Response_DTO>> UpdateAsync(long id, StockReservation_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<StockReservation_Edit_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a stock reservation
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        // Additional Query Operations
        /// Retrieves a stock reservation by reservation number
        public async Task<Result<StockReservation_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<StockReservation_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }
        /// Retrieves stock reservation summaries for list views
        public async Task<Result<List<StockReservation_Summary_DTO>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<StockReservation_Summary_DTO>($"{BaseRoute}/summaries", cancellationToken);
            if (!result.IsSuccess)
                return Result<List<StockReservation_Summary_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<List<StockReservation_Summary_DTO>>.Success(result.Value?.ToList() ?? new List<StockReservation_Summary_DTO>());
        }
        /// Searches stock reservations with optional filters
        public async Task<Result<List<StockReservation_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, StockReservation_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            if (!result.IsSuccess)
                return Result<List<StockReservation_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<List<StockReservation_Response_DTO>>.Success(result.Value?.ToList() ?? new List<StockReservation_Response_DTO>());
        }
        // Workflow Operations
        /// Fulfills a stock reservation (workflow transition: ACTIVE → FULFILLED)
        /// Creates stock movements and allocates reserved stock
        public async Task<Result<StockReservation_Response_DTO>> FulfillReservationAsync(long id, StockReservation_Fulfill_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockReservation_Fulfill_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/fulfill", request, cancellationToken);
        }
        /// Expires a stock reservation (workflow transition: ACTIVE → EXPIRED)
        /// Releases reserved stock back to available inventory
        public async Task<Result<StockReservation_Response_DTO>> ExpireReservationAsync(long id, StockReservation_Expire_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockReservation_Expire_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/expire", request, cancellationToken);
        }
        /// Cancels a stock reservation (workflow transition: ACTIVE → CANCELLED)
        public async Task<Result<StockReservation_Response_DTO>> CancelReservationAsync(long id, StockReservation_Cancel_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<StockReservation_Cancel_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }
        /// Retrieves workflow history for a stock reservation
        public async Task<Result<List<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            if (!result.IsSuccess)
                return Result<List<WorkflowHistoryDto>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<List<WorkflowHistoryDto>>.Success(result.Value?.ToList() ?? new List<WorkflowHistoryDto>());
        }

        // Workflow Operations

        /// <summary>
        /// Approves a stock reservation (workflow transition: PENDING → APPROVED)
        /// </summary>
        public async Task<Result<StockReservation_Response_DTO>> ApproveAsync(long id, StockReservation_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockReservation_Approve_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a stock reservation (workflow transition: PENDING → REJECTED)
        /// </summary>
        public async Task<Result<StockReservation_Response_DTO>> RejectAsync(long id, StockReservation_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockReservation_Reject_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Executes a reservation (reserves the stock) (workflow transition: APPROVED → ACTIVE)
        /// </summary>
        public async Task<Result<StockReservation_Response_DTO>> ReserveAsync(long id, StockReservation_Reserve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockReservation_Reserve_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/reserve", request, cancellationToken);
        }

        /// <summary>
        /// Releases a stock reservation (workflow transition: ACTIVE → RELEASED)
        /// </summary>
        public async Task<Result<StockReservation_Response_DTO>> ReleaseAsync(long id, StockReservation_Release_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockReservation_Release_DTO, StockReservation_Response_DTO>($"{BaseRoute}/{id}/release", request, cancellationToken);
        }
    }
}
