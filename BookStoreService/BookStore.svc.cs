/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//Bookstore.svc.cs

//Task 1 required functions

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Assembly to enable log4net
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace BookStoreService
{
    public partial class BookStore : IBookStore
    {
        //decorate class for logging
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string BookListFilename = "books.txt";
        //collection in memory to store my list
        private List<Book> _booklist;
        private readonly string _path;

        //Constructor
        public BookStore()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BookListFilename);
            _booklist = new List<Book>();

            // Read the file and display it line by line.
            if (!WriteFileToList(_booklist, _path))
            {
                Logger.Error("Bookstore Service constructor failed");
            }
        }
        //Task 1.2.1
        public List<Book> GetAllBooks()
        {
            return _booklist;
        }

        // Task 1.2.2
        public bool AddBook(string id, string bookName, string author, string year, string price, string stock)
        {
            // validate the sanity of the input
            if (!ValidateAddBook(id, bookName, author, year, price, stock)) return false;

            //add to the list
            Logger.Info("Adding book to the booklist");
            string[] inputparams = {id, bookName, author, year, price, stock};

            //clear the contents of the file then rewrite the list to file
            //almost a transaction
            try
            {
                _booklist.Add(CreateBook(inputparams));
                return ClearFileContents(_path) && WriteListToFile(_booklist, _path);
            }

            catch (Exception e)
            {
                Logger.Error("FileWriter Failed to add Book Record");
                Logger.Error(e.StackTrace);
                return false;
            }
        }

        //Task 1.2.3
        //Really not too happy with this function as is one given more time that
        //I would refactor and possibly use a delegate/generics.
        // The search function is utilised to return the list that meets the 
        //criteria
        public bool DeleteBook(DeletableField field, string value)
        {
            try
            {
                List<Book> listtodelete;
                switch (field)
                {
                    case DeletableField.Year:
                        listtodelete = SearchBooks(SearchableField.Year, value);
                        break;
                    case DeletableField.Id:
                        listtodelete = SearchBooks(SearchableField.Id, value);
                        break;
                    case DeletableField.BookName:
                        listtodelete = SearchBooks(SearchableField.BookName, value);
                        break;
                    case DeletableField.Num:
                        listtodelete = _booklist.GetRange(Convert.ToInt32(value) - 1, 1);
                        break;
                    default:
                        Console.WriteLine("Couldnt match the enumerator");
                        return false;
                }
                //see if we have anything to do
                if (listtodelete.Count == 0)
                {
                    return false;
                }
                //Here we do a set operation and extract the books not in the delete
                //list to another list.  This extracted list is then set to be the booklist.
                var notInList = new List<Book>(_booklist.Except(listtodelete));
                _booklist = notInList;
                //write the list to file
                return ClearFileContents(_path) && WriteListToFile(_booklist, _path);
            }
            catch (Exception)
            {
                Logger.Warn("Unable to delete Books");
                return false;
            }
        }

        //Task 1.2.4
        //Really not too happy with this function as is one given more time that
        //I would refactor and possibly use a delegate/generics.
        // The search function returns list that meets the criteria
        public List<Book> SearchBooks(SearchableField field, string value)
        {
            try
            {
                switch (field)
                {
                    case SearchableField.Year:
                        return _booklist.FindAll(b => b.Year.Equals(Convert.ToInt32(value))).ToList();
                    case SearchableField.Id:

                        return _booklist.FindAll(b => b.Id.Contains(value)).ToList();
                    case SearchableField.AuthorName:
                        return
                            _booklist.FindAll(
                                b => b.Author.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0);
                    case SearchableField.BookName:
                        return
                            _booklist.FindAll(b => b.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
                                .ToList();
                    default:
                        Console.WriteLine("Default case");
                        return new List<Book>();
                }
            }
            catch (Exception)
            {
                //we agree to return a list, so if things go bad, return an empty list
                Logger.Warn("empty Book List retuned");
                return new List<Book>();
            }
        }
    }
}