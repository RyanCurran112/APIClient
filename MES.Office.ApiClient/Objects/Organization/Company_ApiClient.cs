using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Organization;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Organization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Organization
{
    /// <summary>
    /// API Client for Company operations
    /// </summary>
    public class Company_ApiClient : CrudApiClientBase<Company_Response_DTO, Company_Response_DTO, Company_Edit_DTO>, ICompany_ApiClient
    {
        protected override string BaseEndpoint => "api/companies";

        public Company_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get a company by its code
        /// </summary>
        public override async Task<Result<Company_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Company_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }

        /// <summary>
        /// Get simple list of companies for dropdowns
        /// </summary>
        public async Task<Result<List<Company_Lookup_DTO>>> GetLookupAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var queryString = includeInactive ? "?includeInactive=true" : "";
            return await GetAsync<List<Company_Lookup_DTO>>($"{BaseEndpoint}/simple{queryString}", cancellationToken);
        }
    }
}
