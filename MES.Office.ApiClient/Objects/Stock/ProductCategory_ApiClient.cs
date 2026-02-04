using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Objects.Stock
{
    /// <summary>
    /// API Client for Product Category operations
    /// </summary>
    public class ProductCategory_ApiClient : CrudApiClientBase<ProductCategory_Response_DTO, ProductCategory_Response_DTO, ProductCategory_Edit_DTO>, IProductCategory_ApiClient
    {
        protected override string BaseEndpoint => "api/product-categories";
        public ProductCategory_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        // Alias methods for domain-specific naming
        /// <summary>
        /// Gets a product category by ID (alias for GetByIdAsync)
        /// </summary>
        public Task<Result<ProductCategory_Response_DTO>> GetProductCategoryByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return GetByIdAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets all product categories (alias for GetAllAsync)
        /// </summary>
        public Task<Result<IEnumerable<ProductCategory_Response_DTO>>> GetProductCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return GetAllAsync(cancellationToken);
        }

        /// <summary>
        /// Creates a new product category (alias for CreateAsync)
        /// </summary>
        public Task<Result<ProductCategory_Response_DTO>> CreateProductCategoryAsync(ProductCategory_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return CreateAsync(dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing product category (alias for UpdateAsync)
        /// </summary>
        public Task<Result<ProductCategory_Response_DTO>> UpdateProductCategoryAsync(long id, ProductCategory_Edit_DTO dto, CancellationToken cancellationToken = default)
        {
            return UpdateAsync(id, dto, cancellationToken);
        }

        /// <summary>
        /// Deletes a product category (alias for DeleteAsync)
        /// </summary>
        public Task<Result<Unit>> DeleteProductCategoryAsync(long id, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(id, cancellationToken);
        }
    }
}
