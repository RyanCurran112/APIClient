using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Common
{
    /// <summary>
    /// API Client for AuditLogItem operations.
    /// This is a READ-ONLY client - audit logs are system-generated and cannot be modified.
    /// </summary>
    public class AuditLogItem_ApiClient : CrudApiClientBase<AuditLogItem_Response_DTO, AuditLogItem_Response_DTO, AuditLogItem_Edit_DTO>, IAuditLogItem_ApiClient
    {
        protected override string BaseEndpoint => "api/audit-logs";

        public AuditLogItem_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Creates a new audit log item - NOT SUPPORTED.
        /// Audit logs are system-generated and cannot be created manually.
        /// </summary>
        public override Task<Result<AuditLogItem_Response_DTO>> CreateAsync(AuditLogItem_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<AuditLogItem_Response_DTO>.Failure(
                "Audit logs are system-generated and cannot be created manually."));
        }

        /// <summary>
        /// Updates an audit log item - NOT SUPPORTED.
        /// Audit logs are immutable and cannot be modified.
        /// </summary>
        public override Task<Result<AuditLogItem_Response_DTO>> UpdateAsync(long id, AuditLogItem_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<AuditLogItem_Response_DTO>.Failure(
                "Audit logs are immutable and cannot be modified."));
        }

        /// <summary>
        /// Deletes an audit log item - NOT SUPPORTED.
        /// Audit logs are permanent records and cannot be deleted.
        /// </summary>
        public override Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result<Unit>.Failure(
                "Audit logs are permanent records and cannot be deleted."));
        }

        /// <summary>
        /// Get audit log items by entity type and entity ID.
        /// </summary>
        public async Task<Result<IEnumerable<AuditLogItem_Response_DTO>>> GetByEntityAsync(string entityType, long entityId, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<AuditLogItem_Response_DTO>($"{BaseEndpoint}/entity/{entityType}/{entityId}", cancellationToken);
        }

        /// <summary>
        /// Get audit log items by user ID.
        /// </summary>
        public async Task<Result<IEnumerable<AuditLogItem_Response_DTO>>> GetByUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await GetListAsync<AuditLogItem_Response_DTO>($"{BaseEndpoint}/user/{userId}", cancellationToken);
        }
    }
}
