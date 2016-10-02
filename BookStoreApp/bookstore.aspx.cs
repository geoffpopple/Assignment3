using BookStoreApp.ServiceReference2;
using System;


namespace BookStoreApp
{
    public partial class bookstore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             var books= new BookStoreClient().GetAllBooks();
            GridView1.DataSource = books;
            GridView1.DataBind();


        }
    }
}