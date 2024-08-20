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
    public partial class TransBudgetView : System.Web.UI.Page
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
               
                DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string systembudget = myRow["systembudget"].ToString();

                if (systembudget == "Tahun Ajaran")
                {
                    cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                    cboYear.DataValueField = "id";
                    cboYear.DataTextField = "name";
                    cboYear.DataBind();
                    loadData(cboYear.Text, cboCabang.Text);
                }
                else if (systembudget == "Tahunan")
                {
                    cboYear1.DataSource = ObjDb.GetRows("select a.date id, a.date name from (select year(GETDATE()) date union all select year(GETDATE()) - 3 date union all select year(GETDATE()) - 2 date union all select year(GETDATE()) - 1 date union all select year(GETDATE()) + 1 date ) a");
                    cboYear1.DataValueField = "id";
                    cboYear1.DataTextField = "name";
                    cboYear1.DataBind();
                    loadData(cboYear1.Text, cboCabang.Text);
                }
                else
                {
                    cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                    cboYear.DataValueField = "id";
                    cboYear.DataTextField = "name";
                    cboYear.DataBind();
                    loadData(cboYear.Text, cboCabang.Text);
                }
            }
        }

        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'ALL' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }

        protected void loadData(string tahun = "", string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("ViewRBudget", ObjGlobal.Param);
            grdAccount.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            GridView1.DataSource = ObjGlobal.GetDataProcedure("ViewRBudget", ObjGlobal.Param);
            GridView1.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            GridView2.DataSource = ObjGlobal.GetDataProcedure("ViewRBudget", ObjGlobal.Param);
            GridView2.DataBind();


            DataSet mySet = ObjDb.GetRows("select systembudget from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["systembudget"].ToString();

            if (systembudget == "Tahun Ajaran")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = true;
                tahunan.Visible = false;
                divYear1.Visible = false;
                divYear.Visible = true;
                btnprintPajak.Visible = false;
                btnprintTahunAjaran.Visible = true;
            }
            else if (systembudget == "Tahunan")
            {
                pajak.Visible = false;
                tahunAjaran.Visible = false;
                tahunan.Visible = true;
                divYear1.Visible = true;
                divYear.Visible = false;
                btnprintPajak.Visible = false;
                btnprintTahunAjaran.Visible = false;
            }
            else
            {
                pajak.Visible = true;
                tahunAjaran.Visible = false;
                tahunan.Visible = false;
                divYear1.Visible = false;
                divYear.Visible = true;
                btnprintPajak.Visible = true;
                btnprintTahunAjaran.Visible = false;
            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear.Text, cboCabang.Text);
        }

        protected void cboYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear1.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cboYear.Text, cboCabang.Text);
        }
        protected void printPajak(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear(); 
            Param.Add("thn", cboYear.Text);
            Param.Add("nocabang", cboCabang.Text);

            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "RbudgetPajak.rpt";
            Session["REPORTTILE"] = "Report Budget Pajak";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
        protected void printTahunAjaran(object sender, EventArgs e)
        {
            HttpContext.Current.Session["ParamReport"] = null;
            Session["REPORTNAME"] = null;
            Session["REPORTTITLE"] = null;
            Param.Clear();
            Param.Add("thn", cboYear.Text);
            Param.Add("nocabang", cboCabang.Text);

            HttpContext.Current.Session.Add("ParamReport", Param);
            Session["REPORTNAME"] = "RbudgetTahunAjaran.rpt";
            Session["REPORTTILE"] = "Report Budget Tahun Ajaran";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);
        }
    }
}
