using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Transaksi.View
{
    public partial class TranscetakTagihanView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        //buatcetakan
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                loadDataCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                loadDataFirst();
            }

        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatapCetakPiutang", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
           
            //loadDataCombo();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void loadDataCombo()
        {

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '------' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang = '" + ObjSys.GetCabangId + "')x");
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

        protected void grdKelasSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKelasSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


                    HttpContext.Current.Session["ParamReport"] = null;
                    Session["REPORTNAME"] = null;
                    Session["REPORTTITLE"] = null;
                    Param.Clear();
                    Param.Add("bln", cboMonth.Text);
                    Param.Add("thn", cboYear.Text);
                    Param.Add("nosiswa", txtSearch.Text);
                    Param.Add("kelas", cboKelas.Text);
                    Param.Add("nocabang", ObjSys.GetCabangId);
                    HttpContext.Current.Session.Add("ParamReport", Param);
                    Session["REPORTNAME"] = "Rptvouchertagihan.rpt";
                    Session["REPORTTILE"] = "Voucher Cetak Tagihan";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
                

            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }

        }
    }
}