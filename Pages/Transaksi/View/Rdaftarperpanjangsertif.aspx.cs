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
    public partial class Rdaftarperpanjangsertif : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("");

            }
        }

        protected void LoadData(string text)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdAssetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPperpanjangsertifikat", ObjGlobal.Param);
            grdAssetUpdate.DataBind();

        }
        protected void tes(object sender, EventArgs e)
        {

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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
           
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }
        

        protected void grdAssetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAssetUpdate.PageIndex = e.NewPageIndex;
            LoadData("");
        }

        protected void grdAssetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }


    }
}