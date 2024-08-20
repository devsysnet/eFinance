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

namespace eFinance.Pages.Transaksi.View
{
    public partial class MProjectView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            //execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadCombo();
            }
        }

           
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMasterProjectView", ObjGlobal.Param);
            grdCabang.DataBind();
            
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            //PopulateCheckedValues_View();
        }

        protected void loadCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang in(2,3) and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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

        protected void ClearData()
        {
            txtNama.Text = "";
            dtKas.Text = "";
            Textnokontrak.Text = "";
            txtUraian.Text = "";
            LoadData();
            CloseMessage();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void grdCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdCabang.SelectedRow.RowIndex;
                hdnID.Value = grdCabang.DataKeys[rowIndex].Values[0].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatamProject_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtNama.Text = myRow["Project"].ToString();
                dtKas.Text = myRow["tglProject"].ToString();
                Textnokontrak.Text = myRow["nokontrak"].ToString();
                txtUraian.Text = myRow["keterangan"].ToString();

                CloseMessage();
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

      
     

      
    }
}