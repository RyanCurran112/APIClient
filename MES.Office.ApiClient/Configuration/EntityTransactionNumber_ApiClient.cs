using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for Entity Transaction Numbers
    /// </summary>
    public class EntityTransactionNumber_ApiClient : CrudApiClientBase<EntityTransactionNumber_Response_DTO, EntityTransactionNumber_Edit_DTO>, IEntityTransactionNumber_ApiClient
    {
        protected override string BaseEndpoint => "api/configuration/transaction-numbers";
        public EntityTransactionNumber_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<EntityTransactionNumber_Response_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetTestDataAsync(x => x.TransactionType, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<IEnumerable<EntityTransactionNumber_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            }
            return Result<IEnumerable<EntityTransactionNumber_Response_DTO>>.Success(result.Value);
        }
    }
}
