using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransRealisasiKasbon : System.Web.UI.Page
    {
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
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPLoadKasBon", ObjGlobal.Param);
            grdKas.DataBind();

        }
        #region rows
        private void SetInitialRow(string noPR = "")
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
            ObjGlobal.Param.Add("noPR", noPR);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDetilPR", ObjGlobal.Param);
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["kdRek"].ToString();
                dr["Column2"] = myRow["noRek"].ToString();
                dr["Column3"] = myRow["Ket"].ToString();
                dr["Column4"] = myRow["namakegiatan"].ToString();
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
        protected void grdMemoJurnal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
            string value = (grdMemoJurnal.SelectedRow.FindControl("txtHdnValue") as HiddenField).Value;
            txtHdnPopup.Value = value;
            txtSearch.Text = "";
        }
        #region PopUpProduct
        private void LoadDataPanel()
        {
            grdAkun.DataSource = ObjDb.GetRows("select * from mRekening where   (kdRek like '%" + txtSearch.Text + "%' and kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT' and sts = '2') or (Ket like '%" + txtSearch.Text + "%'  and kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT' and sts='2')");
            grdAkun.DataBind();
        }
        protected void btnCariAkun_Click(object sender, EventArgs e)
        {
            LoadDataPanel();
            mpe.Show();
        }



        protected void grdAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(txtHdnPopup.Value);
            string kode = "", nama = "", no = "";
            kode = (grdAkun.SelectedRow.FindControl("lblKodeAkun") as Label).Text;
            nama = (grdAkun.SelectedRow.FindControl("lblNamaAkun") as Label).Text;
            no = (grdAkun.SelectedRow.FindControl("hidNoAkun") as HiddenField).Value;

            TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtAccount");
            TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex - 1].FindControl("txtDescription");
            HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex - 1].FindControl("hdnAccount");

            int noCek = 0;
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                TextBox txtAccountt = (TextBox)grdMemoJurnal.Rows[i].Cells[1].FindControl("txtAccount");
                if (txtAccountt.Text == kode)
                {
                    noCek += 1;
                }
            }
            if (noCek > 0)
            {
                Response.Write("<script>alert('Account yang dipilih tidak boleh sama !');</script>");
                mpe.Show();
            }
            else
            {
                txtAccount.Text = kode;
                txtDescription.Text = nama;
                hdnAccount.Value = no;
            }

             mpe.Hide();
        }

        protected void grdAkun_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAkun.PageIndex = e.NewPageIndex;
            LoadDataPanel();
            mpe.Show();
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void grdMemoJurnal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnRowIndexMemoJurnal.Value = rowIndex.ToString();

                if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtAccount");
                    TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDescription");
                    TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtRemark");
                    TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[rowIndex].FindControl("txtKredit");
                    HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[rowIndex].FindControl("hdnAccount");
                    hdnAccount.Value = "";
                    txtAccount.Text = "";
                    txtDescription.Text = "";
                    txtRemark.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";
                    

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }



        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (cboType.Text == "0")
            {
                message += ObjSys.CreateMessage("Type tidak boleh kosong.");
                valid = false;
            }
            if (dtDate.Text == "")
            {
                message += ObjSys.CreateMessage("Date tidak boleh kosong.");
                valid = false;
            }
            if (Convert.ToDecimal(txtDebitTotal.Text) != Convert.ToDecimal(txtKreditTotal.Text))
            {
                message += ObjSys.CreateMessage("Total Debit Dan Kredit Tidak Sama.");
                valid = false;
            }
            if (Convert.ToDecimal(txtNilai.Text) != Convert.ToDecimal(txtKreditTotal.Text))
            {
                message += ObjSys.CreateMessage("Nilai Dan Kredit Tidak Sama.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("27", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));
                    DataSet mySet = ObjDb.GetRows("select top 1 * from tkasDetil where kdTran = '" + txtKodeKas.Text + "' and debet <> 0 ");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    //ObjDb.Data.Clear();
                    //ObjDb.Data.Add("kdTran", Kode);
                    //ObjDb.Data.Add("jenisTran", cboType.Text);
                    //ObjDb.Data.Add("noTran", hdnId.Value);
                    //ObjDb.Data.Add("noRek", myRow["norek"].ToString());
                    //ObjDb.Data.Add("kdRek", myRow["kdRek"].ToString());
                    //ObjDb.Data.Add("Uraian", "Realisasi Permintaan Dana");
                    //ObjDb.Data.Add("Debet", "0");
                    //ObjDb.Data.Add("Kredit", Convert.ToDecimal(txtDebit.Text).ToString());
                    //ObjDb.Data.Add("noMataUang", "20");
                    //ObjDb.Data.Add("kurs", "1");
                    //ObjDb.Data.Add("debetasing", "0");
                    //ObjDb.Data.Add("kreditasing", Convert.ToDecimal(txtNilai.Text).ToString());
                    //ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));
                    //ObjDb.Data.Add("sts", "1");
                    //ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                    //ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                    //ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                    //ObjDb.Data.Add("noKasbonDana", hdnId.Value);
                    //ObjDb.Insert("tKasdetil", ObjDb.Data);

                    for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
                    {
                        HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                        TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                        TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                        TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");
                        
                        if (txtAccount.Text != "")
                        {
                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("kasbondana", hdnId.Value);
                            ObjGlobal.Param.Add("kdTran", Kode);
                            ObjGlobal.Param.Add("jenisTran", cboType.Text);
                            ObjGlobal.Param.Add("noTran", hdnId.Value);
                            ObjGlobal.Param.Add("noRek", hdnAccount.Value);
                            ObjGlobal.Param.Add("kdRek", txtAccount.Text);
                            ObjGlobal.Param.Add("Uraian", txtRemark.Text);
                            ObjGlobal.Param.Add("Debet", Convert.ToDecimal(txtDebit.Text).ToString());
                            ObjGlobal.Param.Add("Kredit", "0");
                            ObjGlobal.Param.Add("noMataUang", "20");
                            ObjGlobal.Param.Add("kurs", "1");
                            ObjGlobal.Param.Add("debetasing", Convert.ToDecimal(txtDebitTotal.Text).ToString());
                            ObjGlobal.Param.Add("kreditasing", "0");
                            ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));
                            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                            ObjGlobal.GetDataProcedure("SPInsertRealisasiKasBon", ObjGlobal.Param);
                        }

                    }

                    if (Convert.ToDecimal(hdnNilaiLama.Value) != Convert.ToDecimal(txtNilai.Text))
                    {
                        decimal sisa = Convert.ToDecimal(hdnNilaiLama.Value) - Convert.ToDecimal(txtNilai.Text);

                        string kdrekkas = "-";
                        DataSet dataSN = ObjDb.GetRows("SELECT kdrek FROM mRekening WHERE norek = '" + cboAccount.Text + "'");
                        if (dataSN.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowSn = dataSN.Tables[0].Rows[0];
                            kdrekkas = myRowSn["kdrek"].ToString();
                        }

                        string noCus = "0", Cust = "";
                        DataSet dataKas = ObjDb.GetRows("SELECT noCus, Cust FROM tKas WHERE nomorKode = '" + txtKodeKas.Text + "' and noKas = '" + hdnnoKas.Value + "'");
                        if (dataKas.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowKas = dataKas.Tables[0].Rows[0];
                            noCus = myRowKas["noCus"].ToString();
                            Cust = myRowKas["noCus"].ToString();
                        }


                            ObjGlobal.Param.Clear();
                            ObjGlobal.Param.Add("nomorKode", Kode);
                            ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));
                            ObjGlobal.Param.Add("jenisTran", cboType.Text);
                            ObjGlobal.Param.Add("noRekum", myRow["norek"].ToString());
                            ObjGlobal.Param.Add("kdrekum", myRow["kdRek"].ToString());
                            ObjGlobal.Param.Add("noRekkas", cboAccount.Text.ToString());
                            ObjGlobal.Param.Add("kdRekkas", kdrekkas.ToString());
                            ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(sisa).ToString());
                            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                            ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                            ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                            ObjGlobal.Param.Add("noKasbonDana", hdnId.Value);
                            ObjGlobal.GetDataProcedure("SPInsertsisarealisasi", ObjGlobal.Param);
                    }


                    if (cboType.Text == "Pengembalian")
                    {
                        decimal sisa = Convert.ToDecimal(hdnNilaiLama.Value) - Convert.ToDecimal(txtNilai.Text);

                        string kdrekkas = "-";
                        DataSet dataSN = ObjDb.GetRows("SELECT kdrek FROM mRekening WHERE norek = '" + cboAccount.Text + "'");
                        if (dataSN.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowSn = dataSN.Tables[0].Rows[0];
                            kdrekkas = myRowSn["kdrek"].ToString();
                        }

                        string noCus = "0", Cust = "";
                        DataSet dataKas = ObjDb.GetRows("SELECT noCus, Cust FROM tKas WHERE nomorKode = '" + txtKodeKas.Text + "' and noKas = '" + hdnnoKas.Value + "'");
                        if (dataKas.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowKas = dataKas.Tables[0].Rows[0];
                            noCus = myRowKas["noCus"].ToString();
                            Cust = myRowKas["noCus"].ToString();
                        }


                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("nomorKode", Kode);
                        ObjGlobal.Param.Add("Tgl", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));
                        ObjGlobal.Param.Add("jenisTran", cboType.Text);
                        ObjGlobal.Param.Add("noRekum", myRow["norek"].ToString());
                        ObjGlobal.Param.Add("kdrekum", myRow["kdRek"].ToString());
                        ObjGlobal.Param.Add("noRekkas", cboAccount.Text.ToString());
                        ObjGlobal.Param.Add("kdRekkas", kdrekkas.ToString());
                        //ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(hdnNilaiLama).ToString());
                        ObjGlobal.Param.Add("Nilai", Convert.ToDecimal(hdnNilaiLama.Value).ToString());
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                        ObjGlobal.Param.Add("noKasbonDana", hdnId.Value);
                        ObjGlobal.GetDataProcedure("SPInsertsisarealisasi", ObjGlobal.Param);
                    }

                    ObjSys.UpdateAutoNumberCode("27", Convert.ToDateTime(ObjSys.GetNow).ToString("yyyy-MM-dd"));
                    ShowMessage("success", "Data berhasil disimpan.");
                    showHideFormKas(true, false);
                    loadData();
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
        private void clearData()
        {
            for (int i = 0; i < grdMemoJurnal.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdMemoJurnal.Rows[i].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtAccount");
                TextBox txtDescription = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDescription");
                TextBox txtRemark = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtRemark");
                TextBox txtDebit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtDebit");
                TextBox txtKredit = (TextBox)grdMemoJurnal.Rows[i].FindControl("txtKredit");

                hdnAccount.Value = "";
                txtAccount.Text = "";
                txtDescription.Text = "";
                txtRemark.Text = "";
                txtDebitTotal.Text = "";
                txtKreditTotal.Text = "";
                SetPreviousData();
                cboType.Text = "0";
                dtDate.Text = "";


            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
            showHideFormKas(true, false);
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKas.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void grdKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdKas.SelectedRow.RowIndex;
                string id = grdKas.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = id;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("id", id);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadKasBonDetil", ObjGlobal.Param);
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
                showHideNilai(false, false);
                string noPR = myRow["noIndex"].ToString();
                txtDebitTotal.Text = ObjSys.IsFormatNumber(myRow["nilai"].ToString());
                SetInitialRow(noPR);

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
            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();
        }

    }
}