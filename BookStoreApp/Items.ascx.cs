using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStoreService
{
    public partial class Items : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtBookNum.Text = Request.Form[txtBookNum.UniqueID];
            txtAmount.Text = Request.Form[txtAmount.UniqueID];
        }
        public string BookNum
        {
            get { return txtBookNum.Text; }
            set { txtBookNum.Text = value; }
        }
        public string Quantity
        {
            get { return txtAmount.Text; }
            set { txtAmount.Text = value; }
        }
    }
}