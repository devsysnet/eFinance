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
    public partial class Rlistblmsetorparoki : System.Web.UI.Page
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
                LoadData();

            }
        }


        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadsetorKolekteall", ObjGlobal.Param);
            grdSiswa.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            //tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }

        protected void LoadDataCombo()
        {

            //cbobankasal.DataSource = ObjDb.GetRows("select norek,ket from mrekening where jenis=1 and sts=2 UNION select norek,ket from mRekening a inner join mcabang b on a.nocabang=b.noCabang where jenis=2 and b.nocabang='" + ObjSys.GetCabangId + "' and sts=2 and stsaktif=1");
            //cbobankasal.DataValueField = "norek";
            //cbobankasal.DataTextField = "ket";
            //cbobankasal.DataBind();
        }


        protected void grdSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nokas = grdSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nokas;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nokas", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadsetorKolektedetail", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    nomorKode.Text = myRow["nomorKode"].ToString();
                    tgl.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");
                    nilai.Text = myRow["Nilai"].ToString();
                    JenisKolekte.Text = myRow["JenisKolekte"].ToString();
                    uraian.Text = myRow["uraian"].ToString();
                    cbobankasal.Text = myRow["norek"].ToString();
                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

    }
}