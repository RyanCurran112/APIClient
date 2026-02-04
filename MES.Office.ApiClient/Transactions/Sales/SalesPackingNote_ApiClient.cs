using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Sales;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Sales;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Sales
{
    /// <summary>
    /// API Client for Sales Packing Note operations
    /// </summary>
    public class SalesPackingNote_ApiClient : ApiClientBase, ISalesPackingNote_ApiClient
    {
        private const string BaseRoute = "api/sales/packing-notes";

        protected override string BaseEndpoint => BaseRoute;

        public SalesPackingNote_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        // ========================================
        // CRUD Operations (from IApiClient)
        // ========================================

        /// <summary>
        /// Gets a sales packing note by ID
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets all sales packing notes
        /// </summary>
        public async Task<Result<IEnumerable<SalesPackingNote_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<SalesPackingNote_Response_DTO>(BaseRoute, cancellationToken);
        }

        /// <summary>
        /// Creates a new sales packing note
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> CreateAsync(SalesPackingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_Edit_DTO, SalesPackingNote_Response_DTO>(BaseRoute, dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing sales packing note
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> UpdateAsync(long id, SalesPackingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<SalesPackingNote_Edit_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a sales packing note
        /// </summary>
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }

        // ========================================
        // Additional Query Operations
        // ========================================

        /// <summary>
        /// Retrieves a sales packing note by transaction number
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> GetByNumberAsync(string number, CancellationToken cancellationToken = default)

        {
            return await GetAsync<SalesPackingNote_Response_DTO>($"{BaseRoute}/number/{number}", cancellationToken);
        }

        /// <summary>
        /// Retrieves sales packing note summaries for list views
        /// </summary>
        public async Task<Result<IEnumerable<object>>> GetSummariesAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<object>($"{BaseRoute}/summaries", cancellationToken);
        }

        /// <summary>
        /// Searches sales packing notes with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<SalesPackingNote_Response_DTO>>> SearchAsync(Dictionary<string, object> filters, CancellationToken cancellationToken = default)

        {
            var result = await PostListAsync<Dictionary<string, object>, SalesPackingNote_Response_DTO>($"{BaseRoute}/search", filters, cancellationToken);
            if (!result.IsSuccess)
                return Result<IEnumerable<SalesPackingNote_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            return Result<IEnumerable<SalesPackingNote_Response_DTO>>.Success(result.Value);
        }

        // ========================================
        // Workflow Operations
        // ========================================

        /// <summary>
        /// Submits a sales packing note for approval (workflow transition: DRAFT → PENDING_APPROVAL)
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> SubmitForApprovalAsync(long id, SalesPackingNote_Submit_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_Submit_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Approves a sales packing note (workflow transition: PENDING_APPROVAL → APPROVED)
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> ApproveAsync(long id, SalesPackingNote_Approve_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_Approve_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a sales packing note (workflow transition: PENDING_APPROVAL → REJECTED)
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> RejectAsync(long id, SalesPackingNote_Reject_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_Reject_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Starts the packing process (workflow transition: APPROVED → IN_PROGRESS)
        /// Assigns a packer and initializes the packing workflow
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> StartPackingAsync(long id, SalesPackingNote_StartPacking_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_StartPacking_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/start-packing", request, cancellationToken);
        }

        /// <summary>
        /// Completes the packing process (workflow transition: IN_PROGRESS → COMPLETED)
        /// Finalizes all boxes, validates quantities, and prepares for shipment
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> CompletePackingAsync(long id, SalesPackingNote_CompletePacking_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_CompletePacking_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/complete-packing", request, cancellationToken);
        }

        /// <summary>
        /// Cancels a sales packing note (workflow transition: Any → CANCELLED)
        /// Reverses any stock movements if applicable
        /// </summary>
        public async Task<Result<SalesPackingNote_Response_DTO>> CancelAsync(long id, SalesPackingNote_Cancel_Workflow_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_Cancel_Workflow_DTO, SalesPackingNote_Response_DTO>($"{BaseRoute}/{id}/cancel", request, cancellationToken);
        }

        /// <summary>
        /// Retrieves workflow history for a sales packing note
        /// </summary>
        public async Task<Result<IEnumerable<WorkflowHistoryDto>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetListAsync<WorkflowHistoryDto>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
        }

        // ========================================
        // Packing Operations
        // ========================================

        /// <summary>
        /// Adds a new box to the packing note
        /// </summary>
        public async Task<Result<SalesPackingNote_Box_Response_DTO>> AddBoxAsync(long id, SalesPackingNote_AddBox_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_AddBox_DTO, SalesPackingNote_Box_Response_DTO>($"{BaseRoute}/{id}/boxes", request, cancellationToken);
        }

        /// <summary>
        /// Packs an item into a box
        /// Creates a box item record linking the line to the box
        /// </summary>
        public async Task<Result<SalesPackingNote_BoxItem_Response_DTO>> PackItemAsync(long id, SalesPackingNote_PackItem_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_PackItem_DTO, SalesPackingNote_BoxItem_Response_DTO>($"{BaseRoute}/{id}/pack-item", request, cancellationToken);
        }

        /// <summary>
        /// Seals a box to prevent further modifications
        /// Updates box status and records seal information
        /// </summary>
        public async Task<Result<SalesPackingNote_Box_Response_DTO>> SealBoxAsync(long id, SalesPackingNote_SealBox_DTO request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPackingNote_SealBox_DTO, SalesPackingNote_Box_Response_DTO>($"{BaseRoute}/{id}/seal-box", request, cancellationToken);
        }
    }
}
