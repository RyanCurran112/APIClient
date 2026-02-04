using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Sales;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Sales;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Sales
{
    /// <summary>
    /// API Client for Customer operations
    /// </summary>
    public class Customer_ApiClient : CrudApiClientBase<Customer_Response_DTO, Customer_Response_DTO, Customer_Edit_DTO>, ICustomer_ApiClient
    {
        protected override string BaseEndpoint => "api/customers";
        public Customer_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Get a customer by its code
        /// </summary>
        public override async Task<Result<Customer_Response_DTO>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)

        {
            return await GetAsync<Customer_Response_DTO>($"{BaseEndpoint}/by-code/{code}", cancellationToken);
        }
    }
}
