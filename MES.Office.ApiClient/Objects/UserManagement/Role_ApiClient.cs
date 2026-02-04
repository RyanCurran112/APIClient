using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.UserManagement;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.UserManagement
{
    /// <summary>
    /// API Client for Role operations
    /// </summary>
    public class Role_ApiClient : CrudApiClientBase<Role_Response_DTO, Role_Response_DTO, Role_Edit_DTO>, IRole_ApiClient
    {
        protected override string BaseEndpoint => "api/roles";

        public Role_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get a role by its code
        /// </summary>
        public override async Task<Result<Role_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Role_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }

        /// <summary>
        /// Get simple list of roles for dropdowns
        /// </summary>
        public async Task<Result<List<Role_Lookup_DTO>>> GetLookupAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var queryString = includeInactive ? "?includeInactive=true" : "";
            return await GetAsync<List<Role_Lookup_DTO>>($"{BaseEndpoint}/simple{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get system roles only
        /// </summary>
        public async Task<Result<IEnumerable<Role_Response_DTO>>> GetSystemRolesAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<Role_Response_DTO>>($"{BaseEndpoint}/system", cancellationToken);
        }

        /// <summary>
        /// Get permissions assigned to a role
        /// </summary>
        public async Task<Result<IEnumerable<Permission_Response_DTO>>> GetRolePermissionsAsync(long roleId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<Permission_Response_DTO>>($"{BaseEndpoint}/{roleId}/permissions", cancellationToken);
        }

        /// <summary>
        /// Assign permissions to a role
        /// </summary>
        public async Task<Result<Unit>> AssignPermissionsAsync(long roleId, List<long> permissionIds, CancellationToken cancellationToken = default)
        {
            return await PostAsync<List<long>, Unit>($"{BaseEndpoint}/{roleId}/permissions", permissionIds, cancellationToken);
        }
    }
}
