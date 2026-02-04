using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.UserManagement;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.UserManagement
{
    /// <summary>
    /// API Client for User operations
    /// </summary>
    public class User_ApiClient : CrudApiClientBase<User_Response_DTO, User_Response_DTO, User_Edit_DTO>, IUser_ApiClient
    {
        protected override string BaseEndpoint => "api/users";

        public User_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get a user by their code
        /// </summary>
        public override async Task<Result<User_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<User_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }

        /// <summary>
        /// Get a user by their username
        /// </summary>
        public async Task<Result<User_Response_DTO>> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await GetAsync<User_Response_DTO>($"{BaseEndpoint}/by-username/{userName}", cancellationToken);
        }

        /// <summary>
        /// Get simple list of users for dropdowns
        /// </summary>
        public async Task<Result<List<User_Lookup_DTO>>> GetLookupAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var queryString = includeInactive ? "?includeInactive=true" : "";
            return await GetAsync<List<User_Lookup_DTO>>($"{BaseEndpoint}/simple{queryString}", cancellationToken);
        }

        /// <summary>
        /// Get all active users
        /// </summary>
        public async Task<Result<IEnumerable<User_Response_DTO>>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<User_Response_DTO>>($"{BaseEndpoint}/active", cancellationToken);
        }

        /// <summary>
        /// Get users by company
        /// </summary>
        public async Task<Result<IEnumerable<User_Response_DTO>>> GetByCompanyAsync(long companyId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<IEnumerable<User_Response_DTO>>($"{BaseEndpoint}/by-company/{companyId}", cancellationToken);
        }

        /// <summary>
        /// Assign roles to a user
        /// </summary>
        public async Task<Result<Unit>> AssignRolesAsync(long userId, List<long> roleIds, CancellationToken cancellationToken = default)
        {
            return await PostAsync<List<long>, Unit>($"{BaseEndpoint}/{userId}/roles", roleIds, cancellationToken);
        }

        /// <summary>
        /// Validate a user for WMS login
        /// </summary>
        public async Task<Result<User_Validation_Response_DTO>> ValidateUserAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await GetAsync<User_Validation_Response_DTO>($"{BaseEndpoint}/validate/{userName}", cancellationToken);
        }
    }
}
