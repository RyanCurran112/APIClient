using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Site operations
    /// </summary>
    public class Site_ApiClient : CrudApiClientBase<Site_Response_DTO, Site_Response_DTO, Site_Edit_DTO>, ISite_ApiClient
    {
        protected override string BaseEndpoint => "api/sites";
        public Site_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
    }
}
