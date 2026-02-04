using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Unit of Measure operations
    /// </summary>
    public class UnitOfMeasure_ApiClient : CrudApiClientBase<UnitOfMeasure_Response_DTO, UnitOfMeasure_Response_DTO, UnitOfMeasure_Edit_DTO>, IUnitOfMeasure_ApiClient
    {
        protected override string BaseEndpoint => "api/uom";
        public UnitOfMeasure_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a unit of measure by its code
        /// </summary>
        public override async Task<Result<UnitOfMeasure_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<UnitOfMeasure_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
        /// Get all units of measure by type
        public async Task<Result<List<UnitOfMeasure_Response_DTO>>> GetByTypeAsync(string type, bool includeInactive = false, CancellationToken cancellationToken = default)

        {
            var endpoint = $"{BaseEndpoint}/by-type/{type}";
            if (includeInactive)
            {
                endpoint += "?includeInactive=true";
            }
            var result = await GetListAsync<UnitOfMeasure_Response_DTO>(endpoint, cancellationToken);
            return result.Map(items => items.ToList());
        }

        // Alias methods for domain-specific naming
        /// <summary>
        /// Gets a unit of measure by ID (alias for GetByIdAsync)
        /// </summary>
        public Task<Result<UnitOfMeasure_Response_DTO>> GetUnitOfMeasureByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets all units of measure (alias for GetAllAsync)
        /// </summary>
        public Task<Result<IEnumerable<UnitOfMeasure_Response_DTO>>> GetUnitsOfMeasureAsync(CancellationToken cancellationToken = default)
        {
            return GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Creates a new unit of measure (alias for CreateAsync)
        /// </summary>
        public Task<Result<UnitOfMeasure_Response_DTO>> CreateUnitOfMeasureAsync(UnitOfMeasure_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return CreateAsync(dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing unit of measure (alias for UpdateAsync)
        /// </summary>
        public Task<Result<UnitOfMeasure_Response_DTO>> UpdateUnitOfMeasureAsync(long id, UnitOfMeasure_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return UpdateAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a unit of measure (alias for DeleteAsync)
        /// </summary>
        public Task<Result<Unit>> DeleteUnitOfMeasureAsync(long id, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(id, cancellationToken);
        }
    }
}
