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
    /// API Client implementation for Sales Order operations
    /// Provides HTTP client access to all sales order endpoints
    /// </summary>
    public class SalesOrder_ApiClient : CrudApiClientBase<SalesOrder_Response_DTO, SalesOrder_Response_DTO, SalesOrder_Edit_DTO>, ISalesOrder_ApiClient
    {
        protected override string BaseEndpoint => "api/sales/orders";
        public SalesOrder_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // Override GetAllAsync to return list of Response DTOs instead of Summary DTOs
        public override async Task<Result<IEnumerable<SalesOrder_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<SalesOrder_Response_DTO>(BaseEndpoint, cancellationToken);
        }
        // BULK OPERATIONS
        public async Task<Result<BulkOperationResult>> BulkDeleteAsync(List<long> ids, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync<List<long>, BulkOperationResult>($"{BaseEndpoint}/bulk", ids, cancellationToken);
        }
        public async Task<Result<BulkOperationResult>> ChangeWorkflowStatusAsync(ChangeWorkflowStatusRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<ChangeWorkflowStatusRequest, BulkOperationResult>($"{BaseEndpoint}/workflow-status", request, cancellationToken);
        }
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(SalesOrder_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Edit_DTO, ValidationResult_DTO>($"{BaseEndpoint}/validate", dto, cancellationToken);
        }
        // WORKFLOW OPERATIONS
        public async Task<Result<SalesOrder_Response_DTO>> SubmitForApprovalAsync(long id, SalesOrder_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Submit_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/submit", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> ApproveAsync(long id, SalesOrder_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Approve_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/approve", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> RejectAsync(long id, SalesOrder_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Reject_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/reject", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> PickAsync(long id, SalesOrder_Pick_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Pick_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/pick", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> ShipAsync(long id, SalesOrder_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Ship_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/ship", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> DeliverAsync(long id, SalesOrder_Deliver_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Deliver_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/deliver", request, cancellationToken);
        }

        public async Task<Result<SalesOrder_Response_DTO>> InvoiceAsync(long id, SalesOrder_Invoice_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesOrder_Invoice_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/{id}/invoice", request, cancellationToken);
        }

        public async Task<Result<IEnumerable<SalesOrder_Response_DTO>>> SearchAsync(SalesOrder_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<SalesOrder_Filter_DTO, SalesOrder_Response_DTO>($"{BaseEndpoint}/search", filter, cancellationToken);
        }

        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseEndpoint}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }

        // TRANSACTION VIEW FILTER METHODS

        /// <summary>
        /// Get sales orders with filters for transaction views
        /// </summary>
        public async Task<Result<IEnumerable<SalesOrder_Response_DTO>>> GetWithFiltersAsync(
            long? customerId = null,
            long? productId = null,
            long? siteId = null,
            long? salespersonUserId = null,
            string? statusCode = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 50,
            string? sortBy = null,
            string sortDirection = "desc",
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();

            if (customerId.HasValue)
                queryParams.Add($"customerId={customerId.Value}");
            if (productId.HasValue)
                queryParams.Add($"productId={productId.Value}");
            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (salespersonUserId.HasValue)
                queryParams.Add($"salespersonUserId={salespersonUserId.Value}");
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            if (!string.IsNullOrEmpty(sortBy))
                queryParams.Add($"sortBy={Uri.EscapeDataString(sortBy)}");
            queryParams.Add($"sortDirection={Uri.EscapeDataString(sortDirection)}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<SalesOrder_Response_DTO>($"{BaseEndpoint}{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get sales order lines by product with optional filters
        /// </summary>
        public async Task<Result<IEnumerable<SalesOrder_Line_List_DTO>>> GetLinesAsync(
            long? productId = null,
            long? customerId = null,
            long? siteId = null,
            string? statusCode = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 50,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();

            if (productId.HasValue)
                queryParams.Add($"productId={productId.Value}");
            if (customerId.HasValue)
                queryParams.Add($"customerId={customerId.Value}");
            if (siteId.HasValue)
                queryParams.Add($"siteId={siteId.Value}");
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");

            queryParams.Add($"page={page}");
            queryParams.Add($"pageSize={pageSize}");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<SalesOrder_Line_List_DTO>($"{BaseEndpoint}/lines{queryString}", cancellationToken);
        }
    }
}
