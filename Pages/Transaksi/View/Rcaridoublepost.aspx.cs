using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class Rcaridoublepost : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
                //dtMulai.Text = DateTime.Now.ToString("yyy-MM-dd");
                //btnPosting.Visible = false;
                //LoadData();
            }
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    CloseMessage();
        //    loadData();

        //}

        protected void loadData()
        {

            ObjGlobal.Param.Clear();
            grdHarianGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadcaridoublepost", ObjGlobal.Param);
            grdHarianGL.DataBind();


        }
      
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }

        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }

        protected void grdHarianGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdHarianGL.PageIndex = e.NewPageIndex;
            //LoadData();
        }


    }
}