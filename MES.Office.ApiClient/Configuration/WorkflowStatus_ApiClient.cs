using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Common;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for Workflow Status
    /// </summary>
    public class WorkflowStatus_ApiClient : CrudApiClientBase<WorkflowStatus_Response_DTO, WorkflowStatus_Edit_DTO>, IWorkflowStatus_ApiClient
    {
        protected override string BaseEndpoint => "api/workflows/statuses";
        public WorkflowStatus_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<WorkflowStatus_Response_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetTestDataAsync(x => x.Code, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<IEnumerable<WorkflowStatus_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            }
            return Result<IEnumerable<WorkflowStatus_Response_DTO>>.Success(result.Value);
        }
    }
}
