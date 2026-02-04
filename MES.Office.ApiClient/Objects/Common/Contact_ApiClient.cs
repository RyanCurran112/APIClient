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
    /// API Client for Contact operations
    /// Contacts use owner-based routing pattern
    /// </summary>
    public class Contact_ApiClient : ApiClientBase, IContact_ApiClient
    {
        protected override string BaseEndpoint => "api/contacts";

        public Contact_ApiClient(
            HttpClient httpClient,
            ILogger<Contact_ApiClient> logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Get all contacts for an owner entity
        /// </summary>
        public async Task<Result<IEnumerable<Contact_Response_DTO>>> GetByOwnerAsync(
            AddressOwnerType ownerType,
            long ownerId,
            bool includeInactive = false,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}?includeInactive={includeInactive}";
            return await GetListAsync<Contact_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get simplified contact list for dropdown selection
        /// </summary>
        public async Task<Result<IEnumerable<Contact_Simple_DTO>>> GetLookupByOwnerAsync(
            AddressOwnerType ownerType,
            long ownerId,
            bool includeInactive = false,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/lookup?includeInactive={includeInactive}";
            return await GetListAsync<Contact_Simple_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get primary contact for an owner
        /// </summary>
        public async Task<Result<Contact_Response_DTO>> GetPrimaryAsync(
            AddressOwnerType ownerType,
            long ownerId,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/primary";
            return await GetAsync<Contact_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get contact by ID
        /// </summary>
        public async Task<Result<Contact_Response_DTO>> GetByIdAsync(
            long id,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}";
            return await GetAsync<Contact_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Get contact by email
        /// </summary>
        public async Task<Result<Contact_Response_DTO>> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-email?email={Uri.EscapeDataString(email)}";
            return await GetAsync<Contact_Response_DTO>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Create a new contact for an owner
        /// </summary>
        public async Task<Result<Contact_Response_DTO>> CreateAsync(
            AddressOwnerType ownerType,
            long ownerId,
            Contact_Edit_DTO dto,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}";
            return await PostAsync<Contact_Edit_DTO, Contact_Response_DTO>(endpoint, dto, cancellationToken);
        }

        /// <summary>
        /// Update an existing contact
        /// </summary>
        public async Task<Result<bool>> UpdateAsync(
            long id,
            Contact_Edit_DTO dto,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/{id}";
            var result = await PutAsync<Contact_Edit_DTO, object>(endpoint, dto, cancellationToken);
            return result.IsSuccess
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(result.ErrorCode, result.ErrorMessage);
        }

        /// <summary>
        /// Delete a contact
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
        /// Set a contact as primary for its owner
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
        /// Bulk update contacts for an owner
        /// </summary>
        public async Task<Result<IEnumerable<Contact_Response_DTO>>> BulkUpdateAsync(
            AddressOwnerType ownerType,
            long ownerId,
            List<Contact_Edit_DTO> contacts,
            CancellationToken cancellationToken = default)
        {
            var endpoint = $"{BaseEndpoint}/by-owner/{ownerType}/{ownerId}/bulk";
            return await PostListAsync<List<Contact_Edit_DTO>, Contact_Response_DTO>(endpoint, contacts, cancellationToken);
        }
    }
}
