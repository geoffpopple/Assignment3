using System;
using System.Linq;

namespace Book_Purchase_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BookPuchaseSrv : IBookPurchasesvc
    {
        public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {
            //Get the List of Books from the BookStore service
            ServiceReference1.BookStoreClient bookstore = new ServiceReference1.BookStoreClient();
            var books = bookstore.GetAllBooks();

            //setup some helper vars
            float myBudget = info.budget;
            var totalCost = info.items.Skip(1).Sum(v => v.Key * v.Value);
            //
            if (totalCost > myBudget)
            {
                return new BookPurchaseResponse(false, "Not Enough Money");
            }
                throw new NotImplementedException();
        }
    }
}
