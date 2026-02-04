using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Purchasing
{
    /// <summary>
    /// ApiClient implementation for Purchase Delivery Note operations
    /// Route: api/purchasing/delivery-notes
    /// </summary>
    public class PurchaseDeliveryNote_ApiClient : ApiClientBase, IPurchaseDeliveryNote_ApiClient
    {
        private const string BaseRoute = "api/purchasing/delivery-notes";
        protected override string BaseEndpoint => BaseRoute;
        public PurchaseDeliveryNote_ApiClient(
            HttpClient httpClient,
            ILogger logger = null,
            IConfiguration configuration = null)
            : base(httpClient, logger, configuration)
        {
        }
        #region Standard CRUD Operations
        /// <summary>
        /// Retrieves a purchase delivery note by ID
        /// </summary>
        public async Task<Result<PurchaseDeliveryNote_Response_DTO>> GetByIdAsync(long id, CancellationToken cancellationToken = default)

        {
            return await GetAsync<PurchaseDeliveryNote_Response_DTO>($"{BaseRoute}/{id}", cancellationToken);
        }
        /// Retrieves all purchase delivery notes
        public async Task<Result<IEnumerable<PurchaseDeliveryNote_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<PurchaseDeliveryNote_Response_DTO>(BaseRoute, cancellationToken);
        }
        /// Retrieves purchase delivery notes with filtering
        public async Task<Result<IEnumerable<PurchaseDeliveryNote_Response_DTO>>> GetFilteredAsync(
            string statusCode = null,
            long? supplierId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(statusCode))
                queryParams.Add($"statusCode={Uri.EscapeDataString(statusCode)}");
            if (supplierId.HasValue)
                queryParams.Add($"supplierId={supplierId.Value}");
            if (dateFrom.HasValue)
                queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
            if (dateTo.HasValue)
                queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");
            var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            return await GetListAsync<PurchaseDeliveryNote_Response_DTO>($"{BaseRoute}{queryString}", cancellationToken);
        }
        /// Creates a new purchase delivery note
        public async Task<Result<PurchaseDeliveryNote_Response_DTO>> CreateAsync(PurchaseDeliveryNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PostAsync<PurchaseDeliveryNote_Edit_DTO, PurchaseDeliveryNote_Response_DTO>(BaseRoute, dto, cancellationToken);
        }
        /// Updates an existing purchase delivery note
        public async Task<Result<PurchaseDeliveryNote_Response_DTO>> UpdateAsync(long id, PurchaseDeliveryNote_Edit_DTO dto, CancellationToken cancellationToken = default)

        {
            return await PutAsync<PurchaseDeliveryNote_Edit_DTO, PurchaseDeliveryNote_Response_DTO>($"{BaseRoute}/{id}", dto, cancellationToken);
        }
        /// Deletes a purchase delivery note
        public async Task<Result<Unit>> DeleteAsync(long id, CancellationToken cancellationToken = default)

        {
            return await DeleteAsync($"{BaseRoute}/{id}", cancellationToken);
        }
        #endregion
        #region Workflow Operations
        /// <summary>
        /// Submits multiple purchase delivery notes for approval
        /// </summary>
        public async Task<Result<int>> SubmitForApprovalAsync(List<long> ids, CancellationToken cancellationToken = default)
        {
            return await PostAsync<List<long>, int>($"{BaseRoute}/submit-for-approval", ids, cancellationToken);
        }

        /// <summary>
        /// Receives goods from a purchase delivery note
        /// </summary>
        public async Task<Result<PurchaseDeliveryNote_Response_DTO>> ReceiveAsync(
            long id,
            DateTime receivedDate,
            Dictionary<long, decimal> receivedQuantities,
            string receiptNotes = null,
            CancellationToken cancellationToken = default)
        {
            var request = new PurchaseDeliveryNote_Receive_DTO
            {
                ReceivedDate = receivedDate,
                ReceivedQuantities = receivedQuantities,
                ReceiptNotes = receiptNotes
            };
            return await PostAsync<PurchaseDeliveryNote_Receive_DTO, PurchaseDeliveryNote_Response_DTO>($"{BaseRoute}/{id}/receive", request, cancellationToken);
        }

        /// <summary>
        /// Posts a purchase delivery note to inventory
        /// </summary>
        public async Task<Result<PurchaseDeliveryNote_Response_DTO>> PostAsync(
            long id,
            DateTime postDate,
            string comments = null,
            CancellationToken cancellationToken = default)
        {
            var request = new PurchaseDeliveryNote_Post_DTO
            {
                PostDate = postDate,
                Comments = comments
            };
            return await PostAsync<PurchaseDeliveryNote_Post_DTO, PurchaseDeliveryNote_Response_DTO>($"{BaseRoute}/{id}/post", request, cancellationToken);
        }
        #endregion
    }
}
