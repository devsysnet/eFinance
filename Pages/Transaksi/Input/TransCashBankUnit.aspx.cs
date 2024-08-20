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
    public partial class TransCashBankUnit : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                dtKas.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");

                if (cboCurrency.Text == "20")
                    showhidekurs.Visible = false;
                else
                    showhidekurs.Visible = true;

                txtKurs.Text = "1.00";

                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }

   
            }
        }

        protected void LoadDataBank(string kdRek = "")
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", kdRek);
            if (cboTransaction.Text == "1")
                ObjGlobal.Param.Add("MasukKeluar", cboFromToCus.Text);
            else if (cboTransaction.Text == "2")
                ObjGlobal.Param.Add("MasukKeluar", cboFromToSupp.Text);
            else
                ObjGlobal.Param.Add("MasukKeluar", "0");
            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("kategoriusaha", ObjSys.GetKategori_Usaha);
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank", ObjGlobal.Param);
            grdBank.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPO.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPO", ObjGlobal.Param);
            grdPO.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPR", ObjGlobal.Param);
            grdPR.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchGaji.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdGaji.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGaji", ObjGlobal.Param);
            grdGaji.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchIuran.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdIuran.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataIuran", ObjGlobal.Param);
            grdIuran.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchKasBon.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdKasBon.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKasBon", ObjGlobal.Param);
            grdKasBon.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdcabanggaji.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataCabang", ObjGlobal.Param);
            grdcabanggaji.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchTHR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("tglTran", dtKas.Text);
            grdTHR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTHR", ObjGlobal.Param);
            grdTHR.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchAbsensi.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdAbsensi.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAbsensi", ObjGlobal.Param);
            grdAbsensi.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodePRKas.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPRKas.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPRKas", ObjGlobal.Param);
            grdPRKas.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPO", ObjGlobal.Param);
            //grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPengembalian", ObjGlobal.Param);
            grdPengembalian.DataBind();
        }

        #region loadData
        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            if (cboTransaction.Text == "1")
                ObjGlobal.Param.Add("MasukKeluar", cboFromToCus.Text);
            else if (cboTransaction.Text == "2")
                ObjGlobal.Param.Add("MasukKeluar", cboFromToSupp.Text);
            else
                ObjGlobal.Param.Add("MasukKeluar", "0");
            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("kategoriusaha", ObjSys.GetKategori_Usaha);
            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank", ObjGlobal.Param);
            grdBank.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPO.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPO", ObjGlobal.Param);
            grdPO.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPR", ObjGlobal.Param);
            grdPR.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchGaji.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdGaji.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataGaji", ObjGlobal.Param);
            grdGaji.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchIuran.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdIuran.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataIuran", ObjGlobal.Param);
            grdIuran.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchKasBon.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdKasBon.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKasBon", ObjGlobal.Param);
            grdKasBon.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdcabanggaji.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataCabang", ObjGlobal.Param);
            grdcabanggaji.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchTHR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("tglTran", dtKas.Text);
            grdTHR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTHR", ObjGlobal.Param);
            grdTHR.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchAbsensi.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdAbsensi.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAbsensi", ObjGlobal.Param);
            grdAbsensi.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodePRKas.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPRKas.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPRKas", ObjGlobal.Param);
            grdPRKas.DataBind();

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPO", ObjGlobal.Param);
            //grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPengembalian", ObjGlobal.Param);
            grdPengembalian.DataBind();

        }

        protected void loadDataCombo()
        {
            cboFromToCus.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Dari/Untuk---' name union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas where noJnsTransKas = '1' and kategoriUsaha is null union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas where noJnsTransKas = '1' and kategoriUsaha = '" + ObjSys.GetKategori_Usaha + "') a order by a.id");
            cboFromToCus.DataValueField = "id";
            cboFromToCus.DataTextField = "name";
            cboFromToCus.DataBind();

            cboFromToSupp.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Dari/Untuk---' name union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas where noJnsTransKas = '2' and kategoriUsaha is null union all SELECT distinct noTransKas id, JnsTransKas name FROM mJenisTransaksiKas where noJnsTransKas = '2' and kategoriUsaha = '" + ObjSys.GetKategori_Usaha + "') a order by a.id");
            cboFromToSupp.DataValueField = "id";
            cboFromToSupp.DataTextField = "name";
            cboFromToSupp.DataBind();

            string sql = "";
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                sql = "select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1' and headerakunkas = 'tampil') a order by a.id";
            }
            else
            {
                sql = "select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id";
            }

            cboAccount.DataSource = ObjDb.GetRows(sql);
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();

            cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang where stsMataUang = '1' ) a");
            cboCurrency.DataValueField = "id";
            cboCurrency.DataTextField = "name";
            cboCurrency.DataBind();

            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang=2) a order by urutan");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();


        }
        #endregion

        #region setInitial & AddRow
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("cbocabangunit", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtAccount"] = string.Empty;
            dr["hdnAccount"] = string.Empty;
            dr["cbocabangunit"] = ObjSys.GetCabangId;
            dr["lblDescription"] = string.Empty;
            dr["txtRemarkDetil"] = string.Empty;
            dr["txtDebit"] = string.Empty;
            dr["txtKredit"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

        }

        private void SetInitialRowOto(string noTransKas = "0")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("cbocabangunit", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noTransKas", noTransKas);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPmRekeningtranskasikas", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["cbocabangunit"] = ObjSys.GetCabangId;

                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemarkDetil"] = string.Empty;
                dr["txtDebit"] = string.Empty;
                dr["txtKredit"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }

        private void SetInitialRowTamp(string nomTampungan = "0", decimal nilai = 0)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("cbocabangunit", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nomTampungan", nomTampungan);
            ObjGlobal.Param.Add("nilai", Convert.ToDecimal(nilai).ToString());
            DataSet mySet = ObjGlobal.GetDataProcedure("SPDetilTampungan", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["txtRemarkDetil"] = string.Empty;
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["cbocabangunit"] = ObjSys.GetCabangId;

                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemarkDetil"] = string.Empty;
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["nilaiDebet"].ToString());
                dr["txtKredit"] = ObjSys.IsFormatNumber(myRow["nilaiKredit"].ToString());
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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                        TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i].FindControl("txtRemarkDetil");
                        Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");
                        DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i].FindControl("cbocabangunit");

                        cbocabangunit.DataSource = ObjDb.GetRows("select a.* from (select nocabang id,namacabang name,0 as noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang in (2,3) and parent = '" + ObjSys.GetParentCabang + "' and nocabang != '" + ObjSys.GetCabangId + "') a order by a.noUrut");
                        cbocabangunit.DataValueField = "id";
                        cbocabangunit.DataTextField = "name";
                        cbocabangunit.DataBind();

                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        cbocabangunit.Text = dt.Rows[i]["cbocabangunit"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        txtRemarkDetil.Text = dt.Rows[i]["txtRemarkDetil"].ToString();
                        txtDebit.Text = dt.Rows[i]["txtDebit"].ToString();
                        txtKredit.Text = dt.Rows[i]["txtKredit"].ToString();
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
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                        TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i].FindControl("txtRemarkDetil");
                        Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");
                        DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i].FindControl("cbocabangunit");

                     

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtAccount"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["hdnAccount"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["lblDescription"] = lblDescription.Text;
                        dtCurrentTable.Rows[i]["cbocabangunit"] = cbocabangunit.Text;
                        dtCurrentTable.Rows[i]["txtRemarkDetil"] = txtRemarkDetil.Text;
                        dtCurrentTable.Rows[i]["txtDebit"] = txtDebit.Text;
                        dtCurrentTable.Rows[i]["txtKredit"] = txtKredit.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdKasBank.DataSource = dtCurrentTable;
                    grdKasBank.DataBind();
                }
            }
            SetPreviousData();
        }
        #endregion

        #region Select & Pagging

        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = grdBank.SelectedRow.RowIndex;

                string kdRek = (grdBank.SelectedRow.FindControl("lblKdRek") as Label).Text;
                string Ket = (grdBank.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (grdBank.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;

                HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtAccount");
                Label lblDescription = (Label)grdKasBank.Rows[rowIndexHdn].FindControl("lblDescription");
                TextBox txtRemarkGrid = (TextBox)grdKasBank.Rows[rowIndexHdn].FindControl("txtRemarkDetil");

                //Account Boleh Sama 07092020
                //for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //{
                //    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                //    if (kdRek == kdRekBank.Text)
                //    {
                //        message += ObjSys.CreateMessage("Akun : " + kdRek + " sudah terpilih.");
                //        valid = false;
                //    }
                //}

                //Jika login pusat tetapi akun yang dipilih bukan pusat maka muncul proteksi
                //if (ObjSys.GetstsPusat == "4")
                //{
                //    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                //    {
                //        TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");

                //        DataSet mySet = ObjDb.GetRows("select * from mRekening where noCabang = "+ ObjSys.GetstsPusat + " and noRek = "+ noRek  + "");
                //        if (mySet.Tables[0].Rows.Count == 0)
                //        {
                //            //DataRow myRow = mySet.Tables[0].Rows[0];
                //            //string Id = myRow["noRek"].ToString();

                //            message += ObjSys.CreateMessage("Akun anya untuk Admin Kantor Yayasan.");
                //            valid = false;
                //        }
                //        else
                //            valid = true;
                //    }

                //}

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                    txtRemarkGrid.Text = txtRemark.Text;

                    txtSearch.Text = "";
                    loadData();
                    dlgBank.Hide();

                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgBank.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


        protected void cboFromToCus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            ClearDataSelected();

            if (cboFromToCus.Text == "1")

                showHideForm(false, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToCus.Text == "9")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false);

            else if (cboFromToCus.Text == "10")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false);

            else if (cboFromToCus.Text == "17")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);

            else if (cboFromToCus.Text == "19" || cboFromToCus.Text == "20" || cboFromToCus.Text == "21" || cboFromToCus.Text == "22")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToCus.Text == "15")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            if (cboFromToCus.Text == "19" || cboFromToCus.Text == "20" || cboFromToCus.Text == "21" || cboFromToCus.Text == "22")
            {

                loadDataTampKeluar();
                if (cboFromToCus.Text == "19" || cboFromToCus.Text == "20" || cboFromToCus.Text == "21" || cboFromToCus.Text == "22")
                {
                    SetInitialRowOto(cboFromToCus.Text);
                    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                    {
                        TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                        kdRekBank.Enabled = true;
                    }
                }

            }
            else
            {
                SetInitialRow();
                for (int i = 1; i < 3; i++)
                {
                    AddNewRow();
                }


                txtValue.Text = "0.00";
                txtTotalDebit.Text = "0.00";

                loadDataKegiatan();
            }
        }


        protected void cboFromToSupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            ClearDataSelected();

            if (cboFromToSupp.Text == "1")
                showHideForm(false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "2")
                showHideForm(false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "3")
                showHideForm(false, false, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "4")
                showHideForm(false, false, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "5")
                showHideForm(false, false, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "6")
                showHideForm(false, false, false, true, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "18")
                showHideForm(true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "7")
                showHideForm(false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "8")
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "10")
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false);

            else if (cboFromToSupp.Text == "11")
                showHideForm(false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "12")
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false);

            else if (cboFromToSupp.Text == "13")
                if (ObjSys.GetstsPusat == "4")
                    showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false);

                else
                {
                    showHideForm(false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    btnSimpan.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Hanya untuk Admin Kantor Yayasan.");
                }

            else if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "21" || cboFromToSupp.Text == "22")
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            //else if (cboFromToSupp.Text == "15")
            //     showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false);

            else if (cboFromToSupp.Text == "16")
                if (ObjSys.GetstsPusat == "4")
                    showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);

                else
                {
                    showHideForm(false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Hanya untuk Admin Kantor Yayasan.");
                }

            else if (cboFromToSupp.Text == "17")
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);

            if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "21" || cboFromToSupp.Text == "22" || cboFromToSupp.Text == "15" || (cboFromToSupp.Text == "16" && cboTampKeluar.Text != "0"))
            {

                loadDataTampKeluar();
                if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "15" || cboFromToSupp.Text == "21" || cboFromToSupp.Text == "22")
                {
                    SetInitialRowOto(cboFromToSupp.Text);
                    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                    {
                        TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                        kdRekBank.Enabled = true;
                    }
                }

                if (cboFromToSupp.Text == "16" && cboTampKeluar.Text != "0")
                {
                    SetInitialRowTamp(cboTampKeluar.Text, Convert.ToDecimal(txtValue.Text));
                }
            }

            else
            {
                SetInitialRow();
                for (int i = 1; i < 3; i++)
                {
                    AddNewRow();
                }
                for (int i = 0; i < grdKasBank.Rows.Count; i++)
                {

                    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                    kdRekBank.Enabled = true;
                }
            }

            txtValue.Text = "0.00";
            txtTotalDebit.Text = "0.00";

            loadDataUnitTransfer();

            loadDataUnitBPJS();
            loadDataKegiatan();
        }

        protected void loadDataUnitTransfer()
        {
            DataSet mySet = ObjDb.GetRows("select * from mcabang where noCabang = " + ObjSys.GetCabangId + "");

            DataRow myRow = mySet.Tables[0].Rows[0];
            string parent = myRow["parent"].ToString();
            string stsCabang = myRow["stsCabang"].ToString();
            string stsPusat = myRow["stsPusat"].ToString();


            if (stsCabang == "0" || stsCabang == "4")
            {
                cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang where stscabang in(2,3,4)) a order by a.noUrut");
                //cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + ObjSys.GetCabangId + "' or nocabang = '" + ObjSys.GetCabangId + "'  ) a order by a.noUrut");
                cboUnitTransfer.DataValueField = "id";
                cboUnitTransfer.DataTextField = "name";
                cboUnitTransfer.DataBind();
            }

            else if (stsCabang == "1" || stsCabang == "3")
            {
                cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + parent + "' or nocabang = '" + ObjSys.GetCabangId + "'  ) a order by a.noUrut");
                cboUnitTransfer.DataValueField = "id";
                cboUnitTransfer.DataTextField = "name";
                cboUnitTransfer.DataBind();
            }
            else if (stsCabang == "1" || stsCabang == "2")
            {
                cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + parent + "' and stscabang='3' ) a order by a.noUrut");
                //untuk ytk
                //cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "') a order by a.noUrut");
                cboUnitTransfer.DataValueField = "id";
                cboUnitTransfer.DataTextField = "name";
                cboUnitTransfer.DataBind();
            }


        }

        protected void loadDataUnitBPJS()
        {
            cboUnitBPJS.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and nocabang = '" + ObjSys.GetstsCabang + "') a order by a.noUrut");
            cboUnitBPJS.DataValueField = "id";
            cboUnitBPJS.DataTextField = "name";
            cboUnitBPJS.DataBind();
        }

        protected void loadDataTampKeluar()
        {
            cboTampKeluar.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Jenis-' name union all SELECT distinct nomTampungan id, jenisTampungan name FROM mTampPengeluaran_H) a");
            cboTampKeluar.DataValueField = "id";
            cboTampKeluar.DataTextField = "name";
            cboTampKeluar.DataBind();
        }

        protected void loadDataKegiatan()
        {
            cboKegiatan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Kegiatan-' name union all SELECT distinct noMKegiatan id, namaKegiatan name FROM mJenisKegiatan where nocabang='" + ObjSys.GetCabangId + "') a");
            cboKegiatan.DataValueField = "id";
            cboKegiatan.DataTextField = "name";
            cboKegiatan.DataBind();
        }
        #endregion

        #region Button
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string noProduk = "0.00";
            DataSet mySetH = ObjDb.GetRows("select TOP 1 nilaiKursPajak from tKursPajak where noMatauang = '" + cboCurrency.SelectedValue + "'");
            if (mySetH.Tables[0].Rows.Count > 0)
            {
                DataRow myRowH = mySetH.Tables[0].Rows[0];
                noProduk = myRowH["nilaiKursPajak"].ToString();
            }

            if (cboCurrency.Text == "20")
            {
                showhidekurs.Visible = false;
                txtKurs.Text = "1.00";
            }
            else
            {
                showhidekurs.Visible = true;
                txtKurs.Text = ObjSys.IsFormatNumber(noProduk).ToString();
            }

            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            int cekData = 0;
            for (int i = 0; i < grdKasBank.Rows.Count; i++)
            {
                TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                if (txtAccount.Text != "")
                {
                    cekData++;
                }
            }


            // cek sudah input / belum saldo awalnya, jika belum lepas protek post saldo kas terakhir
            DataSet dataSaldoKas = ObjDb.GetRows("select isnull(MAX(Tgl),'1/1/1900') as tglMaxPost from tSaldokas where noCabang = '" + ObjSys.GetCabangId + "' and norek = '" + cboAccount.Text + "' and noMataUang = '" + cboCurrency.Text + "'");
            if (dataSaldoKas.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSK = dataSaldoKas.Tables[0].Rows[0];
                string tglMaxPost = myRowSK["tglMaxPost"].ToString();
                // cek Post Saldo Kas Terakhir
                // sts = 0 udah posting, 1 = belum posting
                // cek Post Saldo Bulanan (Belum)
                if (Convert.ToDateTime(dtKas.Text) < Convert.ToDateTime(tglMaxPost))
                {
                    message += ObjSys.CreateMessage("Tanggal Kas harus lebih besar tanggal terakhir posting.");
                    valid = false;
                }
            }
            if (cboFromToSupp.SelectedValue == "11")
            {
                TextBox txtAccount = (TextBox)grdKasBank.Rows[0].FindControl("txtAccount");
                DataSet dataAkun = ObjDb.GetRows("SELECT norek,noCabang FROM mRekening WHERE kdRek = '" + txtAccount.Text + "'");

                DataRow myRowdataAkun = dataAkun.Tables[0].Rows[0];
                string noRek = myRowdataAkun["noRek"].ToString();
                string noCabang = myRowdataAkun["noCabang"].ToString();
                DataSet dataSaldoKas1 = ObjDb.GetRows("select tgl as tglMaxPost from tSaldokas where noCabang = '" + noCabang + "' and norek = '" + noRek + "' and tgl = ( select max(tgl) from tsaldokas  where noCabang = '" + noCabang + "' and norek = '" + noRek + "') ");
                if (dataSaldoKas1.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK1 = dataSaldoKas1.Tables[0].Rows[0];
                    string tglMaxPost = myRowSK1["tglMaxPost"].ToString();
                    // cek Post Saldo Kas Terakhir
                    // sts = 0 udah posting, 1 = belum posting
                    // cek Post Saldo Bulanan (Belum)

                    if (Convert.ToDateTime(dtKas.Text) < Convert.ToDateTime(tglMaxPost))
                    {
                        message += ObjSys.CreateMessage("Tanggal Kas harus lebih besar tanggal terakhir posting.");
                        valid = false;
                    }
                }


            }
            if (cboFromToSupp.SelectedValue == "12")
            {
                TextBox txtAccount = (TextBox)grdKasBank.Rows[0].FindControl("txtAccount");
                DataSet dataAkun = ObjDb.GetRows("SELECT norek FROM mRekening WHERE kdRek = '" + txtAccount.Text + "'");

                DataRow myRowdataAkun = dataAkun.Tables[0].Rows[0];
                string noRek = myRowdataAkun["noRek"].ToString();

                DataSet dataSaldoKas1 = ObjDb.GetRows("select tgl as tglMaxPost from tSaldokas where noCabang = '" + cboUnitTransfer.SelectedValue + "' and norek = '" + noRek + "' and tgl = ( select max(tgl) from tsaldokas  where noCabang = '" + cboUnitTransfer.SelectedValue + "' and norek = '" + noRek + "') ");
                if (dataSaldoKas1.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK1 = dataSaldoKas1.Tables[0].Rows[0];
                    string tglMaxPost = myRowSK1["tglMaxPost"].ToString();
                    // cek Post Saldo Kas Terakhir
                    // sts = 0 udah posting, 1 = belum posting
                    // cek Post Saldo Bulanan (Belum)

                    if (Convert.ToDateTime(dtKas.Text) < Convert.ToDateTime(tglMaxPost))
                    {
                        message += ObjSys.CreateMessage("Tanggal Kas harus lebih besar tanggal terakhir posting.");
                        valid = false;
                    }
                }


            }

            DataSet dataSaldobln1 = ObjDb.GetRows("select distinct month(tgl) as bln from tsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtKas.Text).Year + "' and month(tgl)='" + Convert.ToDateTime(dtKas.Text).Month + "'");
            if (dataSaldobln1.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSK = dataSaldobln1.Tables[0].Rows[0];
                int blnDb = int.Parse(myRowSK["bln"].ToString());

                //if (Convert.ToDateTime(dtKas.Text).Month != blnDb)
                //{
                message += ObjSys.CreateMessage("Sudah Posting Bulanan GL");
                valid = false;
                //}

            }

            DataSet dataSaldobln = ObjDb.GetRows("select distinct year(tgl) as thn from btsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtKas.Text).Year + "'");
            if (dataSaldobln.Tables[0].Rows.Count > 0)
            {
                DataRow myRowSK = dataSaldobln.Tables[0].Rows[0];
                int thnDb = int.Parse(myRowSK["thn"].ToString());

                //if (Convert.ToDateTime(dtKas.Text).Year = thnDb)
                //{
                message += ObjSys.CreateMessage("Sudah Posting Tahunan GL");
                valid = false;
                //}

            }

            if (dtKas.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Kas tidak boleh kosong.");
                valid = false;
            }
            else if (Convert.ToDateTime(dtKas.Text) > Convert.ToDateTime(ObjSys.GetNow))
            {
                message += ObjSys.CreateMessage("Tanggal Transaksi Tidak Boleh Lebih Besar Tanggal Hari ini.");
                valid = false;
            }
            else if (cboTransaction.Text == "0")
            {
                message += ObjSys.CreateMessage("Transaksi harus dipilih.");
                valid = false;
            }
            else if (cboAccount.Text == "0")
            {
                message += ObjSys.CreateMessage("Rekening harus dipilih.");
                valid = false;
            }
            else if (txtValue.Text == "")
            {
                message += ObjSys.CreateMessage("Nilai tidak boleh kosong.");
                valid = false;
            }
            else if (cboTransaction.Text == "1" && cboFromToCus.Text == "0")
            {
                message += ObjSys.CreateMessage("Dari / Untuk harus dipilih.");
                valid = false;
            }
            else if (cboTransaction.Text == "2" && cboFromToSupp.Text == "0")
            {
                message += ObjSys.CreateMessage("Dari / Untuk harus dipilih.");
                valid = false;
            }
            else if (txtOther.Text == "" && cboFromToCus.Text == "1")
            {
                message += ObjSys.CreateMessage("Catatan Lain-lain harus diisi.");
                valid = false;
            }
            else if (txtDanaBOS.Text == "" && cboFromToCus.Text == "10")
            {
                message += ObjSys.CreateMessage("Catatan Dana BOS harus diisi.");
                valid = false;
            }
            else if (txtOther.Text == "" && cboFromToSupp.Text == "1")
            {
                message += ObjSys.CreateMessage("Catatan Lain-lain harus diisi.");
                valid = false;
            }
            else if (txtDanaBOS.Text == "" && cboFromToSupp.Text == "10")
            {
                message += ObjSys.CreateMessage("Catatan Dana BOS harus diisi.");
                valid = false;
            }
            else if (txtRemark.Text == "")
            {
                message += ObjSys.CreateMessage("Uraian harus diisi.");
                valid = false;
            }
            else if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Akun Detil harus dipilih, minimal 1");
                valid = false;
            }
            else if (cboTransaction.Text == "2" && cboFromToSupp.Text == "12" && cboUnitTransfer.Text == "0")
            {
                message += ObjSys.CreateMessage("Perwakilan / Unit Transfer harus dipilih.");
                valid = false;
            }
            else if (cboTransaction.Text == "1" && cboFromToCus.Text == "17" && cboKegiatan.Text == "0")
            {
                message += ObjSys.CreateMessage("Kegiatan harus dipilih.");
                valid = false;
            }
            else if (cboTransaction.Text == "2" && cboFromToSupp.Text == "17" && cboKegiatan.Text == "0")
            {
                message += ObjSys.CreateMessage("Kegiatan harus dipilih.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    bool validate = true;

                    for (int ii = 0; ii < grdKasBank.Rows.Count; ii++)
                    {


                        HiddenField hdnAccount2 = (HiddenField)grdKasBank.Rows[ii].FindControl("hdnAccount");
                        TextBox txtAccount2 = (TextBox)grdKasBank.Rows[ii].FindControl("txtAccount");
                        if (txtAccount2.Text != "")
                        {
                            DataSet dataAkun = ObjDb.GetRows("SELECT kdrek, jenis FROM mRekening WHERE norek = '" + hdnAccount2.Value + "'");
                            string kdrekkas = "-", jnsAkun = "";
                            DataRow myRowAkun = dataAkun.Tables[0].Rows[0];
                            jnsAkun = myRowAkun["jenis"].ToString(); //1=Kas,2=Bank
                            kdrekkas = myRowAkun["kdrek"].ToString();
                            if (cboFromToSupp.Text == "11" && jnsAkun != "2")
                            {
                                message += ObjSys.CreateMessage("Rekening Harus yang Jenisnya Transfer Bank.");
                                validate = false;
                            }
                            else if (cboFromToSupp.Text == "12" && jnsAkun != "1")
                            {
                                message += ObjSys.CreateMessage("Rekening Harus yang Jenisnya Transfer Kas.");
                                validate = false;
                            }
                            else
                            {
                                validate = true;
                            }
                        }
                        if (validate == true)
                        {

                            string kurs = "1";
                            if (txtKurs.Text != "")
                                kurs = Convert.ToDecimal(txtKurs.Text).ToString();

                            if (Convert.ToDecimal(txtTotalDebit.Text) == Convert.ToDecimal(txtTotalKredit.Text))
                            {

                                decimal g = Convert.ToDecimal(kurs);
                                decimal f = Convert.ToDecimal(txtValue.Text);
                                decimal hasilakhir = g * f;

                                string kdrekkas = "-", jnsAkun = "";
                                DataSet dataAkun = ObjDb.GetRows("SELECT kdrek, jenis FROM mRekening WHERE norek = '" + cboAccount.Text + "'");
                                if (dataAkun.Tables[0].Rows.Count > 0)
                                {
                                    DataRow myRowAkun = dataAkun.Tables[0].Rows[0];
                                    kdrekkas = myRowAkun["kdrek"].ToString();
                                    jnsAkun = myRowAkun["jenis"].ToString(); //1=Kas,2=Bank
                                }

                                ObjDb.Data.Clear();
                                string jenistran = "-";
                                decimal totdebetkas = 0;
                                decimal totkreditkas = 0;
                                decimal totdebetkasrp = 0;
                                decimal totkreditkasrp = 0;
                                string Kode = "";
                                string kodeSN = "";
                                string typeNumber = "";
                                if (cboTransaction.Text == "1")
                                {
                                    Kode = ObjSys.GetCodeAutoNumberNewCustom("1", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(cboAccount.Text));

                                    jenistran = "Kas/Bank Masuk";
                                    totdebetkas = Convert.ToDecimal(txtValue.Text);
                                    totkreditkas = 0;
                                    totdebetkasrp = hasilakhir;
                                    totkreditkasrp = 0;
                                }

                                if (cboTransaction.Text == "2")
                                {
                                    Kode = ObjSys.GetCodeAutoNumberNewCustom("2", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(cboAccount.Text));

                                    jenistran = "Kas/Bank Keluar";
                                    totdebetkas = 0;
                                    totkreditkas = Convert.ToDecimal(txtValue.Text);
                                    totdebetkasrp = 0;
                                    totkreditkasrp = hasilakhir;
                                }
                                ObjDb.Data.Add("nomorKode", Kode);
                                ObjDb.Data.Add("type", jenistran);
                                ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                                ObjDb.Data.Add("noRek", cboAccount.Text);
                                ObjDb.Data.Add("kdrek", kdrekkas);
                                if (cboFromToCus.Text == "1" || cboFromToSupp.Text == "1")
                                //1=jenisnya lain2
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", txtOther.Text);
                                }
                                if (cboFromToSupp.Text == "2")
                                //2=jenisnya Pembelian dr PR
                                {
                                    ObjDb.Data.Add("noCus", hdnnoPO.Value);
                                    ObjDb.Data.Add("Cust", txtPO.Text);
                                }
                                if (cboFromToSupp.Text == "3")
                                //3=jenisnya Permintaan dr PR dana
                                {
                                    ObjDb.Data.Add("noCus", hdnnoPR.Value);
                                    ObjDb.Data.Add("Cust", txtPR.Text);
                                }
                                if (cboFromToSupp.Text == "4")
                                //4=jenisnya Gaji dr posting HR
                                {
                                    ObjDb.Data.Add("noCus", hdnnoGaji.Value);
                                    ObjDb.Data.Add("Cust", txtKodeGaji.Text);
                                }
                                if (cboFromToSupp.Text == "5")
                                //5=jenisnya Iuran(BPJS) dari Posting HR
                                {
                                    ObjDb.Data.Add("noCus", hdnnoIuran.Value);
                                    ObjDb.Data.Add("Cust", txtKodeIuran.Text);
                                }
                                if (cboFromToSupp.Text == "6")
                                //6=jenisnya Kas Bon
                                {
                                    ObjDb.Data.Add("noCus", hdnnoKasBon.Value);
                                    ObjDb.Data.Add("Cust", txtKodeKasbon.Text);
                                }
                                if (cboFromToSupp.Text == "18")
                                //18=jenisnya Gaji karyawan yg ambil dari posting gaji
                                {
                                    ObjDb.Data.Add("noCus", hdnnocabanggaji.Value);
                                    ObjDb.Data.Add("Cust", txtcabanggaji.Text);
                                }
                                if (cboFromToSupp.Text == "7")
                                //7=THR/Bouns
                                {
                                    ObjDb.Data.Add("noCus", hdnnoTHR.Value);
                                    ObjDb.Data.Add("Cust", txtKodeTHR.Text);
                                }
                                if (cboFromToSupp.Text == "8")
                                //8=absesni- keluarkan uang berdasarkan Absen
                                {
                                    ObjDb.Data.Add("noCus", hdnnoAbsensi.Value);
                                    ObjDb.Data.Add("Cust", txtKodeAbsensi.Text);
                                }
                                if (cboFromToCus.Text == "9")
                                //9=permintaan dr PR
                                {
                                    ObjDb.Data.Add("noCus", hdnnoPRKas.Value);
                                    ObjDb.Data.Add("Cust", txtKodePRKas.Text);
                                }
                                if (cboFromToCus.Text == "10" || cboFromToSupp.Text == "10")
                                //10=Dana BOS
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", txtDanaBOS.Text);
                                }
                                if (cboFromToSupp.Text == "12")
                                //12=Dana transfer kas
                                {
                                    ObjDb.Data.Add("noCus", "12");
                                    ObjDb.Data.Add("Cust", "Transfer Dana");
                                    ObjDb.Data.Add("noCabangTransfer", cboUnitTransfer.Text);
                                }
                                if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "21" || cboFromToCus.Text == "22")
                                //12=Dana gaji karyawan unit 14 ambil dari posting gaji -  21 jurnal manual
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", "Gaji Karyawan");
                                }
                                if (cboFromToSupp.Text == "11")
                                //11=Dana transfer Bank
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", txtOther.Text);
                                }
                                if (cboFromToSupp.Text == "13")
                                //13=catat Piutang UNit
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", txtKodePengembalian.Text);
                                    ObjDb.Data.Add("nohutang", hdnnoPengembalian.Value);
                                    ObjDb.Data.Add("nocabangTransfer", cboCabang.Text);
                                }
                                if (cboFromToSupp.Text == "16")
                                //16=tampungan dana keluar
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", "Tamp Keluar");
                                    ObjDb.Data.Add("nomtampungan", cboTampKeluar.Text);
                                }
                                if (cboFromToCus.Text == "17" || cboFromToSupp.Text == "17")
                                //17=Dana Kegiatan
                                {
                                    ObjDb.Data.Add("noCus", "0");
                                    ObjDb.Data.Add("Cust", "Kegiatan");
                                    ObjDb.Data.Add("noKegiatan", cboKegiatan.Text);
                                }

                                if (cboFromToCus.Text == "19")
                                //19=Pembayaran PPDB
                                {
                                    ObjDb.Data.Add("noCus", "19");
                                    ObjDb.Data.Add("Cust", "Pembayaran Di Muka");
                                }

                                if (cboFromToCus.Text == "20")
                                //20=Kolekte
                                {
                                    ObjDb.Data.Add("noCus", "20");
                                    ObjDb.Data.Add("Cust", "Kolekte Mingguan");
                                }

                                if (cboFromToCus.Text == "21")
                                //20=Stipendium
                                {
                                    ObjDb.Data.Add("noCus", "21");
                                    ObjDb.Data.Add("Cust", "Gaji Karyawan");
                                }

                                if (cboFromToCus.Text == "22")
                                //20=Stipendium
                                {
                                    ObjDb.Data.Add("noCus", "22");
                                    ObjDb.Data.Add("Cust", "Transfer Iuran ke Yayasan");
                                }

                                if (cboTransaction.Text == "1")
                                {
                                    ObjDb.Data.Add("dari", cboFromToCus.Text);
                                }
                                else
                                {
                                    ObjDb.Data.Add("dari", cboFromToSupp.Text);
                                }
                                if (txtAccount2.Text != "")
                                {
                                    ObjDb.Data.Add("Uraian", txtRemark.Text);
                                    ObjDb.Data.Add("noMataUang", cboCurrency.Text);
                                    ObjDb.Data.Add("kursKas", Convert.ToDecimal(kurs).ToString());
                                    ObjDb.Data.Add("Nilai", Convert.ToDecimal(txtValue.Text).ToString());
                                    ObjDb.Data.Add("nilaiRp", Convert.ToDecimal(hasilakhir).ToString());
                                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                    ObjDb.Data.Add("sts", "1");
                                    ObjDb.Data.Add("StsApv", "1");
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("tKas", ObjDb.Data);

                                    ObjSys.UpdateAutoNumberCodeNew("22", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));

                                    DataSet mySet = ObjDb.GetRows("select * from tkas where nomorKode = '" + Kode + "'");
                                    DataRow myRow = mySet.Tables[0].Rows[0];
                                    string Id = myRow["noKas"].ToString();

                                    ObjDb.Data.Clear();
                                    ObjDb.Data.Add("noKas", Id);
                                    ObjDb.Data.Add("kdTran", Kode);
                                    ObjDb.Data.Add("jenisTran", jenistran);
                                    ObjDb.Data.Add("noTran", Id);
                                    ObjDb.Data.Add("noRek", cboAccount.Text);
                                    ObjDb.Data.Add("kdRek", kdrekkas);
                                    ObjDb.Data.Add("Uraian", txtRemark.Text);
                                    ObjDb.Data.Add("Debet", totdebetkasrp.ToString());
                                    ObjDb.Data.Add("Kredit", totkreditkasrp.ToString());
                                    ObjDb.Data.Add("nomatauang", cboCurrency.Text);
                                    ObjDb.Data.Add("kurs", Convert.ToDecimal(kurs).ToString());
                                    ObjDb.Data.Add("debetasing", totdebetkas.ToString());
                                    ObjDb.Data.Add("kreditasing", totkreditkas.ToString());
                                    ObjDb.Data.Add("sts", "0");
                                    ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("tKasdetil", ObjDb.Data);


                                    for (int i = 0; i < grdKasBank.Rows.Count; i++)
                                    {
                                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                                        Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                                        TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i].FindControl("txtRemarkDetil");
                                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");
                                        DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i].FindControl("cbocabangunit");

                                        if (txtAccount.Text != "")
                                        {
                                            string Debit = "0.00", Kredit = "0.00";
                                            if (txtDebit.Text != "")
                                            {
                                                Debit = Convert.ToDecimal(txtDebit.Text).ToString();
                                            }
                                            if (txtKredit.Text != "")
                                            {
                                                Kredit = Convert.ToDecimal(txtKredit.Text).ToString();
                                            }
                                            decimal a = Convert.ToDecimal(kurs);
                                            decimal b = Convert.ToDecimal(Debit);
                                            decimal nilairpdebet = (a * b);

                                            decimal c = Convert.ToDecimal(kurs);
                                            decimal d = Convert.ToDecimal(Kredit);
                                            decimal nilairpkredit = (c * d);

                                            ObjDb.Data.Clear();
                                            ObjDb.Data.Add("noKas", Id);
                                            ObjDb.Data.Add("kdTran", Kode);
                                            ObjDb.Data.Add("jenisTran", jenistran);
                                            ObjDb.Data.Add("noTran", Id);
                                            ObjDb.Data.Add("noRek", hdnAccount.Value);
                                            ObjDb.Data.Add("kdRek", txtAccount.Text);
                                            ObjDb.Data.Add("Uraian", txtRemarkDetil.Text);
                                            ObjDb.Data.Add("Debet", Convert.ToDecimal(Debit).ToString());
                                            ObjDb.Data.Add("Kredit", Convert.ToDecimal(Kredit).ToString());
                                            ObjDb.Data.Add("kurs", Convert.ToDecimal(kurs).ToString());
                                            ObjDb.Data.Add("nomatauang", cboCurrency.Text);
                                            ObjDb.Data.Add("debetasing", Convert.ToDecimal(nilairpdebet).ToString());
                                            ObjDb.Data.Add("kreditasing", Convert.ToDecimal(nilairpkredit).ToString());
                                            ObjDb.Data.Add("sts", "0");
                                            ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                                            ObjDb.Data.Add("noCabang", cbocabangunit.Text);
                                            ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                            ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                            ObjDb.Insert("tKasdetil", ObjDb.Data);
                                        }

                                    }

                                    ObjGlobal.Param.Clear();
                                    ObjGlobal.Param.Add("noKas", Id);
                                    ObjGlobal.GetDataProcedure("SPinsertjurnalRKkaspusat", ObjGlobal.Param);


                                    if (cboTransaction.Text == "1")
                                    {
                                        ObjSys.UpdateAutoNumberCodeNewCustom("1", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(cboAccount.Text));
                                    }
                                    else
                                    {
                                        ObjSys.UpdateAutoNumberCodeNewCustom("2", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(cboAccount.Text));
                                    }

                                    if (cboFromToSupp.Text == "2")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.Param.Add("noPO", hdnnoPO.Value);
                                        ObjGlobal.GetDataProcedure("SPupdatejurnalPO", ObjGlobal.Param);

                                        string sqlpo = "update tHutang set nsaldoHutang = nsaldoHutang - '" + Convert.ToDecimal(txtValue.Text) + "', nSaldoRpHutang = nSaldoRpHutang - '" + Convert.ToDecimal(txtValue.Text) + "' where noHutang = '" + hdnnoPO.Value + "'";
                                        ObjDb.ExecQuery(sqlpo);
                                    }


                                    if (cboFromToSupp.Text == "3")
                                    {
                                        string PRDana = ObjSys.GetCodeAutoNumberNew("22", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));

                                        DataSet myPinta = ObjDb.GetRows("select a.peminta, b.noKaryawan,a.nopr from TransPR_H a inner join MstKaryawan b on a.peminta = b.nama where a.noPR = '" + hdnnoPR.Value + "'");
                                        DataRow myRowPinta = myPinta.Tables[0].Rows[0];
                                        string IdPinta = myRowPinta["noKaryawan"].ToString();
                                        string nopr = myRowPinta["nopr"].ToString();

                                        //string sqlPRDana = "insert into Transkasbondana (kdTran, tgl, noKas, peminta, nilai, sts, uraian, nopr,createdBy, createdate)" +
                                        //    " values ('" + PRDana + "', '" + Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd") + "', '" + Id + "', '" + IdPinta + "', '" + Convert.ToDecimal(txtValue.Text).ToString() + "', '1', '" + txtRemark.Text + "', '" + nopr + "', '" + ObjSys.GetUserId + "', '" + ObjSys.GetNow + "' )";
                                        //ObjDb.ExecQuery(sqlPRDana);

                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("kdTran", PRDana);
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.Param.Add("tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                                        ObjGlobal.Param.Add("peminta", IdPinta);
                                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtValue.Text).ToString());
                                        ObjGlobal.Param.Add("sts", "1");
                                        ObjGlobal.Param.Add("uraian", txtRemark.Text);
                                        ObjGlobal.Param.Add("nopr", nopr);
                                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                                        ObjGlobal.Param.Add("createdDate", ObjSys.GetNow);
                                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                                        ObjGlobal.GetDataProcedure("SPInsertkasbondana", ObjGlobal.Param);

                                    }


                                    if (cboFromToSupp.Text == "4")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.Param.Add("noGaji", hdnnoGaji.Value);
                                        ObjGlobal.GetDataProcedure("SPupdatejurnalgaji", ObjGlobal.Param);

                                        string sqlgaji = "update TtotaGajibln set sts = '1' where nototgajibln = '" + hdnnoGaji.Value + "'";
                                        ObjDb.ExecQuery(sqlgaji);

                                    }

                                    if (cboFromToSupp.Text == "5")
                                    {
                                        string sqliuran = "update ttotiuranbln set sts = '1' where noTotIuran = '" + hdnnoIuran.Value + "'";
                                        ObjDb.ExecQuery(sqliuran);
                                    }

                                    if (cboFromToSupp.Text == "6")
                                    {
                                        string BON = ObjSys.GetCodeAutoNumberNew("21", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));

                                        string sqlkasbon = "insert into tKasBon (kdKasBon,tgl,noKaryawan,nilai,angsuran,saldo,sts,noKas) " +
                                            "values ('" + BON + "','" + Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd") + "', '" + hdnnoKasBon.Value + "', '" + Convert.ToDecimal(txtValue.Text) + "', '" + Convert.ToDecimal(txtAngsuran.Text) + "', '" + Convert.ToDecimal(txtValue.Text) + "','0','" + Id + "')";
                                        ObjDb.ExecQuery(sqlkasbon);

                                        ObjSys.UpdateAutoNumberCodeNew("21", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));

                                    }

                                    if (cboFromToSupp.Text == "7")
                                    {
                                        string sqlthr = "update Tthrkaryawan set sts = '1' where nothr = '" + hdnnoTHR.Value + "'";
                                        ObjDb.ExecQuery(sqlthr);
                                    }

                                    if (cboFromToSupp.Text == "8")
                                    {
                                        string sqlabsensi = "update TtotAbsenbln set sts = '1' where noTotAbsen = '" + hdnnoAbsensi.Value + "'";
                                        ObjDb.ExecQuery(sqlabsensi);
                                    }

                                    if (cboFromToCus.Text == "9")
                                    {
                                        string sqlPRKas = "update tkas set stsPRKas = '1' where noKas = '" + hdnnoPRKas.Value + "'";
                                        ObjDb.ExecQuery(sqlPRKas);

                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPupdateststerimadana", ObjGlobal.Param);
                                    }

                                    if (cboFromToSupp.Text == "12")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPupdatejurnaltransfer", ObjGlobal.Param);

                                    }

                                    if (cboFromToSupp.Text == "11")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPupdatejurnaltransfer", ObjGlobal.Param);

                                    }

                                    if (cboFromToSupp.Text == "13")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPupdatejurnalpiutangunit", ObjGlobal.Param);

                                    }

                                    if (cboFromToSupp.Text == "16")
                                    {
                                        string sqlTampKeluar = "insert into Ttampunganpengeluaran (nokas,nomtampungan,tgl,nilai,nilaialokasi,saldo,nocabang,createdBy,createdDate) " +
                                            "values (" + Id + "," + cboTampKeluar.Text + ", '" + Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDecimal(txtTotalDebit.Text) + "',0, '" + Convert.ToDecimal(txtTotalDebit.Text) + "', '" + ObjSys.GetCabangId + "','" + ObjSys.GetUserId + "','" + ObjSys.GetNow + "')";
                                        ObjDb.ExecQuery(sqlTampKeluar);

                                    }

                                    if (cboFromToCus.Text == "15")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPupdatejenistran", ObjGlobal.Param);
                                    }


                                    if (cboFromToCus.Text == "19")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPDeletetkasdetilnol", ObjGlobal.Param);
                                    }


                                    //if (cboFromToSupp.Text == "18")
                                    //{
                                    //    ObjGlobal.Param.Clear();
                                    //    ObjGlobal.Param.Add("noKas", Id);
                                    //    ObjGlobal.GetDataProcedure("SPupdatejurnalgajiunit", ObjGlobal.Param);
                                    //}


                                    if (cboFromToSupp.Text == "21")
                                    {
                                        ObjGlobal.Param.Clear();
                                        ObjGlobal.Param.Add("noKas", Id);
                                        ObjGlobal.GetDataProcedure("SPInsertJurnalPremi", ObjGlobal.Param);
                                    }
                                }

                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                ShowMessage("success", "Data berhasil disimpan.");
                                ClearData();

                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                                ShowMessage("error", "Total debit dan kredit tidak sama.");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
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
            ClearData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
            dlgBank.Show();
        }

        #endregion

        #region Other
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

        protected void ClearDataSelected()
        {
            txtOther.Text = "";
            txtPO.Text = "";
            txtPR.Text = "";
            txtKodeGaji.Text = "";
            txtKodeIuran.Text = "";
            txtKodeKasbon.Text = "";
            txtcabanggaji.Text = "";
            txtKodeTHR.Text = "";
            txtKodeAbsensi.Text = "";
            txtKodePRKas.Text = "";
            txtDanaBOS.Text = "";
            hdnnoPO.Value = "";
            hdnnoPR.Value = "";
            hdnnoGaji.Value = "";
            hdnnoIuran.Value = "";
            hdnnoKasBon.Value = "";
            hdnnocabanggaji.Value = "";
            hdnnoTHR.Value = "";
            hdnnoAbsensi.Value = "";
            hdnnoPRKas.Value = "";
            txtTotalDebit.Text = "0.00";
            txtTotalKredit.Text = "0.00";
            cboUnitTransfer.Text = "0";
            //cboFromToCus.Text = "0";
            //cboFromToSupp.Text = "0";
        }
        protected void ClearData()
        {
            dtKas.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            txtPO.Text = "";
            txtRemark.Text = "";
            txtSearch.Text = "";
            txtOther.Text = "";
            txtPR.Text = "";
            txtKodeGaji.Text = "";
            txtKodeIuran.Text = "";
            txtKodeKasbon.Text = "";
            txtcabanggaji.Text = "";
            txtKodeTHR.Text = "";
            txtKodeAbsensi.Text = "";
            txtKodePRKas.Text = "";
            txtDanaBOS.Text = "";
            txtTotalDebit.Text = "0.00";
            txtTotalKredit.Text = "0.00";
            txtValue.Text = "0.00";
            hdnParameterProd.Value = "";
            hdnnoPO.Value = "";
            hdnnoPR.Value = "";
            hdnnoGaji.Value = "";
            hdnnoIuran.Value = "";
            hdnnoKasBon.Value = "";
            hdnnoKasBon.Value = "";
            hdnnoTHR.Value = "";
            hdnnoAbsensi.Value = "";
            hdnnoPRKas.Value = "";
            cboAccount.Text = "0";
            cboTransaction.Text = "0";
            cboFromToCus.Text = "0";
            cboFromToSupp.Text = "0";
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }
            loadDataCombo();
            showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
        }

        protected void showHideForm(bool Divcabanggaji, bool DivPO, bool DivCboCust, bool DivCboSupp, bool DivOther, bool DivPR, bool DivGaji, bool DivIuran, bool DivKasBon, bool DivTHR, bool DivAngsuran, bool DivAbsensi, bool DivPRKas, bool DivDanaBOS, bool DivTransfer, bool DivPengembalian, bool DivBPJS, bool DivTampKeluar, bool DivKegiatan)
        {
            Formcabanggaji.Visible = Divcabanggaji;
            formPO.Visible = DivPO;
            formFromToCus.Visible = DivCboCust;
            forFormToSupp.Visible = DivCboSupp;
            formOther.Visible = DivOther;
            formPR.Visible = DivPR;
            formGaji.Visible = DivGaji;
            formIuran.Visible = DivIuran;
            formKasBon.Visible = DivKasBon;
            formTHR.Visible = DivTHR;
            formAngsuran.Visible = DivAngsuran;
            formAbsen.Visible = DivAbsensi;
            formPRKas.Visible = DivPRKas;
            formDanaBOS.Visible = DivDanaBOS;
            formTransfer.Visible = DivTransfer;
            formPengembalian.Visible = DivPengembalian;
            formBPJS.Visible = DivBPJS;
            formTampKeluar.Visible = DivTampKeluar;
            formKegiatan.Visible = DivKegiatan;
        }

        #endregion

        protected void loadDataReimbersment()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodeReimbersment.Text);
            grdReimbersment.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataReimbersment", ObjGlobal.Param);
            grdReimbersment.DataBind();
        }

        protected void btnReimbersment_Click(object sender, ImageClickEventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void btnCariReimbersment_Click(object sender, EventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdReimbersment.PageIndex = e.NewPageIndex;
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void grdReimbersment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CloseMessage();

                int rowIndex = grdReimbersment.SelectedRow.RowIndex;
                string Id = grdReimbersment.DataKeys[rowIndex].Values[0].ToString();
                string Kd = grdReimbersment.SelectedRow.Cells[1].Text;
                string nilai = grdReimbersment.SelectedRow.Cells[3].Text;
                string ket = grdReimbersment.SelectedRow.Cells[4].Text;


                txtValue.Text = ObjSys.IsFormatNumber(nilai);
                txtRemark.Text = ket;

                txtTotalDebit.Text = ObjSys.IsFormatNumber(nilai);
                txtTotalKredit.Text = ObjSys.IsFormatNumber(nilai);

                SetInitialRowReim(Id);

                dlgReimbersment.Hide();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        private void SetInitialRowReim(string Id = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnAccount", typeof(string)));
            dt.Columns.Add(new DataColumn("lblDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("cbocabangunit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
            dt.Columns.Add(new DataColumn("txtDebit", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKredit", typeof(string)));

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Id", Id);
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadReimbursmentKasBank", ObjGlobal.Param);

            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["txtAccount"] = myRow["kdRek"].ToString();
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["cbocabangunit"] = ObjSys.GetCabangId;

                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemarkDetil"] = string.Empty;
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["value"].ToString());
                dr["txtKredit"] = string.Empty;
                dt.Rows.Add(dr);
            }

            ViewState["CurrentTable"] = dt;
            grdKasBank.DataSource = dt;
            grdKasBank.DataBind();

            SetPreviousData();
        }

        protected void btnPO_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPO.Show();
        }

        protected void btnSearchPO_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPO.PageIndex = e.NewPageIndex;
            loadData();
            dlgPO.Show();
        }

        protected void grdPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPO.SelectedRow.RowIndex;
                string Id = grdPO.DataKeys[rowIndex].Values[0].ToString();
                string kodePO = grdPO.SelectedRow.Cells[1].Text;

                hdnnoPO.Value = Id;
                txtPO.Text = kodePO;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPO", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPO", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    //txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }

                DataSet mySet = ObjDb.GetRows("select distinct a.noPO, c.noRek, c.kdRek, c.Ket, d.nSaldoHutang as hargasatuan " +
                    "from TransPO_D a inner join mBarang b on a.noBarang = b.noBarang inner join mRekening " +
                    "c on c.noRek = b.norek inner join thutang d on d.nopo = a.nopo where d.nohutang = '" + Id + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                string dpp = myRow["hargasatuan"].ToString();

                txtValue.Text = ObjSys.IsFormatNumber(dpp);

                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalKredit.Text = ObjSys.IsFormatNumber(dpp);

                loadData();
                dlgPO.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnPR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPR.Show();
        }

        protected void btnSearchPR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPR.PageIndex = e.NewPageIndex;
            loadData();
            dlgPR.Show();
        }

        protected void grdPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPR.SelectedRow.RowIndex;
                string Id = grdPR.DataKeys[rowIndex].Values[0].ToString();
                string kodePR = grdPR.SelectedRow.Cells[1].Text;
                string uraian = grdPR.SelectedRow.Cells[4].Text;

                hdnnoPR.Value = Id;
                txtPR.Text = kodePR;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPR", Id);
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtRemarkDetil");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtRemarkDetil.Text = uraian;
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    //txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }
                DataSet mySet = ObjDb.GetRows("select SUM(isnull(budget,0)) as hargasatuan " +
                    "from TransPR_D where noPR = '" + hdnnoPR.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                //txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(myRow["hargasatuan"]).ToString());
                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                //txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(myRow["hargasatuan"]).ToString());
                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtRemark.Text = uraian;
                loadData();
                dlgPR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnGaji_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgGaji.Show();
        }

        protected void btnIuran_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgIuran.Show();
        }

        protected void btnKasBon_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgKasBon.Show();
        }

        protected void btncabanggaji_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgcabanggaji.Show();
        }

        protected void btnTHR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgTHR.Show();
        }

        protected void btnSearchGaji_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgGaji.Show();
        }

        protected void grdGaji_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdGaji.SelectedRow.RowIndex;
                string Id = grdGaji.DataKeys[rowIndex].Values[0].ToString();
                string kodegaji = grdGaji.SelectedRow.Cells[1].Text;

                hdnnoGaji.Value = Id;
                txtKodeGaji.Text = kodegaji;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noGaji", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankGaji", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                loadData();
                dlgGaji.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchIuran_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdGaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgIuran.Show();
        }

        protected void grdIuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdIuran.SelectedRow.RowIndex;
                string Id = grdIuran.DataKeys[rowIndex].Values[0].ToString();
                string kodeIuran = grdIuran.SelectedRow.Cells[1].Text;

                hdnnoIuran.Value = Id;
                txtKodeIuran.Text = kodeIuran;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noIuran", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankIuran", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                loadData();
                dlgIuran.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnSearchKasBon_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgKasBon.Show();
        }

        protected void btnSearchcabanggaji_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgcabanggaji.Show();
        }

        protected void grdKasBon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdKasBon.PageIndex = e.NewPageIndex;
            loadData();
            dlgKasBon.Show();
        }

        protected void grdcabanggaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdcabanggaji.PageIndex = e.NewPageIndex;
            loadData();
            dlgcabanggaji.Show();
        }

        protected void grdKasBon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdKasBon.SelectedRow.RowIndex;
                string Id = grdKasBon.DataKeys[rowIndex].Values[0].ToString();
                string kode = grdKasBon.SelectedRow.Cells[1].Text;

                hdnnoKasBon.Value = Id;
                txtKodeKasbon.Text = kode;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasbon", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankKasBon", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(txtValue.Text).ToString();

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(txtDebit.Text);
                }

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                loadData();
                dlgKasBon.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


        protected void grdcabanggaji_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();


                //int rowIndex = grdcabanggaji.SelectedRow.RowIndex;
                //string Id = grdcabanggaji.DataKeys[rowIndex].Values[0].ToString();
                //string kode = grdcabanggaji.SelectedRow.Cells[1].Text;

                //hdnnocabanggaji.Value = Id;
                //txtcabanggaji.Text = kode;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadRekeninggaji1", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(txtValue.Text).ToString();

                    txtDebit.Enabled = true;
                    totaldebit += Convert.ToDecimal(txtDebit.Text);
                }

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                //loadData();
                dlgcabanggaji.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }


        protected void btnSearchTHR_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdTHR.PageIndex = e.NewPageIndex;
            loadData();
            dlgTHR.Show();
        }

        protected void grdTHR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdTHR.SelectedRow.RowIndex;
                string Id = grdTHR.DataKeys[rowIndex].Values[0].ToString();
                string kodeTHR = grdTHR.SelectedRow.Cells[1].Text;

                hdnnoTHR.Value = Id;
                txtKodeTHR.Text = kodeTHR;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noTHR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankTHR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                loadData();
                dlgTHR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void grdKasBank_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtAccount");
                    if (txtAccount.Text != "")
                    {
                        CloseMessage();
                        LoadDataBank(txtAccount.Text);
                        dlgBank.Show();
                    }
                    if (txtAccount.Text == "")
                    {
                        CloseMessage();
                        loadData();
                        //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                        //hdnParameterProd.Value = value;
                        dlgBank.Show();
                    }


                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[rowIndex].FindControl("hdnAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[rowIndex].FindControl("lblDescription");
                    TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtRemarkDetil");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[rowIndex].FindControl("cbocabangunit");

                    TextBox txtDebit = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtDebit");
                    TextBox txtKredit = (TextBox)grdKasBank.Rows[rowIndex].FindControl("txtKredit");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";
                    txtRemarkDetil.Text = "";
                    txtDebit.Text = "";
                    txtKredit.Text = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();CalculateDiscount();", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnSearchAbsensi_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdAbsensi.PageIndex = e.NewPageIndex;
            loadData();
            dlgAbsensi.Show();
        }

        protected void grdAbsensi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdAbsensi.SelectedRow.RowIndex;
                string Id = grdAbsensi.DataKeys[rowIndex].Values[0].ToString();
                string kodeAbsensi = grdAbsensi.SelectedRow.Cells[1].Text;

                hdnnoAbsensi.Value = Id;
                txtKodeAbsensi.Text = kodeAbsensi;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noAbsensi", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankAbsensi", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                }

                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                loadData();
                dlgAbsensi.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void btnAbsen_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgAbsensi.Show();
        }

        protected void btnPRKas_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPRKas.Show();
        }

        protected void btnSearchPRKas_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPRKas.PageIndex = e.NewPageIndex;
            loadData();
            dlgPRKas.Show();
        }

        protected void grdPRKas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPRKas.SelectedRow.RowIndex;
                string Id = grdPRKas.DataKeys[rowIndex].Values[0].ToString();
                string kodeKas = grdPRKas.SelectedRow.Cells[1].Text;

                hdnnoPRKas.Value = Id;
                txtKodePRKas.Text = kodeKas;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noKasPR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPRKas", ObjGlobal.Param);
                if (cboTransaction.Text == "2")
                {
                    int i = 0;
                    decimal totaldebit = 0;
                    foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                    {
                        i++;
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                        Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");
                        DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");


                        hdnAccount.Value = myRowH["noRek"].ToString();
                        txtAccount.Text = myRowH["kdRek"].ToString();
                        lblDescription.Text = myRowH["Ket"].ToString();
                        txtDebit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                        txtDebit.Enabled = false;
                        totaldebit += Convert.ToDecimal(myRowH["total"].ToString());
                    }
                    txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                    txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                    txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());

                    loadData();
                    dlgPRKas.Hide();
                }

                if (cboTransaction.Text == "1")
                {
                    int i = 0;
                    decimal totalkredit = 0;
                    foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                    {
                        i++;
                        HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                        TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                        Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                        DropDownList cbocabangunit = (DropDownList)grdKasBank.Rows[i - 1].FindControl("cbocabangunit");

                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtKredit");

                        hdnAccount.Value = myRowH["noRek"].ToString();
                        txtAccount.Text = myRowH["kdRek"].ToString();
                        lblDescription.Text = myRowH["Ket"].ToString();
                        txtKredit.Text = ObjSys.IsFormatNumber(myRowH["total"].ToString());

                        txtKredit.Enabled = false;
                        totalkredit += Convert.ToDecimal(myRowH["total"].ToString());
                    }
                    txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totalkredit).ToString());

                    txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totalkredit).ToString());
                    txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totalkredit).ToString());

                    loadData();
                    dlgPRKas.Hide();
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void txtDebit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtDebit = (TextBox)sender;
            var row = (GridViewRow)txtDebit.NamingContainer;

            TextBox txtKredit = (TextBox)row.FindControl("txtKredit");
            if (txtDebit.Text != "0.00" && txtDebit.Text != "0")
                txtKredit.Enabled = false;
            else
                txtKredit.Enabled = true;
        }

        protected void txtKredit_TextChanged(object sender, EventArgs e)
        {
            TextBox txtKredit = (TextBox)sender;
            var row = (GridViewRow)txtKredit.NamingContainer;

            TextBox txtDebit = (TextBox)row.FindControl("txtDebit");
            if (txtKredit.Text != "0.00" && txtKredit.Text != "0")
                txtDebit.Enabled = false;
            else
                txtDebit.Enabled = true;
        }

        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();

            if (cboTransaction.Text == "1")
            {
                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                txtTotalDebit.Text = ObjSys.IsFormatNumber(txtValue.Text);
                txtTotalKredit.Text = "0.00";
            }
            else if (cboTransaction.Text == "2")
            {
                showHideForm(false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                txtTotalDebit.Text = "0.00";
                txtTotalKredit.Text = ObjSys.IsFormatNumber(txtValue.Text);
            }

            cboFromToCus.Text = "0";
            cboFromToSupp.Text = "0";
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }

        }

        protected void btnPengembalian_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPengembalian.Show();
        }

        protected void btnSearchPengembelian_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPengembalian.Show();
        }

        protected void grdPengembalian_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdPengembalian.PageIndex = e.NewPageIndex;
            loadData();
            dlgPengembalian.Show();
        }

        protected void grdPengembalian_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();

                int rowIndex = grdPengembalian.SelectedRow.RowIndex;
                string Id = grdPengembalian.DataKeys[rowIndex].Values[0].ToString();
                string kodePO = grdPengembalian.SelectedRow.Cells[1].Text;

                hdnnoPengembalian.Value = Id;
                txtKodePengembalian.Text = kodePO;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("norekkredit", hdnnoPengembalian.Value);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPengembalian", ObjGlobal.Param);
                int i = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();

                }

                loadData();
                dlgPengembalian.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }

        protected void autoComplete()
        {
            for (int i = 0; i < grdKasBank.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i].FindControl("hdnAccount");
                TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].FindControl("txtAccount");
                Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i].FindControl("txtRemarkDetil");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("kdRek", kdRekBank.Text.Replace(" ", ""));
                DataSet mySet = ObjGlobal.GetDataProcedure("SPmrekening", ObjGlobal.Param);

                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    kdRekBank.Text = myRow["kdRek"].ToString();
                    hdnAccount.Value = myRow["noRek"].ToString();
                    lblDescription.Text = myRow["Ket"].ToString();
                    txtRemarkDetil.Text = txtRemark.Text;
                }
            }
        }

        protected void cboTampKeluar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTampKeluar.Text != "0")
            {
                SetInitialRowTamp(cboTampKeluar.Text, Convert.ToDecimal(txtValue.Text));
            }
            else
            {
                SetInitialRow();
                for (int i = 1; i < 3; i++)
                {
                    AddNewRow();
                }
            }
        }
    }
}