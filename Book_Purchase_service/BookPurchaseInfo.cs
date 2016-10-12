/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//BookPurchaseInfo.cs

//Task 3 required contracts
//using message contracts

using System.Collections.Generic;
using System.ServiceModel;

namespace Book_Purchase_service

{
    [MessageContract]

    public class BookPurchaseInfo
    {
        [MessageBodyMember]
        public float Budget { get; set; }

        [MessageBodyMember]
        public Dictionary<int, int> Items { get; set; }
    }
}