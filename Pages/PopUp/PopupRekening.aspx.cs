using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.PopUp
{
    public partial class PopupRekening : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataRekeningAJ();
                hdnTipe.Value = Request.QueryString["tipe"];
            }

        }
        protected void LoadDataRekeningAJ()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdRekAJ.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank", ObjGlobal.Param);
            grdRekAJ.DataBind();

        }

        protected void grdRekAJ_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRekAJ.PageIndex = e.NewPageIndex;
            LoadDataRekeningAJ();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDataRekeningAJ();
        }

       
    }
}