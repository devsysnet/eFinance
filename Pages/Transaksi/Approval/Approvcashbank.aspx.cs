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
    public partial class Approvcashbank : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                loadData();
                loadDataCombo();
            }
        }


        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewKasBankApp", ObjGlobal.Param);
            grdKas.DataBind();
        }
        protected void loadDataCombo()
        {
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' --------- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis in(1,2)) a");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();

            cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang where stsMataUang = '1' ) a");
            cboCurrency.DataValueField = "id";
            cboCurrency.DataTextField = "name";
            cboCurrency.DataBind();

        }

        #region setInitial & AddRow
        private void SetInitialRow(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnKasBank", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemark", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPViewKasBankD", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnKasBank"] = myRow["noTkasDetil"].ToString();
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["hdnAccount"] = myRow["noTran"].ToString();
                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemark"] = myRow["Uraian"].ToString();
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["Debet"].ToString());
                dr["txtKredit"] = ObjSys.IsFormatNumber(myRow["Kredit"].ToString());

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["hdnKasBank"] = string.Empty;
                dr["txtAccount"] = string.Empty;
                dr["hdnAccount"] = string.Empty;
                dr["lblDescription"] = string.Empty;
                dr["txtRemark"] = string.Empty;
                dr["txtDebit"] = string.Empty;
                dr["txtKredit"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField hdnKasBank = (HiddenField)grdKasBank.Rows[i].FindControl("hdnKasBank");
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                        TextBox txtRemark = (TextBox)grdKasBank.Rows[i].FindControl("txtRemark");
                        Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        txtRemark.Text = dt.Rows[i]["txtRemark"].ToString();
                        txtDebit.Text = dt.Rows[i]["txtDebit"].ToString();
                        txtKredit.Text = dt.Rows[i]["txtKredit"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKas.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;

                DataSet mySet = ObjDb.GetRows("select * from tKas where noKas = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                Type.Text = myRow["Type"].ToString();
                cboAccount.Text = myRow["noRek"].ToString();
                cboCurrency.Text = myRow["noMataUang"].ToString();
                txtRemark.Text = myRow["Uraian"].ToString();
                txtValue.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                txtKurs.Text = ObjSys.IsFormatNumber(myRow["kursKas"].ToString());
                dtKas.Text = Convert.ToDateTime(myRow["Tgl"]).ToString("dd-MMM-yyyy");

                loadDataCombo();

                if (myRow["noMataUang"].ToString() == "20")
                {
                    Kurstrs.Visible = false;
                    txtKurs.Visible = false;
                }
                else
                {
                    Kurstrs.Visible = true;
                    txtKurs.Visible = true;
                }
                SetInitialRow(hdnId.Value);

                grdKasBank.Enabled = false;
                if (myRow["Type"].ToString() == "Kas/Bank Keluar")
                    txtTotalKredit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                else
                    txtTotalDebit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());

                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                showHideFormKas(false, true);
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        
        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideFormKas(true, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", hdnId.Value);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
            ObjGlobal.GetDataProcedure("SPVAppkas", ObjGlobal.Param);

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil diapprove.");

            loadData();
            showHideFormKas(true, false);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", hdnId.Value);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("nouser", ObjSys.GetUserId);
            ObjGlobal.GetDataProcedure("SPVrejectkas", ObjGlobal.Param);

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
            ShowMessage("success", "Data berhasil direject.");

            loadData();
            showHideFormKas(true, false);
        }


    }
}