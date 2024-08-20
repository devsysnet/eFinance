using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eFinance.GlobalPage
{
    public partial class Header : System.Web.UI.UserControl
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgUser.ImageUrl = "../assets/images/user/" + ObjSys.GetUserImage;
                lblNameUser.Text = ObjSys.GetFullName;
            }
        }
        
    }
}