using System;
using System.Collections.Generic;
using System.Globalization;
using Book_Purchase_service.ServiceReference1;
using System.Web.Services;

namespace Book_Purchase_service
{
   public class BookPuchaseSrv : IBookPurchasesvc    
    {
         public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {
            //Get the List of Books from the BookStore service
            BookStoreClient bookstore = new BookStoreClient();
            var books = bookstore.GetAllBooks();
            //setup some helper vars
            float myBudget = info.budget;
            float remainingCash = myBudget;
            //Create a dictionary from our list to make our life easier using the id as the key
            Dictionary<string, Book> bookDict = new Dictionary<string, Book>();
            foreach (Book book in books)
            {
                bookDict.Add(book.Id, book);
            }

            //look at our purchses one at a time and check we cam fulfil
            foreach (var purchased in info.items)
            {
                //check stock does not exceed the stocklevel
                if (purchased.Value > bookDict[purchased.Key].Stock)
                {
                    return new BookPurchaseResponse(false, "Not enough stock");
                }
                else
                {
                    //Deduct the cost from our running total and check we have not blown the budget
                    remainingCash -= purchased.Value * bookDict[purchased.Key].Price;
                    if (remainingCash < 0) return new BookPurchaseResponse(false, "Not enough Money");
                }
            }
            return new BookPurchaseResponse(true, Convert.ToString(remainingCash, CultureInfo.InvariantCulture));
        }

    }
}
