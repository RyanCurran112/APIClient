using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Purchasing
{
    /// <summary>
    /// ApiClient implementation for Purchase Return operations
    /// Route: api/purchasing/returns
    /// </summary>
    public class PurchaseReturn_ApiClient : ApiClientBase, IPurchaseReturn_ApiClient
    {
        private const string BaseRoute = "api/purchasing/returns";
        protected override string BaseEndpoint => BaseRoute;
        public PurchaseReturn_ApiClient(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        #region Standard CRUD Operations
        /// <summary>
        /// Retrieves a purchase return by ID
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Retrieves all purchase returns
        public async Task<Result<IEnumerable<PurchaseReturn_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<PurchaseReturn_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Retrieves purchase returns with filtering
        public async Task<Result<IEnumerable<PurchaseReturn_Response_DTO>>> GetFilteredAsync(
            string statusCode = null,
            long? supplierId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (supplierId.HasValue)
                queryParams.Add($"supplierId={supplierId.Value}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");
            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<PurchaseReturn_Response_DTO>($"{BaseRoute}{queryString}", cancellationToken);
        }
        /// Creates a new purchase return
        public async Task<Result<PurchaseReturn_Response_DTO>> CreateAsync(PurchaseReturn_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<PurchaseReturn_Edit_DTO, PurchaseReturn_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing purchase return
        public async Task<Result<PurchaseReturn_Response_DTO>> UpdateAsync(long id, PurchaseReturn_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<PurchaseReturn_Edit_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a purchase return
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        #endregion
        #region Bulk Operations
        /// Bulk deletes multiple purchase returns
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseRoute}/bulk", ids, cancellationToken);
        }
        /// Changes workflow status for multiple purchase returns
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)

        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseRoute}/workflow-status", request, cancellationToken);
        }
        #endregion
        #region Validation
        /// Validates a purchase return without saving
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(PurchaseReturn_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<PurchaseReturn_Edit_DTO, ValidationResult_DTO>($"{BaseRoute}/validate", dto, cancellationToken);
        }
        #endregion
        #region Workflow Operations
        /// <summary>
        /// Submits a purchase return for approval
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> SubmitForApprovalAsync(long id, PurchaseReturn_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Submit_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Submits a purchase return (alias for SubmitForApprovalAsync)
        /// </summary>
        public Task<Result<PurchaseReturn_Response_DTO>> SubmitAsync(long id, PurchaseReturn_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return SubmitForApprovalAsync(id, request, cancellationToken);
        }

        /// <summary>
        /// Approves a purchase return
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> ApproveAsync(long id, PurchaseReturn_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Approve_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a purchase return
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> RejectAsync(long id, PurchaseReturn_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Reject_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Ships return to supplier
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> ShipAsync(long id, PurchaseReturn_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Ship_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/ship", request, cancellationToken);
        }

        /// <summary>
        /// Executes the actual stock return operation (workflow transition)
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> ReturnAsync(long id, PurchaseReturn_Return_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Return_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/return", request, cancellationToken);
        }

        /// <summary>
        /// Records credit received from supplier
        /// </summary>
        public async Task<Result<PurchaseReturn_Response_DTO>> RecordCreditAsync(long id, PurchaseReturn_Credit_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Credit_DTO, PurchaseReturn_Response_DTO>($"{BaseRoute}/{id}/credit", request, cancellationToken);
        }
        #endregion
        #region History
        /// Gets workflow history for a purchase return
        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)

        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseRoute}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
        #endregion

        #region Search
        /// <summary>
        /// Searches Purchase Returns with typed filter
        /// </summary>
        public async Task<Result<IEnumerable<PurchaseReturn_Response_DTO>>> SearchAsync(PurchaseReturn_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostAsync<PurchaseReturn_Filter_DTO, IEnumerable<PurchaseReturn_Response_DTO>>($"{BaseRoute}/search", filter, cancellationToken);
        }
        #endregion
    }
}
