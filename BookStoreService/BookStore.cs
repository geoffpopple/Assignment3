using System;
using System.Collections.Generic;
using System.IO;

namespace BookStoreService
{
    public partial class BookStore
    {
        private bool ClearFileContents(string filename)
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

        private bool WriteListToFile(List<Book> booklist, string filename)
        {
            Logger.Info("opening the filestream " + filename + " for writing ");
            using (var file = new StreamWriter(filename))
            {
                foreach (Book book in booklist)
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

        private bool WriteFileToList(List<Book> booklist, string filename)
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
    }
}