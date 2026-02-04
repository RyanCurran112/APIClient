using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;

namespace MES.Office.ApiClient.Transactions.Stock
{
    /// <summary>
    /// Interface aliases for API clients without underscore naming convention.
    /// These aliases allow Razor components to use simpler interface names.
    /// </summary>
    public interface IStockIssueApiClient : IStockIssue_ApiClient { }
    public interface IStockAdjustmentApiClient : IStockAdjustment_ApiClient { }
    public interface IStockBinTransferApiClient : IStockBinTransfer_ApiClient { }
    public interface IStockBinTransferRequestApiClient : IStockBinTransferRequest_ApiClient { }
    public interface IStockMovementApiClient : IStockMovement_ApiClient { }
    public interface IStockQuarantineApiClient : IStockQuarantine_ApiClient { }
    public interface IStockReservationApiClient : IStockReservation_ApiClient { }
    public interface IStockTakeApiClient : IStockTake_ApiClient { }
    public interface IInterWarehouseTransferApiClient : IInterWarehouseTransfer_ApiClient { }
}
