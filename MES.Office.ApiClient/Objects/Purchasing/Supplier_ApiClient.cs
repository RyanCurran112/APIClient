using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Purchasing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Purchasing
{
    /// <summary>
    /// API Client for Supplier operations
    /// </summary>
    public class Supplier_ApiClient : CrudApiClientBase<Supplier_Response_DTO, Supplier_Response_DTO, Supplier_Edit_DTO>, ISupplier_ApiClient
    {
        protected override string BaseEndpoint => "api/suppliers";
        public Supplier_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
    }
}
