using BookStoreApp.ServiceReference2;
using BookStoreApp.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreApp
{
    public partial class Bookstore : Page
    {
        private List<Book> _books;
        private BookStoreClient _mybookstore;
        private const string ClickCount = "ClickCount";
        private const string LastBookList = "lastBookList";

        public List<Book> LastBooks
        {
            get
                {
                if (!(ViewState[LastBookList] is List<Book>))
                {
                    ViewState[LastBookList] = new List<Book>();
                }

                return (List<Book>)ViewState[LastBookList];
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            _mybookstore = new BookStoreClient();
            if (!Page.IsPostBack)
            {
                //Set the number of default controls
                ViewState[ClickCount] = ViewState[ClickCount] ?? 1;
                AddLineItems();

                _books = _mybookstore.GetAllBooks();
                ViewState[LastBookList] = _books;
            }
 
            GridView1.DataSource = _books;

            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                _mybookstore.AddBook(txtID.Text, txtName.Text, txtAuthor.Text, txtYear.Text, txtPrice.Text, txtStock.Text);
                _books = _mybookstore.GetAllBooks();
                GridView1.DataSource = _books;
                GridView1.DataBind();
            }
            //we did a partial postback so controls in updatelpanel2 are lost, redraw
            AddLineItems();
        }
        protected void ServerValidation_2(object source, ServerValidateEventArgs arguments)
        {

            if (txtSearch.Text == "" && DropDownList1.Text != "Clear")
            {
                CVSearch.ErrorMessage = "Search Cannot be empty";
                arguments.IsValid = false;
            }

            switch (DropDownList1.Text)
            {
                case "Year":
                    {
                        int result;
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
            GridView1.DataSource = ViewState[LastBookList];
            GridView1.DataBind();
            switch (dropDelete.Text)
            {
                case "Year":
                    {
                        int result;
                        var res = int.TryParse(txtDelete.Text, out result);
                        if ((res == false) || (result <= 0))
                        {
                            CVDelete.ErrorMessage = "Invalid Year";
                            arguments.IsValid = false;
                        }
                        break;
                    }

                case "Num":
                    {
                        int result;
                        var res = int.TryParse(txtDelete.Text, out result);
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
            GridView1.DataSource = ViewState[LastBookList];
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
                AddLineItems();
                GridView1.DataSource = ViewState[LastBookList];
                GridView1.DataBind();
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
                    _books = _mybookstore.SearchBooks(SearchableField.Year, txtSearch.Text);
                    break;
                case "ID":
                    _books = _mybookstore.SearchBooks(SearchableField.Id, txtSearch.Text);
                    break;
                case "Name":
                    _books = _mybookstore.SearchBooks(SearchableField.BookName, txtSearch.Text);
                    break;
                case "Author":
                    _books = _mybookstore.SearchBooks(SearchableField.AuthorName, txtSearch.Text);
                    break;
                case "Clear":
                    _books = _mybookstore.GetAllBooks();
                    break;
                default:
                    _books = new List<Book>();
                    break;
            }
           ViewState[LastBookList] = _books;
            GridView1.DataSource = _books;
        }
            else
            {
                GridView1.DataSource = ViewState[LastBookList];
             }
            GridView1.DataBind();
            AddLineItems();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool result;
                switch (dropDelete.Text)
                {
                    case "Year":
                        result = _mybookstore.DeleteBook(DeletableField.Year, txtDelete.Text);
                        break;
                    case "ID":
                        result = _mybookstore.DeleteBook(DeletableField.Id, txtDelete.Text);
                        break;
                    case "Num":
                        result = _mybookstore.DeleteBook(DeletableField.Num, txtDelete.Text);
                        break;
                    default:
                        result = false;
                        break;
                }
                lblResponse.Text = result ? "Success" : "Failure";
            }
            _books = _mybookstore.GetAllBooks();
            GridView1.DataSource = _books;
            GridView1.DataBind();
            AddLineItems();
        }

        protected string FindIDfromNum(string num)
        {
            GridView1.DataSource = ViewState[LastBookList];
            GridView1.DataBind();
            return GridView1.Rows[Convert.ToInt32(num)-1].Cells[1].Text;
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            _books = _mybookstore.GetAllBooks();
            GridView1.DataSource = _books;
            GridView1.DataBind();

            var bpInfo = new BookPurchaseInfo();
            var lineItems = new Dictionary<int, int>();

            AddLineItems();
            var numlines = int.Parse(ViewState[ClickCount].ToString());

            //parent Control
            Control placeholder = UpdatePanel2.ContentTemplateContainer.FindControl("PH1");
            for (var i = 0; i < numlines; i++)
            {
                var nb = placeholder.Controls[i].FindControl("txtBookNum") as TextBox;
                var ab = placeholder.Controls[i].FindControl("txtAmount") as TextBox;
                if (ab == null || nb == null || ValidateBookId(nb.Text) == 0 || ValidateQty(ab.Text) == 0) break;
                var booknum = Convert.ToInt32(nb.Text);
                var qty = Convert.ToInt32(ab.Text);

                lineItems.Add(booknum, qty);
            }
            var bps = new BookPurchasesvcClient();
            bpInfo.Budget = ValidateBudget(txtBudget.Text);
            bpInfo.Items = lineItems;
            var bpResponse = bps.PurchaseBooks(bpInfo);

            lblResponse.Text =bpResponse.response;
        }


        private void AddLineItems()
        {
            for (int i = 0; i < int.Parse(ViewState[ClickCount].ToString()); i++)
            {
                PH1.Controls.Add(LoadControl("~/Items.ascx"));
            }
        }

        private static float ValidateBudget(string budget)
        {
            float outVal;
            var tryConvert = float.TryParse(budget, out outVal);
            return tryConvert ? outVal : 0f;

        }

        private static int ValidateQty(string value)
        {
            int outVal;
            //catch cruddy input
            var tryConvert = int.TryParse(value, out outVal);
            //catch where  Qty input <0
            outVal = (outVal < 0) ? 0 : outVal;
            return tryConvert ? outVal : 0;

        }
        private  int ValidateBookId(string value)
        {
            int outVal;
            //catch cruddy input
            int.TryParse(value, out outVal);
            //catch where  Qty input <0 or greather than rows in the grid
            return (outVal < 0 || outVal > _mybookstore.GetAllBooks().Count) ? 0 : outVal;
        }
    }
}