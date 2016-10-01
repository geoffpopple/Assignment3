using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Book_Purchase_service

{
    [MessageContract]
    public class BookPurchaseInfo
    {
        [MessageBodyMember]
        public float budget { get; set; }
        [MessageBodyMember]
        public Dictionary<int, int> items { get; set; }
    }
}