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
    /// API Client implementation for Sales Return operations
    /// Provides HTTP client access to all sales return endpoints
    /// </summary>
    public class SalesReturn_ApiClient : CrudApiClientBase<SalesReturn_Response_DTO, SalesReturn_Response_DTO, SalesReturn_Edit_DTO>, ISalesReturn_ApiClient
    {
        protected override string BaseEndpoint => "api/sales/returns";
        public SalesReturn_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // Override GetAllAsync to return list of Response DTOs
        public override async Task<Result<IEnumerable<SalesReturn_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<SalesReturn_Response_DTO>(BaseEndpoint, cancellationToken);
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
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(SalesReturn_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesReturn_Edit_DTO, ValidationResult_DTO>($"{BaseEndpoint}/validate", dto, cancellationToken);
        }
        // WORKFLOW OPERATIONS
        public async Task<Result<SalesReturn_Response_DTO>> SubmitForApprovalAsync(long id, SalesReturn_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Submit_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/submit", request, cancellationToken);
        }

        public Task<Result<SalesReturn_Response_DTO>> SubmitAsync(long id, SalesReturn_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return SubmitForApprovalAsync(id, request, cancellationToken);
        }

        public async Task<Result<SalesReturn_Response_DTO>> ApproveAsync(long id, SalesReturn_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Approve_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/approve", request, cancellationToken);
        }

        public async Task<Result<SalesReturn_Response_DTO>> RejectAsync(long id, SalesReturn_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Reject_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/reject", request, cancellationToken);
        }

        public async Task<Result<SalesReturn_Response_DTO>> ReceiveAsync(long id, SalesReturn_Receive_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Receive_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/receive", request, cancellationToken);
        }

        public async Task<Result<SalesReturn_Response_DTO>> ProcessAsync(long id, SalesReturn_Process_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Process_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/process", request, cancellationToken);
        }

        public Task<Result<SalesReturn_Response_DTO>> ReceiveGoodsAsync(long id, SalesReturn_Receive_DTO request, CancellationToken cancellationToken = default)
        {
            return ReceiveAsync(id, request, cancellationToken);
        }

        public Task<Result<SalesReturn_Response_DTO>> ProcessReturnAsync(long id, SalesReturn_Process_DTO request, CancellationToken cancellationToken = default)
        {
            return ProcessAsync(id, request, cancellationToken);
        }

        public async Task<Result<SalesReturn_Response_DTO>> CancelAsync(long id, SalesReturn_Cancel_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesReturn_Cancel_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/{id}/cancel", request, cancellationToken);
        }

        public async Task<Result<IEnumerable<SalesReturn_Response_DTO>>> SearchAsync(SalesReturn_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostListAsync<SalesReturn_Filter_DTO, SalesReturn_Response_DTO>($"{BaseEndpoint}/search", filter, cancellationToken);
        }

        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseEndpoint}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }
    }
}
