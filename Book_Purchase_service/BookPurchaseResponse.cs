/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//BookPurchaseResponse.cs

//Task 3 required contracts
//using message contracts

using System.ServiceModel;

namespace Book_Purchase_service
{
    [MessageContract]
    public class BookPurchaseResponse
    {
        public BookPurchaseResponse(){ }
        
        public BookPurchaseResponse(bool result,string response)
        {
            Response = response;
            Result = result;
        }

        [MessageBodyMember]
        public bool Result { get; set; }
        [MessageBodyMember]
        public string Response { get; set; }
    }
}