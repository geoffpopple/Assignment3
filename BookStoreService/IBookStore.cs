/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//IBookstore.cs

//Task 1 Helper functions and methods
using System.Collections.Generic;
using System.ServiceModel;

//Interface to support Service Contract
//Implemented in BookStore.svc.cs
namespace BookStoreService
{
    [ServiceContract]
    public interface IBookStore
    {
        [OperationContract]
        //comment
        List<Book> GetAllBooks();

        [OperationContract]
        bool AddBook(string id, string bookName,string author,string year, string price, string stock);

        [OperationContract]
        bool DeleteBook(DeletableField field, string value);

        [OperationContract]
        List<Book> SearchBooks(SearchableField field, string value);
    }
}
