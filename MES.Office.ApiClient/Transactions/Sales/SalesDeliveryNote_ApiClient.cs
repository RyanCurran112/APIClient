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
    /// API Client implementation for Sales Delivery Note operations
    /// Provides HTTP client access to all sales delivery note endpoints
    /// </summary>
    public class SalesDeliveryNote_ApiClient : CrudApiClientBase<SalesDeliveryNote_Response_DTO, SalesDeliveryNote_Response_DTO, SalesDeliveryNote_Edit_DTO>, ISalesDeliveryNote_ApiClient
    {
        protected override string BaseEndpoint => "api/sales/delivery-notes";
        public SalesDeliveryNote_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // Override GetAllAsync to return list of Response DTOs
        public override async Task<Result<IEnumerable<SalesDeliveryNote_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<SalesDeliveryNote_Response_DTO>(BaseEndpoint, cancellationToken);
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
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(SalesDeliveryNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesDeliveryNote_Edit_DTO, ValidationResult_DTO>($"{BaseEndpoint}/validate", dto, cancellationToken);
        }
        // WORKFLOW OPERATIONS
        public async Task<Result<SalesDeliveryNote_Response_DTO>> ApproveAsync(long id, SalesDeliveryNote_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Approve_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/approve", request, cancellationToken);
        }

        public async Task<Result<SalesDeliveryNote_Response_DTO>> RejectAsync(long id, SalesDeliveryNote_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Reject_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/reject", request, cancellationToken);
        }

        public async Task<Result<SalesDeliveryNote_Response_DTO>> ShipAsync(long id, SalesDeliveryNote_Ship_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Ship_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/ship", request, cancellationToken);
        }

        public async Task<Result<SalesDeliveryNote_Response_DTO>> DispatchGoodsAsync(long id, SalesDeliveryNote_Dispatch_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Dispatch_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/dispatch", request, cancellationToken);
        }

        public async Task<Result<SalesDeliveryNote_Response_DTO>> DeliverAsync(long id, SalesDeliveryNote_Deliver_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Deliver_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/deliver", request, cancellationToken);
        }

        public Task<Result<SalesDeliveryNote_Response_DTO>> MarkAsDeliveredAsync(long id, SalesDeliveryNote_Deliver_DTO request, CancellationToken cancellationToken = default)
        {
            return DeliverAsync(id, request, cancellationToken);
        }

        public async Task<Result<SalesDeliveryNote_Response_DTO>> CancelAsync(long id, SalesDeliveryNote_Cancel_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesDeliveryNote_Cancel_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/{id}/cancel", request, cancellationToken);
        }

        public async Task<Result<IEnumerable<SalesDeliveryNote_Response_DTO>>> SearchAsync(SalesDeliveryNote_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<SalesDeliveryNote_Filter_DTO, SalesDeliveryNote_Response_DTO>($"{BaseEndpoint}/search", filter, cancellationToken);
        }

        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseEndpoint}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
    }
}
