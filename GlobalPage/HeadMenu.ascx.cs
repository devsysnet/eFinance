using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Web.Configuration;

namespace eFinance.GlobalPage
{
    public partial class HeadMenu : System.Web.UI.UserControl
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ipaddress = "";
                ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                    ipaddress = Request.ServerVariables["REMOTE_ADDR"];
                //string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                imgUser.ImageUrl = "../assets/images/user/" + ObjSys.GetUserImage;
                lblNama.Text = ObjSys.GetFullName;
                lblJabatan.Text = ObjSys.GetJabatan;
                //lblIp.Text = "Your IP : " + ipaddress;
                //SessionTimeOut();
            }
        }
    }
}