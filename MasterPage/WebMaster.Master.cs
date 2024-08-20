using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.IO;

namespace eFinance.MasterPage
{
    public partial class WebMaster : System.Web.UI.MasterPage
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ISLOGIN"] == null || Session["ISLOGIN"] != "Yes")
            {
                if (Page.IsCallback)
                    Response.Redirect(Func.BaseUrl + "Default.aspx");
                else
                    Response.Redirect(Func.BaseUrl + "Default.aspx");
            }
            if (!IsPostBack)
            {
                DataSet mySet1 = ObjDb.GetRows("select nmPerusahaan from parameter ");
                DataRow myRow1 = mySet1.Tables[0].Rows[0];
                yayasan.Text = myRow1["nmPerusahaan"].ToString();
                String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                Uri uri = new Uri(originalPath);
                string filename = Path.GetFileName(uri.LocalPath);
                DataSet mySet = ObjDb.GetRows("SELECT top 1 siteMapMenu, judulMenu FROM Menu WHERE namaFileMenu like '%" + filename + "%'");
                if (mySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    lblBreadCrumbs.Text = myRow["siteMapMenu"].ToString().Replace(">", "►");
                    //lblTitleMenu.Text = myRow["judulMenu"].ToString();
                }

            }
            //else
            //{
            //    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //    //s.Connect(EPhost);
            //    if (s.Connected)
            //    {
            //        ShowMessage("error", "Unable to connect to host.");
            //    }
            //}
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            lblMessage.Text = ObjSys.GetMessage(_class, _message);
            lblMessage.Visible = true;
        }
        protected void CloseMessage()
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }
    }
}