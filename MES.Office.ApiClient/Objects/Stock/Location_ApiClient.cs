using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Location operations
    /// </summary>
    public class Location_ApiClient : CrudApiClientBase<Location_Response_DTO, Location_Response_DTO, Location_Edit_DTO>, ILocation_ApiClient
    {
        protected override string BaseEndpoint => "api/locations";
        public Location_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a location by its code
        /// </summary>
        public override async Task<Result<Location_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Location_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
        /// Get all locations for a specific site
        public async Task<Result<List<Location_Response_DTO>>> GetBySiteAsync(long siteId, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-site/{siteId}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<Location_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }

        // Alias methods for domain-specific naming
        /// <summary>
        /// Gets a location by ID (alias for GetByIdAsync)
        /// </summary>
        public Task<Result<Location_Response_DTO>> GetLocationByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets all locations (alias for GetAllAsync)
        /// </summary>
        public Task<Result<IEnumerable<Location_Response_DTO>>> GetLocationsAsync(CancellationToken cancellationToken = default)
        {
            return GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Creates a new location (alias for CreateAsync)
        /// </summary>
        public Task<Result<Location_Response_DTO>> CreateLocationAsync(Location_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return CreateAsync(dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing location (alias for UpdateAsync)
        /// </summary>
        public Task<Result<Location_Response_DTO>> UpdateLocationAsync(long id, Location_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return UpdateAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a location (alias for DeleteAsync)
        /// </summary>
        public Task<Result<Unit>> DeleteLocationAsync(long id, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(id, cancellationToken);
        }
    }
}
