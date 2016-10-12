/*/////////////////////////////////
///    Geoff Popple S4208241    ///
///    INFS 3204:Assignmet 3    ///
/////////////////////////////////*/

//bookstore.aspx.cs

//Task 1+2 required functions

using BookStoreApp.ServiceReference2;
using BookStoreApp.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

//Assembly to enable log4net
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace BookStoreApp
{
    public partial class Bookstore : Page
    {
        //decorate class for logging
        private static readonly log4net.ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //local vars
        private List<Book> _books;  //fairly sure i could combine this with the session state var
        private BookStoreClient _mybookstore;

        //Session state variables
        private const string ClickCount = "ClickCount";
        private const string LastBookList = "lastBookList";



        //Set up the gridview and keep track of how many times the
        //more button has been clicked
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
            //load the grid with book data
            //given more time would look to replace this with an event
            //and make use of a observer pattern
            GridView1.DataSource = _books;
            GridView1.DataBind();
        }

        //Function called when adding a book
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

        //Button for more purchase rows
        protected void btnMore_Click(object sender, EventArgs e)

        {
            {

                ViewState[ClickCount] = ControlsRequired() + 1;
                AddLineItems();
                GridView1.DataSource = ViewState[LastBookList];
                GridView1.DataBind();
            }
        }

        //function for seaching -- only works if valid data
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                switch (DropSearch.Text)
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
                        Logger.Info("{dropDelete.text} entered");
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

        //delete button - only works if valid data
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
                        Logger.Info("{dropDelete.text} entered");
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

        //purchase button - builds the objects for the webservice (after validation
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            _books = _mybookstore.GetAllBooks();
            GridView1.DataSource = _books;
            GridView1.DataBind();

            var bpInfo = new BookPurchaseInfo();
            var lineItems = new Dictionary<int, int>();

            AddLineItems();
            var numlines = int.Parse(ViewState[ClickCount].ToString());

            //looks at the contents of the purchase items data
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

            lblResponse.Text = bpResponse.Response;
        }

        #region ValidationFunction

        //Validate we have no duff data in budget
        private static float ValidateBudget(string budget)
        {
            float outVal;
            var tryConvert = float.TryParse(budget, out outVal);
            return tryConvert ? outVal : 0f;

        }

        //Validate we have no duff data in qty
        private static int ValidateQty(string value)
        {
            int outVal;
            //catch cruddy input
            var tryConvert = int.TryParse(value, out outVal);
            //catch where  Qty input <0
            outVal = (outVal < 0) ? 0 : outVal;
            return tryConvert ? outVal : 0;

        }
        //Validate we have no duff data Book ID
        private int ValidateBookId(string value)
        {
            int outVal;
            //catch cruddy input
            int.TryParse(value, out outVal);
            //catch where  Qty input <0 or greather than rows in the grid
            return (outVal < 0 || outVal > _mybookstore.GetAllBooks().Count) ? 0 : outVal;
        }

        //one of the (many) validation functions. This one for the dropdown search list 
        protected void ServerValidation_2(object source, ServerValidateEventArgs arguments)
        {

            if (txtSearch.Text == "" && DropSearch.Text != "Clear")
            {
                CVSearch.ErrorMessage = "Search Cannot be empty";
                arguments.IsValid = false;
            }

            switch (DropSearch.Text)
            {
                case "Year":
                    {
                        int result;
                        var res = int.TryParse(txtSearch.Text, out result);
                        if ((res == false) || (result <= 0))
                        {
                            CVSearch.ErrorMessage = "Invalid Year";
                            arguments.IsValid = false;
                        }
                        break;
                    }
                default:
                    {
                        Logger.Info("{DropSearch.Text} entered");
                        break;
                    }
            }
        }
        //one of the (many) validation functions for the dropdown list this one for delete dropdown
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
                default:
                    {
                        Logger.Info("{dropDelete.Text} entered");
                        break;
                    }

            }

        }
        //one of the (many) validation functions for the dropdown list this one for checking unique constraint in ID
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
        #endregion

        #region HelperFunctions

        //Session state var to hold the list of books between postbacks.
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

        //determines how many rows of purchase controls are required on postback
        private int ControlsRequired()
        {
            return int.Parse(ViewState[ClickCount].ToString());
        }

        //Add the required number of controls to the placeholder
        private void AddLineItems()
        {
            for (int i = 0; i < int.Parse(ViewState[ClickCount].ToString()); i++)
            {
                PH1.Controls.Add(LoadControl("~/Items.ascx"));
            }
        }

        #endregion
    }
}