using System;
using System.Collections.Generic;
using System.IO;

namespace BookStoreService
{
    public class BookStore : IBookStore
    {
        private const string BookListLocation = @"\\Mac\\Home\\Downloads\\books.txt";
            List<Book> _booklist=null;

        public BookStore()
        {
            _booklist = new List<Book>();
            // Read the file and display it line by line.
            var file = new StreamReader(BookListLocation);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] result = line.Split(',');
                _booklist.Add(createBook(result));
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
            _booklist.Add(createBook(inputparams));
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
            catch (Exception)
            {
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

        private Book createBook(string[] result)
        {
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
