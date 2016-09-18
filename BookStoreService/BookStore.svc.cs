using System;
using System.Collections.Generic;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace BookStoreService
{

    public class BookStore : IBookStore
    {

        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string BookListLocation = "books.txt";
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Book> _booklist;

        public BookStore()
        {
            Logger.Info("Calling theBookStore Constructor");
            _booklist = new List<Book>();
            // Read the file and display it line by line.
            Logger.Info("opening the filestream");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , BookListLocation);
            Logger.Info(path);
            var file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] result = line.Split(',');
                _booklist.Add(CreateBook(result));
            }

            file.Close();     
        }

        public List<Book> GetAllBooks()
        {  
            return _booklist;
        }

        public bool AddBook(string id, string bookName, string author, string year, string price, string stock)
        {
            //add to the list
            string[] inputparams = {id,bookName,author,year,price,stock};
            _booklist.Add(CreateBook(inputparams));
            //create our string to add to the file
            string newBook = "\r\n" + id + "," + bookName + "," + author + "," + year + ",$" + price + "," + stock;
            try
            {
                //write it to file
                using (var fs = new FileStream(BookListLocation, FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(newBook);
                sw.Close();
                fs.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Error("FileWriter Failed to add Book Record");
                Logger.Error(e.StackTrace);
                return false;
            }
            return true;
        }
        
        public bool DeleteBook(string field, string value)
        {
            throw new NotImplementedException();
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
    }
}
