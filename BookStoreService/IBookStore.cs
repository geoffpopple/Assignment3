using System.Collections.Generic;
using System.ServiceModel;

namespace BookStoreService
{
    [ServiceContract]
    public interface IBookStore
    {

        [OperationContract]
        List<Book> GetAllBooks();

        [OperationContract]
        bool AddBook(string id, string bookName,string author,string year, string price, string stock);

        [OperationContract]
        bool DeleteBook(string field, string value);

        [OperationContract]
        List<Book> SearchBooks(string field, string value);
    }
}
