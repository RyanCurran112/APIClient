using MES.Office.ApiClient.Core;
using MES.Office.WebAPI.Contracts.Common;
using MES.Office.WebAPI.Contracts.DTOs.Transactions.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Transactions.Stock
{
    /// <summary>
    /// API Client for Stock Bin Transfer Request operations
    /// </summary>
    public class StockBinTransferRequest_ApiClient : CrudApiClientBase<StockBinTransferRequest_Summary_DTO, StockBinTransferRequest_Response_DTO, StockBinTransferRequest_Edit_DTO>, IStockBinTransferRequest_ApiClient
    {
        protected override string BaseEndpoint => "api/StockBinTransferRequest";
        public StockBinTransferRequest_ApiClient(HttpClient httpClient, ILogger logger = null, IConfiguration configuration = null) : base(httpClient, logger, configuration)
        {
        }
        /// <summary>
        /// Gets all Stock Bin Transfer Requests (returns Response DTOs to satisfy interface contract)
        /// </summary>
        public new async Task<Result<IEnumerable<StockBinTransferRequest_Response_DTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockBinTransferRequest_Response_DTO>(BaseEndpoint, cancellationToken);
        }
        /// <summary>
        /// Gets all summary records for Stock Bin Transfer Requests (explicit summary method)
        /// </summary>
        public async Task<Result<IEnumerable<StockBinTransferRequest_Summary_DTO>>> GetAllSummaryAsync(CancellationToken cancellationToken = default)
        {
            return await GetListAsync<StockBinTransferRequest_Summary_DTO>($"{BaseEndpoint}/summary", cancellationToken);
        }
        /// Searches for Stock Bin Transfer Requests with filter criteria, returning full details
        public async Task<Result<IEnumerable<StockBinTransferRequest_Response_DTO>>> SearchAsync(StockBinTransferRequest_Filter_DTO filter, CancellationToken cancellationToken = default)

        {
            return await PostListAsync<StockBinTransferRequest_Filter_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/search", filter, cancellationToken);
        }
        /// <summary>
        /// Submits a Stock Bin Transfer Request for approval
        /// </summary>
        public async Task<Result<StockBinTransferRequest_Response_DTO>> SubmitForApprovalAsync(long id, StockBinTransferRequest_Submit_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransferRequest_Submit_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/{id}/submit", request, cancellationToken);
        }

        /// <summary>
        /// Approves a Stock Bin Transfer Request
        /// </summary>
        public async Task<Result<StockBinTransferRequest_Response_DTO>> ApproveAsync(long id, StockBinTransferRequest_Approve_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransferRequest_Approve_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/{id}/approve", request, cancellationToken);
        }

        /// <summary>
        /// Rejects a Stock Bin Transfer Request
        /// </summary>
        public async Task<Result<StockBinTransferRequest_Response_DTO>> RejectAsync(long id, StockBinTransferRequest_Reject_DTO request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransferRequest_Reject_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/{id}/reject", request, cancellationToken);
        }

        /// <summary>
        /// Adds a line item to a Stock Bin Transfer Request
        /// </summary>
        public async Task<Result<StockBinTransferRequest_Response_DTO>> AddLineAsync(long requestId, StockBinTransferRequest_Line_Edit_DTO lineDto, CancellationToken cancellationToken = default)
        {
            return await PostAsync<StockBinTransferRequest_Line_Edit_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/{requestId}/lines", lineDto, cancellationToken);
        }

        /// <summary>
        /// Updates a line item in a Stock Bin Transfer Request
        /// </summary>
        public async Task<Result<StockBinTransferRequest_Response_DTO>> UpdateLineAsync(long requestId, long lineId, StockBinTransferRequest_Line_Edit_DTO lineDto, CancellationToken cancellationToken = default)
        {
            return await PutAsync<StockBinTransferRequest_Line_Edit_DTO, StockBinTransferRequest_Response_DTO>(
                $"{BaseEndpoint}/{requestId}/lines/{lineId}", lineDto, cancellationToken);
        }

        /// <summary>
        /// Deletes a line item from a Stock Bin Transfer Request
        /// </summary>
        public async Task<Result<Unit>> DeleteLineAsync(long requestId, long lineId, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"{BaseEndpoint}/{requestId}/lines/{lineId}", cancellationToken);
        }
    }
}
