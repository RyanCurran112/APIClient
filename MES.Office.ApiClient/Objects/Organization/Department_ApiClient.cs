using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Organization;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Organization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Organization
{
    /// <summary>
    /// API Client for Department operations
    /// </summary>
    public class Department_ApiClient : CrudApiClientBase<Department_Response_DTO, Department_Response_DTO, Department_Edit_DTO>, IDepartment_ApiClient
    {
        protected override string BaseEndpoint => "api/departments";
        public Department_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a department by its code
        /// </summary>
        public override async Task<Result<Department_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Department_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
