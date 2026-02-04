using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace MES.Office.ApiClient.Configuration
{
    /// <summary>
    /// API Client for Business Rules
    /// </summary>
    public class BusinessRule_ApiClient : CrudApiClientBase<BusinessRule_Response_DTO, BusinessRule_Edit_DTO>, IBusinessRule_ApiClient
    {
        protected override string BaseEndpoint => "api/configuration/business-rules";
        public BusinessRule_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Gets test data records (with test prefix)
        /// </summary>
        public async Task<Result<IEnumerable<BusinessRule_Response_DTO>>> GetTestDataAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetTestDataAsync(x => x.Code, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<IEnumerable<BusinessRule_Response_DTO>>.Failure(result.ErrorCode, result.ErrorMessage);
            }
            return Result<IEnumerable<BusinessRule_Response_DTO>>.Success(result.Value);
        }
    }
}
