using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace eFinance
{
    public partial class Logout : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["confirm"] == "ok")
                {
                    ObjDb.ExecQuery("UPDATE mUser SET lastLoginDate = GETDATE() WHERE noUser = '" + ObjSys.GetUserId + "'");
                    Session["ISLOGIN"] = "No";
                    Session.Remove("USERID");
                    Session.Remove("USERNAME");
                    Session.Remove("GROUPID");
                    Session.Remove("GROUPNAME");

                    Session.Clear();
                    if (Request.Cookies["ID"] != null)
                    {
                        Response.Cookies["ID"].Expires = DateTime.Now.AddDays(-1);
                    }
                    Response.Redirect(Func.BaseUrl + "Default.aspx");
                }
                else
                {
                    Response.Redirect(Func.BaseUrl + "Default.aspx");
                }
            }
        }
    }
}