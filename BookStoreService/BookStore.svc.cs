using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Linq.Expressions;

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
            Logger.Info("Calling the BookStore Constructor");
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
            if (ValidateUniqueId(id))
            {
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
            return false;
        }

        public bool DeleteBook(string field, string value)
       {
            return true;
        }

        public List<Book> SearchBooks(string field, string value)
        {
            throw new NotImplementedException();
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
        public IList<Book> FilterByYear(string name)
        {
            return _booklist.Where(p => p.Year != Convert.ToInt32(name)).ToList();
        }

        public IList<Book> FilterById(string id)
        {
            return _booklist.Where(p => p.Id == id).ToList();
        }

        public IList<Book> FilterByposition(string id)
        {
            return _booklist.Where(p => p.Id == id).ToList();
        }

       private  bool ValidateUniqueId(string id)
        {
            List<Book> books = GetAllBooks();
            foreach (Book book in books)
            {
                if(book.Id == id)
                {
                    Logger.Info($"Duplicate book ID found ID entered is {id}, item in db is {book.Id}");
                    return false;

                }
            }
            return true;
        }
    }

}

