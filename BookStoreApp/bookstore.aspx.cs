using BookStoreApp.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BookStoreApp
{
    public partial class bookstore : System.Web.UI.Page
    {
       private List<Book> books;
        private const string ClickCount = "ClickCount";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Set the number of default controls
                ViewState[ClickCount] = ViewState[ClickCount] == null ? 1 : ViewState[ClickCount];
                AddControls();
            }

           BookStoreClient bookstore = new BookStoreClient();
            books = bookstore.GetAllBooks();
           GridView1.DataSource = books;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblResponse.Text = "Valid.";
                BookStoreClient bookstore = new BookStoreClient();
                bookstore.AddBook(txtID.Text, txtName.Text, txtAuthor.Text, txtYear.Text, txtPrice.Text, txtStock.Text);
                var books = bookstore.GetAllBooks();
                GridView1.DataSource = books;
                GridView1.DataBind();
            }
            else
            {
                lblResponse.Text = "inValid.";
            }
            //we did a paartial postback so controls inupdatecontrolpanel2 are lost, redraw
            AddControls();

        }
        protected void ServerValidation(object source, ServerValidateEventArgs arguments)
        {
            foreach (GridViewRow s in GridView1.Rows)
            {
                if (s.Cells[1].Text == arguments.Value)
                {
                    arguments.IsValid = false;
                    break;
                }
            }
        }

        protected void btnMore_Click(object sender, EventArgs e)

        {
            {

                ViewState[ClickCount] = ControlsRequired() + 1;
                AddControls();
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
            
        }
    }
}