using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for Localized Messages
    /// </summary>
    public class LocalizedMessage_ApiClient : CrudApiClientBase<LocalizedMessage_Response_DTO, LocalizedMessage_Edit_DTO>, ILocalizedMessage_ApiClient
    {
        protected override string BaseEndpoint => "api/localizedmessages";
        public LocalizedMessage_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<LocalizedMessage_Response_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetTestDataAsync(x => x.MessageKey, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<IEnumerable<LocalizedMessage_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            }
            return Result<IEnumerable<LocalizedMessage_Response_DTO>>.Success(result.Value);
        }
    }
}
