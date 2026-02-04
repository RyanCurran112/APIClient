using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;

namespace MES.Office.ApiClient.Transactions.Purchasing
{
    /// <summary>
    /// Interface aliases for API clients without underscore naming convention.
    /// These aliases allow Razor components to use simpler interface names.
    /// </summary>
    public interface IPurchaseDeliveryNoteApiClient : IPurchaseDeliveryNote_ApiClient { }
    public interface IPurchaseReturnApiClient : IPurchaseReturn_ApiClient { }
    public interface IShippingNoteApiClient : IShippingNote_ApiClient { }
}
