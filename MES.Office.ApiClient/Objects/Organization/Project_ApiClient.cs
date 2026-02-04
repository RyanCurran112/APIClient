using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Organization;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Organization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Organization
{
    /// <summary>
    /// API Client for Project operations
    /// </summary>
    public class Project_ApiClient : CrudApiClientBase<Project_Response_DTO, Project_Response_DTO, Project_Edit_DTO>, IProject_ApiClient
    {
        protected override string BaseEndpoint => "api/projects";
        public Project_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a project by its code
        /// </summary>
        public override async Task<Result<Project_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Project_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
