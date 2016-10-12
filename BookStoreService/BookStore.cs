/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//Bookstore.cs

//Task 1 Helper functions and methods
//Really these functions have been lumped together
//and should be split up if extending.

using System;
using System.Collections.Generic;
using System.IO;

namespace BookStoreService
{
    public partial class BookStore
    {
        //Clear the contents of the file - easier than appending!
        private static bool ClearFileContents(string filename)
        {
            try
            {
                File.WriteAllText(filename, string.Empty);
            }
            catch (Exception e)
            {
                Logger.Error("'Clearing the file failed");
                Logger.Error(e.StackTrace);
            }
            return true;
        }

        //FileWriter list -> file
        //no need to create adaptor pattern/Interface as single source/destination
        private static bool WriteListToFile(IEnumerable<Book> booklist, string filename)
        {
            Logger.Info("opening the filestream " + filename + " for writing ");
            using (var file = new StreamWriter(filename))
            {
                foreach (var book in booklist)
                {
                    {
                        string line = $"{book.Id},{book.Name},{book.Author},{book.Year},{book.Price:C2},{book.Stock}";
                        file.WriteLine(line);
                    }
                }
                file.Close();
            }
            return true;
        }

        //FileReader File -> List
        //no need to create adaptor pattern/Interface as single source/destination
        private static bool WriteFileToList(ICollection<Book> booklist, string filename)
        {
            Logger.Info("opening the filestream " + filename + " for reading ");
            StreamReader file = StreamReader.Null;
            try
            {
                file = new StreamReader(filename);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] result = line.Split(',');
                    booklist.Add(CreateBook(result));
                }
            }
            catch (Exception e)
            {
                Logger.Error("Errror Wrting to file" + filename);
                Logger.Error(e.StackTrace);
                return false;
            }
            finally
            {
                file.Close();
            }
            return true;
        }

        //Task 1.2.2 Validation
        // :not null inputs, :UniqueID (as seperate function), :Book Year Positive int, :BookPrice positive float
        // :Book stock positive integer 
        private bool ValidateAddBook(string id, string bookName, string author, string year, string price, string stock)
        {
            //Validate none of the inputs are null
            if (id == null || bookName == null || author == null || year == null || price == null || stock == null)
                return false;

            //Validate year is positive Int >0
            int intYear;
            bool yearResult = int.TryParse(year, out intYear);

            //Validate that Stock is >0
            int intStock;
            bool stockResult = int.TryParse(stock, out intStock);

            //Validate price is positive float > 0
            double dblPrice;
            bool priceResult = double.TryParse(price, out dblPrice);


            if ((ValidateUniqueId(id) && (intYear > 0) && yearResult && priceResult && (dblPrice > 0) &&
                 (intStock > 0) && stockResult) == false) return false;
            return true;
        }

        //Helper function to create a book 
        //Should really move this to the book Class!
        private static Book CreateBook(IReadOnlyList<string> result)
        {
            var newBook = new Book
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
        //Task 1.2.2 Validation - UniqueID
        //Helper function to validate uniqueness 
        //Should really move this to the book Class!
        private bool ValidateUniqueId(string id)
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