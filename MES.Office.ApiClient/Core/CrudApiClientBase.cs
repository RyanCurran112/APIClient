using MES.Office.WebAPI.Contracts.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Core
{
    /// <summary>
    /// Generic base class for CRUD API clients, eliminating code duplication across all clients
    /// </summary>
    /// <typeparam name="TSummaryDto">DTO used for list operations (e.g., Summary_DTO or main DTO)</typeparam>
    /// <typeparam name="TResponseDto">DTO used for single item operations (e.g., Response_DTO or main DTO)</typeparam>
    /// <typeparam name="TEditDto">DTO used for create/update operations (e.g., Edit_DTO)</typeparam>
    public abstract class CrudApiClientBase<TSummaryDto, TResponseDto, TEditDto> : ApiClientBase
        where TSummaryDto : class
        where TResponseDto : class
        where TEditDto : class
    {
        protected CrudApiClientBase(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }

        /// <summary>
        /// Gets all items from the API
        /// </summary>
        public virtual async Task<Result<IEnumerable<TSummaryDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<TSummaryDto>(BaseEndpoint, cancellationToken);
        }

        /// <summary>
        /// Gets a single item by ID
        /// </summary>
        public virtual async Task<Result<TResponseDto>> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TResponseDto>($"{BaseEndpoint}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets a single item by code
        /// </summary>
        public virtual async Task<Result<TResponseDto>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TResponseDto>($"{BaseEndpoint}/code/{code}", cancellationToken);
        }

        /// <summary>
        /// Creates a new item
        /// </summary>
        public virtual async Task<Result<TResponseDto>> CreateAsync(TEditDto dto, CancellationToken cancellationToken = default)
        {
            return await PostAsync<TEditDto, TResponseDto>(BaseEndpoint, dto, cancellationToken);
        }

        /// <summary>
        /// Updates an existing item
        /// </summary>
        public virtual async Task<Result<TResponseDto>> UpdateAsync(long id, TEditDto dto, CancellationToken cancellationToken = default)
        {
            return await PutAsync<TEditDto, TResponseDto>($"{BaseEndpoint}/{id}", dto, cancellationToken);
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        public virtual async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets test data records (with test prefix) - requires code property selector
        /// </summary>
        protected async Task<Result<IEnumerable<TSummaryDto>>> GetTestDataAsync(Func<TSummaryDto, string> codeSelector, CancellationToken cancellationToken = default)
        {
            var result = await GetAllAsync(cancellationToken);
            if (!result.IsSuccess)
            {
                return Result<IEnumerable<TSummaryDto>>.Failure(result.ErrorCode, result.ErrorMessage);
            }

            var testData = FilterTestData(result.Value, codeSelector);
            Logger?.LogInformation("Found {Count} test records", testData.Count);

            return Result<IEnumerable<TSummaryDto>>.Success(testData);
        }
    }

    /// <summary>
    /// Simplified CRUD base for entities that use a single DTO type for all operations
    /// </summary>
    /// <typeparam name="TDto">The single DTO used for all operations</typeparam>
    /// <typeparam name="TEditDto">DTO used for create/update operations</typeparam>
    public abstract class CrudApiClientBase<TDto, TEditDto> : CrudApiClientBase<TDto, TDto, TEditDto>
        where TDto : class
        where TEditDto : class
    {
        protected CrudApiClientBase(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
    }
}
