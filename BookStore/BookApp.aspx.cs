using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace BookStore
{
    
    public partial class BookApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var bookList = new svc_BookStore.BookStoreClient().GetAllBooks();
            grid.DataSource = bookList;
            grid.DataBind();
        }
    }
}