using System.ServiceModel;

namespace Book_Purchase_service
{
    [ServiceContract]
    public interface IBookPurchasesvc
    {
        [OperationContract]
        BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info);
    }
}
