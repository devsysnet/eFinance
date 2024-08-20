using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransSaldoAwalGL : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                LoadData();
            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdSaldoGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadSaldoAwalGL", ObjGlobal.Param);
            grdSaldoGL.DataBind();
            if (grdSaldoGL.Rows.Count > 0)
            {
                btnSimpan.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                btnSimpan.Visible = false;
                btnReset.Visible = false;
            }
        }

        protected void grdSaldoGL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnPos = (HiddenField)e.Row.FindControl("hdnPos");
                TextBox txtDebet = (TextBox)e.Row.FindControl("txtDebet");
                TextBox txtKredit = (TextBox)e.Row.FindControl("txtKredit");
                if (hdnPos.Value == "1")
                {
                    txtDebet.Enabled = true;
                    txtKredit.Enabled = false;
                }
                else
                {
                    txtDebet.Enabled = false;
                    txtKredit.Enabled = true;
                }
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
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            TextBox txtTotalDebet = (TextBox)grdSaldoGL.FooterRow.FindControl("txtTotalDebet");
            TextBox txtTotalKredit = (TextBox)grdSaldoGL.FooterRow.FindControl("txtTotalKredit");
            if (Convert.ToDecimal(txtTotalDebet.Text) != Convert.ToDecimal(txtTotalKredit.Text))
            {
                message += ObjSys.CreateMessage("Debet Kredit harus balance.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
                    {
                        HiddenField hdnPos = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnPos");
                        HiddenField hdnNoRek = (HiddenField)grdSaldoGL.Rows[i].FindControl("hdnNoRek");
                        TextBox txtDebet = (TextBox)grdSaldoGL.Rows[i].FindControl("txtDebet");
                        TextBox txtKredit = (TextBox)grdSaldoGL.Rows[i].FindControl("txtKredit");
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noRek", hdnNoRek.Value);
                        ObjGlobal.Param.Add("kdRek", grdSaldoGL.Rows[i].Cells[1].Text);
                        ObjGlobal.Param.Add("tgl", cboYear.Text + "-" + cboMonth.Text + "-01");
                        ObjGlobal.Param.Add("debet", Convert.ToDecimal(txtDebet.Text).ToString());
                        ObjGlobal.Param.Add("kredit", Convert.ToDecimal(txtKredit.Text).ToString());
                        ObjGlobal.Param.Add("sts", "1");
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);

                        ObjGlobal.ExecuteProcedure("SPInsertGLSaldoAwal", ObjGlobal.Param);
                        ObjGlobal.ExecuteProcedure("SPInsertGLSaldoBulan", ObjGlobal.Param);
                    }

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("tgl", cboYear.Text + "-" + cboMonth.Text + "-01");
                    ObjGlobal.Param.Add("sts", "1");
                    ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                    ObjGlobal.ExecuteProcedure("SPInsertGLSaldoaktivitas", ObjGlobal.Param);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }
    }
}