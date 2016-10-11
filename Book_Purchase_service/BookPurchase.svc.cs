using System;
using System.Globalization;
using Book_Purchase_service.ServiceReference1;

namespace Book_Purchase_service
{
   public class BookPuchaseSrv : IBookPurchasesvc    
    {
         public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {
            //Get the List of Books from the BookStore service
            var bookstore = new BookStoreClient();
            var books = bookstore.GetAllBooks();
            //setup some helper vars
            float myBudget = info.Budget;
            float remainingCash = myBudget;


            //look at our purchses one at a time and check we cam fulfil
            foreach (var purchased in info.Items)
            {
                //check stock does not exceed the stocklevel
                if (purchased.Value > books[purchased.Key-1].Stock)
                {
                    return new BookPurchaseResponse(false, "Not enough stock");
                }
                else
                {
                    //Deduct the cost from our running total and check we have not blown the budget
                    remainingCash -= purchased.Value * books[purchased.Key-1].Price;
                    if (remainingCash < 0) return new BookPurchaseResponse(false, "Not enough Money");
                }
            }
            return new BookPurchaseResponse(true, Convert.ToString(remainingCash, CultureInfo.InvariantCulture));
        }

    }
}
