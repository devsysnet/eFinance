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
    public partial class TransKasBankView : System.Web.UI.Page
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
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewKasBank", ObjGlobal.Param);
            grdKas.DataBind();
        }

        protected void loadDataCombo()
        {
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' --------- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' ) a");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();

            cboCurrency.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' --------- ' name union all SELECT distinct noMataUang id, namaMataUang name FROM mMataUang where stsMataUang = '1' ) a");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

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

                cboAccount.Text = myRow["noRek"].ToString();
                cboCurrency.Text = myRow["noMataUang"].ToString();
                cboTransaction.Text = myRow["type"].ToString();

                if (cboTransaction.Text == "1")
                {
                    cboFromToCus.Text = myRow["Dari"].ToString();
                    txtTotalDebit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                    txtTotalKredit.Text = "0.00";
                    showHideForm(true, false, false, true, false);
                }
                if (cboTransaction.Text == "2")
                {
                    cboFromToSupp.Text = myRow["Dari"].ToString();
                    txtTotalDebit.Text = "0.00";
                    txtTotalKredit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                    showHideForm(true, false, false, false, true);
                }
                txtBank.Text = myRow["Bank"].ToString();
                if (cboFromToCus.Text == "3" || cboFromToCus.Text == "4" || cboFromToCus.Text == "5")
                {
                    hdnCustomer.Value = myRow["noCus"].ToString();
                    txtCustomer.Text = myRow["Cust"].ToString();
                    showHideForm(false, true, false, true, false);
                }
                if (cboFromToSupp.Text == "6" || cboFromToSupp.Text == "7" || cboFromToSupp.Text == "8")
                {
                    hdnSupplier.Value = myRow["noCus"].ToString();
                    txtSupplier.Text = myRow["Cust"].ToString();
                    showHideForm(false, false, true, false, true);
                }
                if (cboFromToCus.Text == "2" || cboFromToSupp.Text == "2")
                {
                    hdnSalesman.Value = myRow["noCus"].ToString();
                    txtSalesman.Text = myRow["Cust"].ToString();
                }

                txtGiroNo.Text = myRow["noGiro"].ToString();
                txtRemark.Text = myRow["Uraian"].ToString();
                txtValue.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                dtKas.Text = Convert.ToDateTime(myRow["Tgl"]).ToString("dd-MMM-yyyy");
                dtGiro.Text = Convert.ToDateTime(myRow["Tgl1"]).ToString("dd-MMM-yyyy");

                SetInitialRow(hdnId.Value);
                prosesDebitOrKredit(cboTransaction.Text);
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

        protected void ClearData()
        {
            CloseMessage();

            dtKas.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            dtGiro.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            txtBank.Text = "";
            txtSalesman.Text = "";
            txtGiroNo.Text = "";
            txtRemark.Text = "";
            txtSearch.Text = "";
            txtCustomer.Text = "";
            txtSupplier.Text = "";
            txtTotalDebit.Text = "0.00";
            txtTotalKredit.Text = "0.00";
            txtValue.Text = "0.00";
            hdnSalesman.Value = "";
            //hdnParameterProd.Value = "";
            hdnCustomer.Value = "";
            hdnSupplier.Value = "";
            cboAccount.Text = "0";
            cboCurrency.Text = "0";
            cboFromToCus.Text = "0";
            cboTransaction.Text = "0";
            cboFromToSupp.Text = "0";
            cboFromToCus.Text = "0";

            SetInitialRow("0");

            loadData();
            loadDataCombo();
            //loadDataPopUp();

            showHideFormKas(true, false);
            showHideForm(false, false, false, true, false);
        }

        protected void showHideForm(bool DivSalesman, bool DivCustomer, bool DivSupplier, bool DivCboCust, bool DivCboSupp)
        {
            formSalesman.Visible = DivSalesman;
            formCustomer.Visible = DivCustomer;
            formSupplier.Visible = DivSupplier;
            formFromToCus.Visible = DivCboCust;
            forFormToSupp.Visible = DivCboSupp;
        }

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void prosesDebitOrKredit(string id = "")
        {
            for (int i = 0; i < grdKasBank.Rows.Count; i++)
            {
                TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");
                if (id == "1")
                {
                    txtDebit.Enabled = false;
                    txtKredit.Enabled = true;
                }
                else if (id == "2")
                {
                    txtDebit.Enabled = true;
                    txtKredit.Enabled = false;
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}