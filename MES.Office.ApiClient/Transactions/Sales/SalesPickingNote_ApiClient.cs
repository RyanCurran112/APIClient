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
    /// API Client implementation for Sales Picking Note operations
    /// Provides HTTP client access to all sales picking note endpoints
    /// </summary>
    public class SalesPickingNote_ApiClient : CrudApiClientBase<SalesPickingNote_Response_DTO, SalesPickingNote_Response_DTO, SalesPickingNote_Edit_DTO>, ISalesPickingNote_ApiClient
    {
        protected override string BaseEndpoint => "api/sales/picking-notes";
        public SalesPickingNote_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        // Override GetAllAsync to return list of Response DTOs
        public override async Task<Result<IEnumerable<SalesPickingNote_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<SalesPickingNote_Response_DTO>(BaseEndpoint, cancellationToken);
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
        public async Task<Result<ValidationResult_DTO>> ValidateAsync(SalesPickingNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<SalesPickingNote_Edit_DTO, ValidationResult_DTO>($"{BaseEndpoint}/validate", dto, cancellationToken);
        }
        // WORKFLOW OPERATIONS
        public async Task<Result<SalesPickingNote_Response_DTO>> StartAsync(long id, SalesPickingNote_Start_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesPickingNote_Start_DTO, SalesPickingNote_Response_DTO>($"{BaseEndpoint}/{id}/start", request, cancellationToken);
        }

        public async Task<Result<SalesPickingNote_Response_DTO>> CompleteAsync(long id, SalesPickingNote_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesPickingNote_Complete_DTO, SalesPickingNote_Response_DTO>($"{BaseEndpoint}/{id}/complete", request, cancellationToken);
        }

        public async Task<Result<SalesPickingNote_Response_DTO>> CancelAsync(long id, SalesPickingNote_Cancel_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesPickingNote_Cancel_DTO, SalesPickingNote_Response_DTO>($"{BaseEndpoint}/{id}/cancel", request, cancellationToken);
        }

        public async Task<Result<SalesPickingNote_Response_DTO>> ScanBarcodeAsync(long id, SalesPickingNote_Scan_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesPickingNote_Scan_DTO, SalesPickingNote_Response_DTO>($"{BaseEndpoint}/{id}/scan", request, cancellationToken);
        }

        public Task<Result<SalesPickingNote_Response_DTO>> CompletePickingAsync(long id, SalesPickingNote_Complete_DTO request, CancellationToken cancellationToken = default)
        {
            return CompleteAsync(id, request, cancellationToken);
        }

        public Task<Result<SalesPickingNote_Response_DTO>> StartPickingAsync(long id, SalesPickingNote_Start_DTO request, CancellationToken cancellationToken = default)
        {
            return StartAsync(id, request, cancellationToken);
        }

        public async Task<Result<List<WorkflowHistory_Response_DTO>>> GetWorkflowHistoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await GetListAsync<WorkflowHistory_Response_DTO>($"{BaseEndpoint}/{id}/workflow-history", cancellationToken);
            return result.Map(items => items.ToList());
        }

        /// <summary>
        /// Searches Sales Picking Notes with typed filter
        /// </summary>
        public async Task<Result<IEnumerable<SalesPickingNote_Response_DTO>>> SearchAsync(SalesPickingNote_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostAsync<SalesPickingNote_Filter_DTO, IEnumerable<SalesPickingNote_Response_DTO>>($"{BaseEndpoint}/search", filter, cancellationToken);
        }
    }
}
