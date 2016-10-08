using BookStoreApp.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreApp
{
    public partial class bookstore : System.Web.UI.Page
    {
        private List<Book> books;
        private BookStoreClient mybookstore;
        private const string ClickCount = "ClickCount";
        private const string lastBookList = "lastBookList";

        public List<Book> lastBooks
        {
            get
                {
                if (!(ViewState[lastBookList] is List<Book>))
                {
                    ViewState[lastBookList] = new List<Book>();
                }

                return (List<Book>)ViewState[lastBookList];
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            mybookstore = new BookStoreClient();
            if (!Page.IsPostBack)
            {
                //Set the number of default controls
                ViewState[ClickCount] = ViewState[ClickCount] == null ? 1 : ViewState[ClickCount];
                AddControls();

                books = mybookstore.GetAllBooks();
                ViewState[lastBookList] = books;
            }
 
            GridView1.DataSource = books;

            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                mybookstore.AddBook(txtID.Text, txtName.Text, txtAuthor.Text, txtYear.Text, txtPrice.Text, txtStock.Text);
                books = mybookstore.GetAllBooks();
                GridView1.DataSource = books;
                GridView1.DataBind();
            }
            //we did a partial postback so controls in updatelpanel2 are lost, redraw
            AddControls();

        }
        protected void ServerValidation_2(object source, ServerValidateEventArgs arguments)
        {

            if (txtSearch.Text == "" && !(DropDownList1.Text=="Clear"))
            {
                CVSearch.ErrorMessage = "Search Cannot be empty";
                arguments.IsValid = false;
            }

            switch (DropDownList1.Text)
            {
                case "Year":
                    {
                        int result = 0;
                        bool res = int.TryParse(txtSearch.Text, out result);
                        if ((res == false) || (result <= 0))
                        {
                            CVSearch.ErrorMessage = "Invalid Year";
                            arguments.IsValid = false;
                        }
                        break;
                    }
            default: { break; }

        }
    }

        protected void ServerValidation_1(object source, ServerValidateEventArgs arguments)
        {
            GridView1.DataSource = ViewState[lastBookList];
            GridView1.DataBind();
            switch (dropDelete.Text)
            {
                case "Year":
                    {
                        int result = 0;
                        bool res = int.TryParse(txtDelete.Text, out result);
                        if ((res == false) || (result <= 0))
                        {
                            CVDelete.ErrorMessage = "Invalid Year";
                            arguments.IsValid = false;
                        }
                        break;
                    }

                case "Num":
                    {
                        int result = 0;
                        bool res = int.TryParse(txtDelete.Text, out result);
                        if ((res == false) || (result < 0) || (result > GridView1.Rows.Count))

                        {
                            arguments.IsValid = false;
                            CVDelete.ErrorMessage = "Invalid Number";
                        }
                        break;

                    }
                default: { break; }

            }

        }
        protected void ServerValidation(object source, ServerValidateEventArgs arguments)
        {
            GridView1.DataSource = ViewState[lastBookList];
            GridView1.DataBind();
            foreach (GridViewRow s in GridView1.Rows)
            {
                if (s.Cells[1].Text == arguments.Value)
                {
                    arguments.IsValid = false;
                }
            }
        }

        protected void btnMore_Click(object sender, EventArgs e)

        {
            {

                ViewState[ClickCount] = ControlsRequired() + 1;
                AddControls();
                GridView1.DataSource = ViewState[lastBookList];
                GridView1.DataBind();
            }
        }

        private void AddControls()
        {

            for (int i = 0; i < ControlsRequired(); i++)
            {
                UpdatePanel2.ContentTemplateContainer.Controls.Add(new Label { Text = "Book Number", ID = "lblbookNum" + i });
                UpdatePanel2.ContentTemplateContainer.Controls.Add(new TextBox { ID = "txtbookNum" + i });
                UpdatePanel2.ContentTemplateContainer.Controls.Add(new Label { Text = "Amount", ID = "txtAmount" + i });
                UpdatePanel2.ContentTemplateContainer.Controls.Add(new TextBox { ID = "txtQuantity" + i });
                UpdatePanel2.ContentTemplateContainer.Controls.Add(new Literal() { Text = "<br/>" });

            }
        }

        private int ControlsRequired()
        {
            return int.Parse(ViewState[ClickCount].ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) { 
            switch (DropDownList1.Text)
            {
                case "Year":
                    books = mybookstore.SearchBooks(SearchableField.Year, txtSearch.Text);
                    break;
                case "ID":
                    books = mybookstore.SearchBooks(SearchableField.Id, txtSearch.Text);
                    break;
                case "Name":
                    books = mybookstore.SearchBooks(SearchableField.BookName, txtSearch.Text);
                    break;
                case "Author":
                    books = mybookstore.SearchBooks(SearchableField.AuthorName, txtSearch.Text);
                    break;
                case "Clear":
                    books = mybookstore.GetAllBooks();
                    break;
                default:
                    books = new List<Book>();
                    break;
            }
           ViewState[lastBookList] = books;
            GridView1.DataSource = books;
        }
            else
            {
                GridView1.DataSource = ViewState[lastBookList];
             }
            GridView1.DataBind();
            AddControls();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool result;
                switch (dropDelete.Text)
                {
                    case "Year":
                        result = mybookstore.DeleteBook(DeletableField.Year, txtDelete.Text);
                        break;
                    case "ID":
                        result = mybookstore.DeleteBook(DeletableField.Id, txtDelete.Text);
                        break;
                    case "Num":
                        result = mybookstore.DeleteBook(DeletableField.Id, FindIDfromNum(txtDelete.Text));
                        break;
                    default:
                        result = false;
                        break;
                }
                if (result == true)
                {
                    lblResponse.Text = "Success";
                }
                else
                {
                    lblResponse.Text = "Failure";
                }
            }
            books = mybookstore.GetAllBooks();
            GridView1.DataSource = books;
            GridView1.DataBind();
            AddControls();
        }

        protected string FindIDfromNum(string num)
        {
            GridView1.DataSource = ViewState[lastBookList];
            GridView1.DataBind();
            return GridView1.Rows[Convert.ToInt32(num)-1].Cells[1].Text;
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {

            AddControls();
            GridView1.DataSource = ViewState[lastBookList];
            GridView1.DataBind();
            // foreach (Control updChild in UpdatePanel2.ContentTemplateContainer.Controls)
            // {
            //   if (updChild.ID.Substring(0, 3) == "txt") { }
            //
            //}
            //Extract the entered data from the controls
            List<lineitems> purchased = new List<lineitems>();
            int numOnPage=UpdatePanel2.ContentTemplateContainer.Controls.Count;
            int numControls = (numOnPage - 3) / 5;
            for (int counter = 1; counter <=numControls; counter++)
            {
                TextBox ctl = FindControl("txtBookNum" + counter) as TextBox;
                TextBox ctl2 = FindControl("txtQuantity" + counter) as TextBox;
                purchased.Add(new lineitems { Booknum = ctl.Text, many = ctl2.Text });
                
            }
            lblResponse.Text = Convert.ToString(numControls);
            
        }
                    
    }

    public class lineitems {
        public string Booknum { get; set; }
        public string many { get; set; }
    }
}