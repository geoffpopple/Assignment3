/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//BookPurchase.svc.cs

//Task 3 required functions

using System;
using System.Globalization;
using Book_Purchase_service.ServiceReference1;

//Assembly to enable log4net
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace Book_Purchase_service
{
   public class BookPuchaseSrv : IBookPurchasesvc    
    {
        //decorate class for logging
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string NotEnoughMoney = "Not Enough Money";
        private const string NotEnoughStock = "Not Enough Stock";

        //Task 2.2.1 
        public BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info)
        {
            //Get the List of Books from the BookStore service
            var bookstore = new BookStoreClient();
            var books = bookstore.GetAllBooks();
            
            //helper vars
            float myBudget = info.Budget;
            float remainingCash = myBudget;

            try
            {
                //look at our purchses one at a time and check we cam fulfil
                foreach (var purchased in info.Items)
                {
                    //check stock does not exceed the stocklevel
                    if (purchased.Value > books[purchased.Key - 1].Stock)
                    {
                        return new BookPurchaseResponse(false, NotEnoughStock);
                    }
                    else
                    {
                        //Deduct the cost from our running total and check we have not blown the budget
                        remainingCash -= purchased.Value * books[purchased.Key - 1].Price;
                        if (remainingCash < 0) return new BookPurchaseResponse(false, NotEnoughMoney);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Info("An exception occured when purchasing books");
                Logger.Info(e.StackTrace);
            }
            return new BookPurchaseResponse(true, Convert.ToString(remainingCash, CultureInfo.InvariantCulture));
        }

    }
}
