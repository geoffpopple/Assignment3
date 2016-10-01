using System.ServiceModel;


namespace BookPurchaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBookPurchase
    {

        [OperationContract]
        BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info);
    }
}
