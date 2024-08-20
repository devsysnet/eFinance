using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.Approval
{
    public partial class TransRevisiBudget : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loaddatacombo();
                loaddata(cboYear.Text, cboCabang.Text);
            }
        }
        protected void loaddatacombo()
        {
            cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
            cboYear.DataValueField = "id";
            cboYear.DataTextField = "name";
            cboYear.DataBind();

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
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
        }
        protected void loaddata(string tahun = "", string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("nocabang", cabang);
            grdBudget.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRevisiBudget", ObjGlobal.Param);
            grdBudget.DataBind();
            if (grdBudget.Rows.Count > 0)
                showhidebutton.Visible = true;
            else
                showhidebutton.Visible = false;
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < grdBudget.Rows.Count; i++)
                {
                    HiddenField hdnnoBudgetD = (HiddenField)grdBudget.Rows[i].FindControl("hdnnoBudgetD");
                    HiddenField hdnnoBudget = (HiddenField)grdBudget.Rows[i].FindControl("hdnnoBudget");
                    HiddenField hdnNoRek = (HiddenField)grdBudget.Rows[i].FindControl("hdnNoRek");
                    Label txtkdRek = (Label)grdBudget.Rows[i].FindControl("txtkdRek");
                    Label Ket = (Label)grdBudget.Rows[i].FindControl("Ket");
                    TextBox txtJanuari = (TextBox)grdBudget.Rows[i].FindControl("txtJanuari");
                    TextBox txtFebuari = (TextBox)grdBudget.Rows[i].FindControl("txtFebuari");
                    TextBox txtMaret = (TextBox)grdBudget.Rows[i].FindControl("txtMaret");
                    TextBox txtApril = (TextBox)grdBudget.Rows[i].FindControl("txtApril");
                    TextBox txtMei = (TextBox)grdBudget.Rows[i].FindControl("txtMei");
                    TextBox txtJuni = (TextBox)grdBudget.Rows[i].FindControl("txtJuni");
                    TextBox txtJuli = (TextBox)grdBudget.Rows[i].FindControl("txtJuli");
                    TextBox txtAgustus = (TextBox)grdBudget.Rows[i].FindControl("txtAgustus");
                    TextBox txtSeptember = (TextBox)grdBudget.Rows[i].FindControl("txtSeptember");
                    TextBox txtOktober = (TextBox)grdBudget.Rows[i].FindControl("txtOktober");
                    TextBox txtNovember = (TextBox)grdBudget.Rows[i].FindControl("txtNovember");
                    TextBox txtDesember = (TextBox)grdBudget.Rows[i].FindControl("txtDesember");


                    string jan = "0", feb = "0", mrt = "0", apr = "0", mei = "0", jun = "0", jul = "0", ags = "0", spt = "0", okt = "0", nov = "0", des = "0";
                    if (txtJanuari.Text != "")
                        jan = txtJanuari.Text;
                    if (txtFebuari.Text != "")
                        feb = txtFebuari.Text;
                    if (txtMaret.Text != "")
                        mrt = txtMaret.Text;
                    if (txtApril.Text != "")
                        apr = txtApril.Text;
                    if (txtMei.Text != "")
                        mei = txtMei.Text;
                    if (txtJuni.Text != "")
                        jun = txtJuni.Text;
                    if (txtJuli.Text != "")
                        jul = txtJuli.Text;
                    if (txtAgustus.Text != "")
                        ags = txtAgustus.Text;
                    if (txtSeptember.Text != "")
                        spt = txtSeptember.Text;
                    if (txtOktober.Text != "")
                        okt = txtOktober.Text;
                    if (txtNovember.Text != "")
                        nov = txtNovember.Text;
                    if (txtDesember.Text != "")
                        des = txtDesember.Text;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noCabang", cboCabang.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("tahun", cboYear.Text);
                    ObjGlobal.Param.Add("kdrek", txtkdRek.Text);
                    ObjGlobal.Param.Add("ket", Ket.Text);
                    ObjGlobal.Param.Add("budget1", Convert.ToDecimal(jan).ToString());
                    ObjGlobal.Param.Add("budget2", Convert.ToDecimal(feb).ToString());
                    ObjGlobal.Param.Add("budget3", Convert.ToDecimal(mrt).ToString());
                    ObjGlobal.Param.Add("budget4", Convert.ToDecimal(apr).ToString());
                    ObjGlobal.Param.Add("budget5", Convert.ToDecimal(mei).ToString());
                    ObjGlobal.Param.Add("budget6", Convert.ToDecimal(jun).ToString());
                    ObjGlobal.Param.Add("budget7", Convert.ToDecimal(jul).ToString());
                    ObjGlobal.Param.Add("budget8", Convert.ToDecimal(ags).ToString());
                    ObjGlobal.Param.Add("budget9", Convert.ToDecimal(spt).ToString());
                    ObjGlobal.Param.Add("budget10", Convert.ToDecimal(okt).ToString());
                    ObjGlobal.Param.Add("budget11", Convert.ToDecimal(nov).ToString());
                    ObjGlobal.Param.Add("budget12", Convert.ToDecimal(des).ToString());
                    ObjGlobal.GetDataProcedure("SPRevisiBudget", ObjGlobal.Param);
                }

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil direvisi");
                loaddatacombo();
                loaddata(cboYear.Text, cboCabang.Text);

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
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            loaddata(cboYear.Text, cboCabang.Text);
        }
    }
}