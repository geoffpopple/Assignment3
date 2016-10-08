using System.Collections.Generic;
using System.ServiceModel;

namespace Book_Purchase_service

{
    [MessageContract]
    public class BookPurchaseInfo
    {
        [MessageBodyMember]
        public float budget { get; set; }
        [MessageBodyMember]
        public Dictionary<string, int> items { get; set; }
    }
}