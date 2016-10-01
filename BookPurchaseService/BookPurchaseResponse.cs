using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace BookPurchaseService
{
    [MessageContract]
    public class BookPurchaseResponse
    {
        public BookPurchaseResponse(){ }
        
        public BookPurchaseResponse(bool result,string response)
        {
            this.response = response;
            this.result = result;
        }

        [MessageBodyMember]
        public bool result { get; set; }
        [MessageBodyMember]
        public string response { get; set; }
    }
}