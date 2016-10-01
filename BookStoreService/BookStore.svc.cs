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
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string BookListLocation = "books.txt";
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Book> _booklist;
        private readonly string _path;

        public BookStore()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BookListLocation);
            _booklist = new List<Book>();

            // Read the file and display it line by line.
            if (!WriteFileToList(_booklist, _path))
            {
                Logger.Error("Bookstore Service constructor failed");
            }
        }

        public List<Book> GetAllBooks()
        {
            return _booklist;
        }

        public bool AddBook(string id, string bookName, string author, string year, string price, string stock)
        {
            //none of the input vales are null
            if (id==null || bookName==null||author==null||year==null|| price==null||stock==null) return false;
            
            //Validate year is positive Int >0
            int intYear;
            bool yearResult = int.TryParse(year, out intYear);

            //Validate that Stock is >0
            int intStock;
            bool stockResult = int.TryParse(stock, out intStock);

            //Validate price is positive Int > 0
            double dblPrice;
            bool priceResult = double.TryParse(price, out dblPrice);

 
            if ((ValidateUniqueId(id) && (intYear > 0) && yearResult && priceResult && (dblPrice > 0) &&
                (intStock > 0) && stockResult)==false) return false;


            //add to the list
            Logger.Info("Adding book to the booklist");
            string[] inputparams = {id, bookName, author, year, price, stock};
            _booklist.Add(CreateBook(inputparams));

            //clear the contents of the file then rewrite the list to file
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            try
            {
                return ClearFileContents(_path) && WriteListToFile(_booklist, _path);
            }

            catch (Exception e)
            {
                Logger.Error("FileWriter Failed to add Book Record");
                Logger.Error(e.StackTrace);
                return false;
            }
        }

        public bool DeleteBook(DeletableField field, string value)
        {
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
                        default:
                            Console.WriteLine("Couldnt math the enumerator");
                            return false;
                    }
                    if (listtodelete.Count == 0)
                    {
                        return false;
                    }

                    var notInList = new List<Book>(_booklist.Except(listtodelete));
                    _booklist = notInList;
                    return ClearFileContents(_path) && WriteListToFile(_booklist, _path);
                }
                catch (Exception)
                {
                    Logger.Warn("Unable to delete Books");
                    return false;
                }
            }
        }

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
                        return _booklist.FindAll(b => b.Author.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >=0);
                    case SearchableField.BookName:
                        return _booklist.FindAll(b => b.Name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
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

        

        private static Book CreateBook(IReadOnlyList<string> result)
        {
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Book newBook = new Book
            {
                Id = result[0],
                Name = result[1],
                Author = result[2],
                Year = Convert.ToInt32(result[3]),
                Price = Convert.ToSingle(result[4].TrimStart('$')),
                Stock = Convert.ToInt32(result[5])
            };
            return newBook;
        }

       private  bool ValidateUniqueId(string id)
        {
            var books = GetAllBooks();
            foreach (var book in books)
            {
                if (book.Id != id) continue;
                Logger.Info($"Duplicate book ID found ID entered is {id}, item in db is {book.Id}");
                return false;
            }
            return true;
        }
  
    }

}

