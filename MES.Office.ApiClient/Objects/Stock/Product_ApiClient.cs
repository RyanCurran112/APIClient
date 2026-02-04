using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Product operations
    /// </summary>
    public class Product_ApiClient : CrudApiClientBase<Product_Response_DTO, Product_Response_DTO, Product_Edit_DTO>, IProduct_ApiClient
    {
        protected override string BaseEndpoint => "api/products";
        public Product_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Search products with advanced filtering
        /// </summary>
        public async Task<Result<PagedResult<Product_Summary_DTO>>> SearchAsync(Product_Filter_DTO filter, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Product_Filter_DTO, PagedResult<Product_Summary_DTO>>($"{BaseEndpoint}/search", filter, cancellationToken);
        }
    }
}
