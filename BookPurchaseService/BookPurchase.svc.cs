using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace BookPurchaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BookPurchasesvc: IBookPurchase
    {
        public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {

            //Get the List of Books from the BookStore service
            //ServiceReference1.BookStoreClient bookstore = new ServiceReference1.BookStoreClient();
            //var books = bookstore.GetAllBooks();

            //setup some helper vars
            float myBudget = info.budget;
            var totalCost = info.items.Skip(1).Sum(v => v.Key * v.Value);
            //
            if (totalCost > myBudget){
                return new BookPurchaseResponse(false, "Not Enough Money");
            }
            return null;
        }
    }
}
