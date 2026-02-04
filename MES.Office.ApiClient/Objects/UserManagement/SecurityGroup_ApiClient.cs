using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.UserManagement;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.UserManagement
{
    /// <summary>
    /// API Client for SecurityGroup operations
    /// </summary>
    public class SecurityGroup_ApiClient : CrudApiClientBase<SecurityGroup_Response_DTO, SecurityGroup_Response_DTO, SecurityGroup_Edit_DTO>, ISecurityGroup_ApiClient
    {
        protected override string BaseEndpoint => "api/securitygroups";

        public SecurityGroup_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get a security group by its code
        /// </summary>
        public override async Task<Result<SecurityGroup_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<SecurityGroup_Response_DTO>($"{BaseEndpoint}/code/{code}", cancellationToken);
        }

        /// <summary>
        /// Get simple list of security groups for dropdowns
        /// </summary>
        public async Task<Result<List<SecurityGroup_Lookup_DTO>>> GetLookupAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var queryString = includeInactive ? "?includeInactive=true" : "";
            return await GetAsync<List<SecurityGroup_Lookup_DTO>>($"{BaseEndpoint}/simple{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get security groups by company
        /// </summary>
        public async Task<Result<IEnumerable<SecurityGroup_Summary_DTO>>> GetByCompanyAsync(long companyId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<SecurityGroup_Summary_DTO>>($"{BaseEndpoint}/company/{companyId}", cancellationToken);
        }

        /// <summary>
        /// Get system security groups
        /// </summary>
        public async Task<Result<IEnumerable<SecurityGroup_Summary_DTO>>> GetSystemGroupsAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<SecurityGroup_Summary_DTO>>($"{BaseEndpoint}/system", cancellationToken);
        }

        /// <summary>
        /// Get security group with denied permissions by code
        /// </summary>
        public async Task<Result<SecurityGroup_WithPermissions_DTO>> GetWithPermissionsAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<SecurityGroup_WithPermissions_DTO>($"{BaseEndpoint}/permissions/{code}", cancellationToken);
        }
    }
}
