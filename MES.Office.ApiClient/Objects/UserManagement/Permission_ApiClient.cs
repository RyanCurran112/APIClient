using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.UserManagement;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.UserManagement
{
    /// <summary>
    /// API Client for Permission operations
    /// </summary>
    public class Permission_ApiClient : CrudApiClientBase<Permission_Response_DTO, Permission_Response_DTO, Permission_Edit_DTO>, IPermission_ApiClient
    {
        protected override string BaseEndpoint => "api/permissions";

        public Permission_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get a permission by its code
        /// </summary>
        public override async Task<Result<Permission_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Permission_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }

        /// <summary>
        /// Get simple list of permissions for dropdowns
        /// </summary>
        public async Task<Result<List<Permission_Lookup_DTO>>> GetLookupAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var queryString = includeInactive ? "?includeInactive=true" : "";
            return await GetAsync<List<Permission_Lookup_DTO>>($"{BaseEndpoint}/simple{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get permissions by category
        /// </summary>
        public async Task<Result<IEnumerable<Permission_Response_DTO>>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<Permission_Response_DTO>>($"{BaseEndpoint}/by-category/{category}", cancellationToken);
        }

        /// <summary>
        /// Get permissions by type
        /// </summary>
        public async Task<Result<IEnumerable<Permission_Response_DTO>>> GetByTypeAsync(string permissionType, CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<Permission_Response_DTO>>($"{BaseEndpoint}/by-type/{permissionType}", cancellationToken);
        }
    }
}
