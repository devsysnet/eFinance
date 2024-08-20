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
    public partial class TranspelunasanARview : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                loadDataFirst();
                ShowHideGridAndForm(true, true);
            }
        }
        protected void print(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear();
            Param.Add("nama", txtSearch.Text);
            Param.Add("noCabang", ObjSys.GetCabangId);
            Param.Add("tglbayar", dtBayar.Text);
            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "PrintDataKuwitansi2.rpt";
            Session["REPORTTILE"] = "Report Kwitansi";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
        private void loadDataCombo()
        {
            cboBank.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih Bank---' as name union select norek as id, kdrek+' - '+ket as name from mRekening where jenis in(2) and sts=2 and nocabang='" + ObjSys.GetCabangId + "' union select norek as id, kdrek+' - '+ket as name from mRekening where jenis in(1) and sts=2)x");
            cboBank.DataValueField = "id";
            cboBank.DataTextField = "name";
            cboBank.DataBind();

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Pilih Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

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
            tabForm.Visible = DivForm;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
            ObjGlobal.Param.Clear();

            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("bank", cboBank.SelectedValue);
            ObjGlobal.Param.Add("tglbayar", dtBayar.Text);
            ObjGlobal.Param.Add("nama", txtSearch.Text);

            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanTotalARView", ObjGlobal.Param);
            DataRow myRow = mySet.Tables[0].Rows[0];

            txtTotal.Text = myRow["total"].ToString();
            ShowHideGridAndForm(true, true);
            if(txtSearch.Text == "")
            {
btnprint.Visible = false;
            }else
            {
                btnprint.Visible = true;

            }

        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Kelas", cboKelas.SelectedValue);
            ObjGlobal.Param.Add("bank", cboBank.SelectedValue);
            ObjGlobal.Param.Add("tglbayar", dtBayar.Text);
            ObjGlobal.Param.Add("nama", txtSearch.Text);

            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);

            grdARSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPelunasanARView", ObjGlobal.Param);
            grdARSiswa.DataBind();
            if (grdARSiswa.Rows.Count == 0)
                txtTotal.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);
        }
        protected void grdARSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdARSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
            ShowHideGridAndForm(true, true);
        }
    }
}