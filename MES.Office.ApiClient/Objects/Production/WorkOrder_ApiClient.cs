using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Production;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Production;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Production
{
    /// <summary>
    /// API Client for WorkOrder operations
    /// </summary>
    public class WorkOrder_ApiClient : CrudApiClientBase<WorkOrder_Response_DTO, WorkOrder_Response_DTO, WorkOrder_Edit_DTO>, IWorkOrder_ApiClient
    {
        protected override string BaseEndpoint => "api/workorders";
        public WorkOrder_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a work order by its code
        /// </summary>
        public override async Task<Result<WorkOrder_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<WorkOrder_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
        /// Get work orders by status
        public async Task<Result<List<WorkOrder_Response_DTO>>> GetByStatusAsync(string status, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-status/{status}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<WorkOrder_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get all work orders for a specific machine
        public async Task<Result<List<WorkOrder_Response_DTO>>> GetByMachineAsync(long machineId, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-machine/{machineId}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<WorkOrder_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get all in-progress work orders
        public async Task<Result<List<WorkOrder_Response_DTO>>> GetInProgressAsync(bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/in-progress";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<WorkOrder_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Get all overdue work orders
        public async Task<Result<List<WorkOrder_Response_DTO>>> GetOverdueAsync(bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/overdue";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<WorkOrder_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }
        /// Release a work order for execution
        public async Task<Result<WorkOrder_Response_DTO>> ReleaseAsync(long id, string userId, CancellationToken cancellationToken = default)

        {
            return await PostAsync<object, WorkOrder_Response_DTO>($"{BaseEndpoint}/{id}/release", new { userId }, cancellationToken);
        }
        /// Start a work order
        public async Task<Result<WorkOrder_Response_DTO>> StartAsync(long id, string userId, CancellationToken cancellationToken = default)

        {
            return await PostAsync<object, WorkOrder_Response_DTO>($"{BaseEndpoint}/{id}/start", new { userId }, cancellationToken);
        }
        /// Complete a work order
        public async Task<Result<WorkOrder_Response_DTO>> CompleteAsync(long id, decimal? completedQuantity, decimal? scrappedQuantity, string userId, CancellationToken cancellationToken = default)

        {
            return await PostAsync<object, WorkOrder_Response_DTO>($"{BaseEndpoint}/{id}/complete", new { completedQuantity, scrappedQuantity, userId }, cancellationToken);
        }
        /// Close a work order
        public async Task<Result<WorkOrder_Response_DTO>> CloseAsync(long id, string userId, CancellationToken cancellationToken = default)

        {
            return await PostAsync<object, WorkOrder_Response_DTO>($"{BaseEndpoint}/{id}/close", new { userId }, cancellationToken);
        }
        /// Cancel a work order
        public async Task<Result<WorkOrder_Response_DTO>> CancelAsync(long id, string reason, string userId, CancellationToken cancellationToken = default)

        {
            return await PostAsync<object, WorkOrder_Response_DTO>($"{BaseEndpoint}/{id}/cancel", new { reason, userId }, cancellationToken);
        }
    }
}
