using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.PopUp
{
    public partial class PopupUser : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                hdnTipe.Value = Request.QueryString["tipe"];
            }
        }

        private void LoadData()
        {
            grdSup.DataSource = ObjDb.GetRows("select * from mUser where namauser LIKE '%" + txtSearch.Text + "%'");
            grdSup.DataBind();
        }

        protected void grdSup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSup.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}