using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransRealisasiKasbonView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtCari.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdKasUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadKasBonUpdate", ObjGlobal.Param);
            grdKasUpdate.DataBind();

        }
        #region rows
        private void SetInitialRow(string noKasBonDana = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dt.Columns.Add(new DataColumn("Column6", typeof(string)));
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noKasBonDana", noKasBonDana);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDetilPRUpdate", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["kdRek"].ToString();
                dr["Column2"] = myRow["noRek"].ToString();
                dr["Column3"] = myRow["Ket"].ToString();
                dr["Column4"] = myRow["Uraian"].ToString();
                dr["Column5"] = ObjSys.IsFormatNumber(myRow["hargasatuan"].ToString());
                dr["Column6"] = string.Empty;
                dt.Rows.Add(dr);

            }

            ViewState["CurrentTable"] = dt;
            grdMemoJurnal.DataSource = dt;
            grdMemoJurnal.DataBind();

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
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["Column1"].ToString();
                        hdnAccount.Value = dt.Rows[i]["Column2"].ToString();
                        txtDescription.Text = dt.Rows[i]["Column3"].ToString();
                        txtRemark.Text = dt.Rows[i]["Column4"].ToString();
                        if (dt.Rows[i]["Column5"].ToString() == "")
                            txtDebit.Text = "0.00";
                        else
                            txtDebit.Text = dt.Rows[i]["Column5"].ToString();
                        if (dt.Rows[i]["Column6"].ToString() == "")
                            txtKredit.Text = "0.00";
                        else
                            txtKredit.Text = dt.Rows[i]["Column6"].ToString();
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["Column3"] = txtDescription.Text;
                        dtCurrentTable.Rows[i]["Column4"] = txtRemark.Text;
                        dtCurrentTable.Rows[i]["Column5"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["Column6"] = txtKredit.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdMemoJurnal.DataSource = dtCurrentTable;
                    grdMemoJurnal.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion
        protected void CloseMessage()
        {
            ((Label)Master.FindControl("lblMessage")).Text = "";
            ((Label)Master.FindControl("lblMessage")).Visible = false;
        }
        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
               
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            showHideFormKas(true, false);
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdKasUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKasUpdate.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void grdKasUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdKasUpdate.SelectedRow.RowIndex;
                string id = grdKasUpdate.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("id", id);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadKasBonDetilUpdate", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                chkAktif.Checked = false;
                txtKode.Text = myRow["kdtran"].ToString();
                dtDate.Text = Convert.ToDateTime(myRow["tgl"]).ToString("dd-MMM-yyyy");
                txtPeminta.Text = myRow["peminta"].ToString();
                hdnnoKas.Value = myRow["noKas"].ToString();
                txtKodeKas.Text = myRow["nomorKode"].ToString();
                txtNilai.Text = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                txtCatatan.Text = myRow["uraian"].ToString();
                hdnNilaiLama.Value = Convert.ToDecimal(myRow["nilai"]).ToString();
                txtKreditTotal.Text = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                loadDataCombo();

                if (Convert.ToDecimal(myRow["nilaiawal"]) != Convert.ToDecimal(myRow["nilai"]))
                {
                    chkAktif.Checked = true;
                    tampil2.Visible = true;
                }
                else
                {
                    chkAktif.Checked = false;
                    tampil2.Visible = false;
                }
                cboAccount.Text = myRow["noRek"].ToString();
                //showHideNilai(false, false);
                string noPR = myRow["noIndex"].ToString();
                txtDebitTotal.Text = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                SetInitialRow(hdnId.Value);

                grdMemoJurnal.Enabled = false;
                showHideFormKas(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void chkAktif_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAktif.Checked == true)
                showHideNilai(true, true);
            else
                showHideNilai(false, false);
        }

        protected void showHideNilai(bool divtampil, bool divtampil2)
        {
            txtNilai.Enabled = divtampil;
            tampil2.Visible = divtampil2;
        }

        protected void txtNilai_TextChanged(object sender, EventArgs e)
        {
            txtKreditTotal.Text = ObjSys.IsFormatNumber(txtNilai.Text);
        }

        protected void loadDataCombo()
        {
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Account--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis in(1,2)) a");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();
        }

    }
}