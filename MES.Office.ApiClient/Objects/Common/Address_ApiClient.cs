using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Common;
using MES.Office.WebAPI.Contracts.Enums;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Common
{
    /// <summary>
    /// API Client for Address operations
    /// Addresses use owner-based routing pattern
    /// </summary>
    public class Address_ApiClient : ApiClientBase, IAddress_ApiClient
    {
        protected override string BaseEndpoint => "api/addresses";

        public Address_ApiClient(
            HttpClient httpClient,
            ILogger<Address_ApiClient> logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get all addresses for an owner entity
        /// </summary>
        public async Task<Result<IEnumerable<Address_Response_DTO>>> GetByOwnerAsync(
            AddressOwnerType ownerType,
            long ownerId,
            bool includeInactive = false,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}?includeInactive={includeInactive}";
            return await GetListAsync<Address_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get simplified address list for dropdown selection
        /// </summary>
        public async Task<Result<IEnumerable<Address_Simple_DTO>>> GetLookupByOwnerAsync(
            AddressOwnerType ownerType,
            long ownerId,
            bool includeInactive = false,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/lookup?includeInactive={includeInactive}";
            return await GetListAsync<Address_Simple_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get primary address for an owner
        /// </summary>
        public async Task<Result<Address_Response_DTO>> GetPrimaryAsync(
            AddressOwnerType ownerType,
            long ownerId,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/primary";
            return await GetAsync<Address_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get address by ID
        /// </summary>
        public async Task<Result<Address_Response_DTO>> GetByIdAsync(
            long id,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}";
            return await GetAsync<Address_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Create a new address for an owner
        /// </summary>
        public async Task<Result<Address_Response_DTO>> CreateAsync(
            AddressOwnerType ownerType,
            long ownerId,
            Address_Edit_DTO dto,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}";
            return await PostAsync<Address_Edit_DTO, Address_Response_DTO>(endpoint, dto, cancellationToken);
        }

        /// <summary>
        /// Update an existing address
        /// </summary>
        public async Task<Result<bool>> UpdateAsync(
            long id,
            Address_Edit_DTO dto,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}";
            var result = await PutAsync<Address_Edit_DTO, object>(endpoint, dto, cancellationToken);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorCode, result.ErrorMessage);
        }

        /// <summary>
        /// Delete an address
        /// </summary>
        public async Task<Result<bool>> DeleteAsync(
            long id,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}";
            var result = await DeleteAsync(endpoint, cancellationToken);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorCode, result.ErrorMessage);
        }

        /// <summary>
        /// Set an address as primary for its owner
        /// </summary>
        public async Task<Result<bool>> SetPrimaryAsync(
            long id,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}/set-primary";
            var result = await PostUnitAsync<object>(endpoint, null, cancellationToken);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorCode, result.ErrorMessage);
        }

        /// <summary>
        /// Bulk update addresses for an owner
        /// </summary>
        public async Task<Result<IEnumerable<Address_Response_DTO>>> BulkUpdateAsync(
            AddressOwnerType ownerType,
            long ownerId,
            List<Address_Edit_DTO> addresses,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/bulk";
            return await PostListAsync<List<Address_Edit_DTO>, Address_Response_DTO>(endpoint, addresses, cancellationToken);
        }
    }
}
