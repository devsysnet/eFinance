using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using eFinance.GlobalApp;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RLedger : System.Web.UI.Page
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
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                divtombol.Visible = false;
            }
        }


        #region LoadData
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("bln", cboMonth.Text);
            ObjGlobal.Param.Add("bln1", cboMonth1.Text);
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPviewRLedgerdetail", ObjGlobal.Param);
            grdAccount.DataBind();
            divtombol.Visible = true;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPviewRLedgerdetail", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@bln1", cboMonth1.Text));
                            cmd.Parameters.Add(new SqlParameter("@Search", txtSearch.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Buku_Besar_" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", ex.ToString());
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        protected void loadDataCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
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
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where parent='" + ObjSys.GetParentCabang + "' union SELECT distinct nocabang id, namaCabang name FROM vCabang where nocabang='" + ObjSys.GetParentCabang + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }
        #endregion
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Session["ParamReport"] = null;
                Session["REPORTNAME"] = null;
                Session["REPORTTITLE"] = null;
                Param.Clear();
                Param.Add("bln", cboMonth.Text);
                Param.Add("thn", cboYear.Text);
                Param.Add("nocabang", cboCabang.Text);
                Param.Add("Search", txtSearch.Text);
                HttpContext.Current.Session.Add("ParamReport", Param);
                Session["REPORTNAME"] = "RLedgerPrint.rpt";
                Session["REPORTTILE"] = "Report Cash Flow";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "OpenReport();", true);

            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }
        }

        protected void btnSearchCOA_Click(object sender, EventArgs e)
        {
            dlgAkun.Show();
            loadDataAkun();
        }

        protected void loadDataAkun()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            ObjGlobal.Param.Add("search", txtSearchAccount.Text);
            grdAkun.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAkun", ObjGlobal.Param);
            grdAkun.DataBind();
        }

        protected void grdAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdAkun.SelectedRow.RowIndex;

            string kodeAkun = (grdAkun.SelectedRow.FindControl("lblkdAkun") as Label).Text;
            string namaAkun = (grdAkun.SelectedRow.FindControl("lblKet") as Label).Text;
            string noAkun = (grdAkun.SelectedRow.FindControl("hdnnoAkun") as HiddenField).Value;

            txtSearch.Text = kodeAkun;

            dlgAkun.Hide();
        }

        protected void btnTutup_Click(object sender, EventArgs e)
        {
            dlgAkun.Hide();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        protected void btnSearchAccount_Click(object sender, EventArgs e)
        {
            dlgAkun.Show();
            loadDataAkun();
        }
    }
}
