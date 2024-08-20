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

namespace eFinance.Pages.Master.View
{
    public partial class Mstparametercabangview : System.Web.UI.Page
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
                //ObjSys.SessionCheck("MProjectupdate.aspx");
                LoadData();
                //LoadDataCombo();
            }
        }

     
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataviewparametercabang", ObjGlobal.Param);
            grdCabang.DataBind();
            //IndexPakai();
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            //PopulateCheckedValues_View();
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
            //txtajaranbaru.Text = "";
            //dtKas.Text = "";
            //dtKas1.Text = "";
            //cboitunggaji.Text = "";
            //cboPotonggaji.Text = "";
            //txtNilai.Text = "";
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
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataviewparametercabangdet", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                tahunAjaran.Text = myRow["tahunAjaran"].ToString();
                biayaadmbank.Text = myRow["biayaadmbank"].ToString();

                dtKas.Text = myRow["mulaithnajaran"].ToString();
                dtKas1.Text = myRow["akhirthnajaran"].ToString();
                cboitunggaji.Text = myRow["absengaji"].ToString();
                dtdari.Text = myRow["tglmulai"].ToString();
                dtsampai.Text = myRow["tglakhir"].ToString();
                cboPotonggaji.Text = myRow["absempotonggaji"].ToString();
                jammasuk.Text = myRow["jammasuk"].ToString();
                jamkeluar.Text = myRow["jamkeluar"].ToString();
                txtNilai.Text = myRow["upahminimum"].ToString();
                penggajian.Text = myRow["penggajian"].ToString();

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