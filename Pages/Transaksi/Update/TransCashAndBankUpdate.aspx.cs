using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransCashAndBankUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadDataFirst();

            }
        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            ObjGlobal.Param.Add("search", Search.Text);
            ObjGlobal.Param.Add("user", ObjSys.GetUserId);
            grdKas.DataSource = ObjGlobal.GetDataProcedure("SPViewKasBankUpdate", ObjGlobal.Param);
            grdKas.DataBind();
        }

        protected void loadData()
        {
            


            if (cboFromToSupp.Text == "51" || cboFromToSupp.Text == "52" || cboFromToSupp.Text == "53")
            {

                ObjGlobal.Param.Clear();
                //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);

                ObjGlobal.Param.Add("MasukKeluar", cboFromToSupp.Text);


                ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("kategoriusaha", ObjSys.GetKategori_Usaha);
                grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank2", ObjGlobal.Param);
                grdBank.DataBind();

            }
            else
            {
                if (cbokolekte.Text == "0")
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

                }
                else
                {
                    ObjGlobal.Param.Clear();
                    //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
                    ObjGlobal.Param.Add("jenis", cbokolekte.Text);
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
                    grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningKasBank1", ObjGlobal.Param);
                    grdBank.DataBind();
                }
            }


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
            ObjGlobal.Param.Add("Search", txtSearchTHR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("tglTran", dtKas.Text);
            grdTHR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTHR", ObjGlobal.Param);
            grdTHR.DataBind();

            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            //ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPengembalian", ObjGlobal.Param);
            grdPengembalian.DataBind();
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
            ObjGlobal.Param.Add("Search", txtSearchTHR.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("tglTran", dtKas.Text);
            grdTHR.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTHR", ObjGlobal.Param);
            grdTHR.DataBind();

            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("Search", txtSearchPO.Text);
            //ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdPengembalian.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPengembalian", ObjGlobal.Param);
            grdPengembalian.DataBind();
        }
        protected void loadCombo()
        {
            cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang= '" + ObjSys.GetCabangId + "') a");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
            cbokolekte.DataSource = ObjDb.GetRows("select '0' id,' ---Pilih Jenis Kolekte--- ' name union all SELECT distinct nokolekte id, jeniskolekte name FROM mstjeniskolekte_h ");
            cbokolekte.DataValueField = "id";
            cbokolekte.DataTextField = "name";
            cbokolekte.DataBind();
        }
        protected void formKolekte_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadData();
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
            dt.Columns.Add(new DataColumn("txtRemarkDetil", typeof(string)));
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
                dr["hdnAccount"] = myRow["noRek"].ToString();
                dr["lblDescription"] = myRow["Ket"].ToString();
                dr["txtRemarkDetil"] = myRow["Uraian"].ToString();
                dr["txtDebit"] = ObjSys.IsFormatNumber(myRow["Debetasing"].ToString());
                dr["txtKredit"] = ObjSys.IsFormatNumber(myRow["Kreditasing"].ToString());

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
                        TextBox txtRemarkDetil = (TextBox)grdKasBank.Rows[i].FindControl("txtRemarkDetil");
                        Label lblDescription = (Label)grdKasBank.Rows[i].FindControl("lblDescription");
                        TextBox txtDebit = (TextBox)grdKasBank.Rows[i].FindControl("txtDebit");
                        TextBox txtKredit = (TextBox)grdKasBank.Rows[i].FindControl("txtKredit");

                        txtAccount.Text = dt.Rows[i]["txtAccount"].ToString();
                        hdnAccount.Value = dt.Rows[i]["hdnAccount"].ToString();
                        lblDescription.Text = dt.Rows[i]["lblDescription"].ToString();
                        txtRemarkDetil.Text = dt.Rows[i]["txtRemarkDetil"].ToString();
                        txtDebit.Text = dt.Rows[i]["txtDebit"].ToString();
                        txtKredit.Text = dt.Rows[i]["txtKredit"].ToString();
                    }
                }
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void grdKas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKas.PageIndex = e.NewPageIndex;
            loadDataFirst();
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

            cboAccount.DataSource = ObjDb.GetRows("select a.* from (select '0' id,' ---Pilih Akun--- ' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
            cboAccount.DataValueField = "id";
            cboAccount.DataTextField = "name";
            cboAccount.DataBind();

            cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang where stsMataUang = '1' ) a");
            cboCurrency.DataValueField = "id";
            cboCurrency.DataTextField = "name";
            cboCurrency.DataBind();
        }
        protected void LoadAwal()
        {

            if (cboTransaction.Text == "1")
            {
                txtTotalDebit.Text = ObjSys.IsFormatNumber(txtValue.Text);
                txtTotalKredit.Text = "0.00";
                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                if (cboFromToCus.Text == "1")
                    showHideForm(false, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

                else if (cboFromToCus.Text == "9")
                    showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);

                else if (cboFromToCus.Text == "10")
                    showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false);
                else if (cboFromToCus.Text == "50")
                    showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,true);

                else if (cboFromToCus.Text == "17")
                    showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);
                else if (cboFromToCus.Text == "23")
                    showHideForm(true,false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            }
            else if (cboTransaction.Text == "2")
            {
                txtTotalDebit.Text = "0.00";
                txtTotalKredit.Text = ObjSys.IsFormatNumber(txtValue.Text);
                showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                
                if (cboFromToSupp.Text == "1")
                    showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "2")
                    showHideForm(false,true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "3")
                    showHideForm(false,false, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                
                else if (cboFromToSupp.Text == "4")
                    showHideForm(false,false, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    
                else if (cboFromToSupp.Text == "5")
                    showHideForm(false,false, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false);
                    
                else if (cboFromToSupp.Text == "6")
                    showHideForm(false,false, false, true, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "7")
                    showHideForm(false,false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false);
                    
                else if (cboFromToSupp.Text == "8")
                    showHideForm(false,false, false, true, false, false, false, false, true, false, false, true, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "10")
                    showHideForm(false,false, false, true, false, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "11")
                    showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "12")
                    showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false);

                else if (cboFromToSupp.Text == "13")
                    if (ObjSys.GetstsPusat == "4")
                        showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false);

                    else
                    {
                        showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                        btnSimpan.Enabled = true;
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Hanya untuk Admin Kantor Yayasan.");
                    }

                else if (cboFromToSupp.Text == "14")
                    showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

                else if (cboFromToSupp.Text == "15")
                    showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false);
                
                else if (cboFromToSupp.Text == "16")
                    showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false);

                else if (cboFromToSupp.Text == "17")
                    showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);

            }
        }
        
        protected void showHideForm(bool divpenerimaan, bool DivPO, bool DivCboCust, bool DivCboSupp, bool DivOther, bool DivPR, bool DivGaji, bool DivIuran, bool DivKasBon, bool DivTHR, bool DivAngsuran, bool DivAbsensi, bool DivPRKas, bool DivDanaBOS, bool DivTransfer, bool DivPengembalian, bool DivBPJS, bool DivTampKeluar, bool DivKegiatan,bool divkolekte)
        {
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
            formKolekte.Visible = divkolekte;
            formpenerimaanunit.Visible = divpenerimaan;
        }

        protected void loadDataUnitPenerimaan()
        {
            DataSet mySet = ObjDb.GetRows("select * from mcabang where noCabang = " + ObjSys.GetCabangId + "");

            DataRow myRow = mySet.Tables[0].Rows[0];
            string parent = myRow["parent"].ToString();
            string stsCabang = myRow["stsCabang"].ToString();
            string stsPusat = myRow["stsPusat"].ToString();


            if (stsCabang == "0" || stsCabang == "4")
            {
                cbounitpenerimaan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang where stscabang in(2,3,4)) a order by a.noUrut");
                //cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + ObjSys.GetCabangId + "' or nocabang = '" + ObjSys.GetCabangId + "'  ) a order by a.noUrut");
                cbounitpenerimaan.DataValueField = "id";
                cbounitpenerimaan.DataTextField = "name";
                cbounitpenerimaan.DataBind();
            }

            else if (stsCabang == "1" || stsCabang == "3")
            {
                cbounitpenerimaan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + parent + "' or nocabang = '" + ObjSys.GetCabangId + "'  ) a order by a.noUrut");
                cbounitpenerimaan.DataValueField = "id";
                cbounitpenerimaan.DataTextField = "name";
                cbounitpenerimaan.DataBind();
            }
            else if (stsCabang == "1" || stsCabang == "2")
            {
                cbounitpenerimaan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "' union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + ObjSys.GetParentCabang + "' and stscabang='3' ) a order by a.noUrut");
                //untuk ytk
                //cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "') a order by a.noUrut");
                cbounitpenerimaan.DataValueField = "id";
                cbounitpenerimaan.DataTextField = "name";
                cbounitpenerimaan.DataBind();
            }


        }
        protected void loadDataUnitTransfer()
        {
            if (ObjSys.Getjnstkas == "0")
            {
                cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE nocabang = '" + ObjSys.GetCabangId + "') a order by a.noUrut");
                cboUnitTransfer.DataValueField = "id";
                cboUnitTransfer.DataTextField = "name";
                cboUnitTransfer.DataBind();
            }

            else if (ObjSys.Getjnstkas == "1")
            {
                if (ObjSys.GetstsPusat == "3" || ObjSys.GetstsPusat == "2")
                {
                    cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE parent = '" + ObjSys.GetParentCabang + "' or nocabang = '" + ObjSys.GetCabangId + "') a order by a.noUrut");
                    cboUnitTransfer.DataValueField = "id";
                    cboUnitTransfer.DataTextField = "name";
                    cboUnitTransfer.DataBind();
                }

                else if (ObjSys.GetstsPusat == "4")
                {
                    cboUnitTransfer.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stscabang in(2,4)) a order by a.noUrut");
                    cboUnitTransfer.DataValueField = "id";
                    cboUnitTransfer.DataTextField = "name";
                    cboUnitTransfer.DataBind();
                }
            }
        }


        protected void loadDataUnitBPJS()
        {
            cboUnitBPJS.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and nocabang = '" + ObjSys.GetCabangId + "') a order by a.noUrut");
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
            cboKegiatan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Kegiatan-' name union all SELECT distinct noMKegiatan id, namaKegiatan name FROM mJenisKegiatan) a");
            cboKegiatan.DataValueField = "id";
            cboKegiatan.DataTextField = "name";
            cboKegiatan.DataBind();
        }

        protected void grdKas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();
                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noKas = grdKas.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noKas;

                    DataSet mySet = ObjDb.GetRows("select case when a.Type = 'Kas/Bank Keluar' then '2' else '1' end as tipe, " +
                        "a.nomorKode,a.noRek,a.Type,case when a.noMataUang=0 then '20' else a.noMatauang end as noMataUang, " +
                        "a.kursKas,a.Uraian,a.Nilai,a.Tgl,a.dari,a.noCus,a.Cust,a.noCabangTransfer, a.nomtampungan, a.noKegiatan,a.nojnskolekte from tKas a where a.noKas = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    loadDataUnitPenerimaan();
                    txtKode.Text = myRow["nomorKode"].ToString();
                    cboTransaction.Text = myRow["tipe"].ToString();
                    cboAccount.Text = myRow["noRek"].ToString();
                    cboCurrency.Text = myRow["noMataUang"].ToString();
                    txtRemark.Text = myRow["Uraian"].ToString();
                    txtValue.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                    hdnValue.Value = myRow["Nilai"].ToString();
                    txtKurs.Text = ObjSys.IsFormatNumber(myRow["kursKas"].ToString());
                    dtKas.Text = Convert.ToDateTime(myRow["Tgl"]).ToString("dd-MMM-yyyy");

                    loadDataCombo();

                    if (cboTransaction.Text == "1")
                        cboFromToCus.Text = myRow["dari"].ToString();
                    else
                        cboFromToSupp.Text = myRow["dari"].ToString();

                    if (myRow["noMataUang"].ToString() == "20")
                        showhidekurs.Visible = false;
                    else
                        showhidekurs.Visible = true;


                    SetInitialRow(hdnId.Value);

                    LoadAwal();

                    if (myRow["Type"].ToString() == "Kas/Bank Keluar")
                        txtTotalKredit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());
                    else
                        txtTotalDebit.Text = ObjSys.IsFormatNumber(myRow["Nilai"].ToString());

                    if (cboFromToCus.Text == "1")
                    {
                        txtOther.Text = myRow["Cust"].ToString();
                    }
                    if (cboFromToCus.Text == "23")
                    {
                        cbounitpenerimaan.Text = myRow["nocabangTransfer"].ToString();
                    }
                    else if (cboFromToSupp.Text == "1")
                    {
                        txtOther.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "2")
                    {
                        hdnnoPO.Value = myRow["noCus"].ToString();
                        txtPO.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "3")
                    {
                        hdnnoPR.Value = myRow["noCus"].ToString();
                        txtPR.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "4")
                    {
                        hdnnoGaji.Value = myRow["noCus"].ToString();
                        txtKodeGaji.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "5")
                    {
                        hdnnoIuran.Value = myRow["noCus"].ToString();
                        txtKodeIuran.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "6")
                    {
                        hdnnoKasBon.Value = myRow["noCus"].ToString();
                        txtKodeKasbon.Text = myRow["Cust"].ToString();

                        DataSet mySet1 = ObjDb.GetRows("select * from tKasBon where noKas = '" + hdnId.Value + "'");
                        if (mySet1.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRow1 = mySet1.Tables[0].Rows[0];
                            txtAngsuran.Text = ObjSys.IsFormatNumber(myRow1["angsuran"].ToString());
                        }

                    }
                    else if (cboFromToSupp.Text == "7")
                    {
                        hdnnoTHR.Value = myRow["noCus"].ToString();
                        txtKodeTHR.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "8")
                    {
                        hdnnoAbsensi.Value = myRow["noCus"].ToString();
                        txtKodeAbsensi.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "9")
                    {
                        hdnnoPRKas.Value = myRow["noCus"].ToString();
                        txtKodePRKas.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToCus.Text == "10" || cboFromToSupp.Text == "10")
                    {
                        txtDanaBOS.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "11")
                    {
                        txtOther.Text = myRow["Cust"].ToString();
                    }
                    else if (cboFromToSupp.Text == "12")
                    {
                        cboUnitTransfer.Text = myRow["noCabangTransfer"].ToString();
                    }
                    else if (cboFromToSupp.Text == "16")
                    {
                        cboTampKeluar.Text = myRow["nomtampungan"].ToString();
                    }
                    else if (cboFromToCus.Text == "17" || cboFromToSupp.Text == "17")
                    {
                        cboKegiatan.Text = myRow["noKegiatan"].ToString();
                    }
                    else if (cboFromToCus.Text == "50" )
                    {
                        cbokolekte.Text = myRow["nojnskolekte"].ToString();
                    }
                    if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "15")
                    {
                        for (int i = 0; i < grdKasBank.Rows.Count; i++)
                        {
                            TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                            kdRekBank.Enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < grdKasBank.Rows.Count; i++)
                        {
                            TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                            kdRekBank.Enabled = true;
                        }
                    }

                    loadDataUnitTransfer();

                    loadDataUnitBPJS();

                    loadDataTampKeluar();

                    loadDataKegiatan();

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
                    showHideFormKas(false, true);
                }

                else if (e.CommandName == "SelectDelete")
                {
                    
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noKas = grdKas.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noKas;

                    DataSet mySet = ObjDb.GetRows("select dari, nilai, noCus from tkas where noKas = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    string dari = myRow["dari"].ToString();
                    string noCus = myRow["noCus"].ToString();
                    string nilai = myRow["nilai"].ToString();

                    if (dari == "2")
                    {
                        string sqlpo = "update tHutang set nsaldoHutang = nsaldoHutang + '" + Convert.ToDecimal(nilai) + "', nSaldoRpHutang = nSaldoRpHutang + '" + Convert.ToDecimal(nilai) + "' where noHutang = '" + noCus + "'";
                        ObjDb.ExecQuery(sqlpo);
                    }

                    if (dari == "3")
                    {
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noKas", hdnId.Value);
                        ObjDb.Delete("Transkasbondana", ObjDb.Where);
                    }

                    if (dari == "4")
                    {
                        string sqlgaji = "update TtotaGajibln set sts = '0' where nototgajibln = '" + noCus + "'";
                        ObjDb.ExecQuery(sqlgaji);
                    }

                    if (dari == "5")
                    {
                        string sqliuran = "update ttotiuranbln set sts = '0' where noTotIuran = '" + noCus + "'";
                        ObjDb.ExecQuery(sqliuran);
                    }

                    if (dari == "6")
                    {
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noKas", hdnId.Value);
                        ObjDb.Delete("tKasBon", ObjDb.Where);
                    }

                    if (dari == "7")
                    {
                        string sqlthr = "update Tthrkaryawan set sts = '0' where nothr = '" + noCus + "'";
                        ObjDb.ExecQuery(sqlthr);
                    }

                    if (cboFromToSupp.Text == "8")
                    {
                        string sqlabsensi = "update TtotAbsenbln set sts = '0' where noTotAbsen = '" + noCus + "'";
                        ObjDb.ExecQuery(sqlabsensi);
                    }

                    if (cboFromToCus.Text == "9")
                    {
                        string sqlPRKas = "update tkas set stsPRKas = '0' where noKas = '" + noCus + "'";
                        ObjDb.ExecQuery(sqlPRKas);
                    }

                    if (dari == "16")
                    {
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noKas", hdnId.Value);
                        ObjDb.Delete("Ttampunganpengeluaran", ObjDb.Where);
                    }

                    //ObjDb.Where.Clear();
                    //ObjDb.Where.Add("noKas", hdnId.Value);
                    //ObjDb.Delete("tKasdetil", ObjDb.Where);
                    //ObjDb.Delete("tKas", ObjDb.Where);

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noKas", hdnId.Value);
                    ObjGlobal.GetDataProcedure("SPDeletetkaslawan", ObjGlobal.Param);

                    showHideFormKas(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    loadDataFirst();

                }
                
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
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
            CloseMessage();
            showHideFormKas(true, false);
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
        
        protected void ClearDataSelected()
        {
            txtOther.Text = "";
            txtPO.Text = "";
            txtPR.Text = "";
            txtKodeGaji.Text = "";
            txtKodeIuran.Text = "";
            txtKodeKasbon.Text = "";
            txtKodeTHR.Text = "";
            hdnnoPO.Value = "";
            hdnnoPR.Value = "";
            hdnnoGaji.Value = "";
            hdnnoIuran.Value = "";
            hdnnoKasBon.Value = "";
            hdnnoTHR.Value = "";
            txtTotalDebit.Text = "0.00";
            txtTotalKredit.Text = "0.00";
        }

        protected void cboFromToCus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            ClearDataSelected();

            if (cboFromToCus.Text == "1")

               showHideForm(false,false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToCus.Text == "9")

               showHideForm(false,false, true, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
            else if (cboFromToCus.Text == "50")

               showHideForm(false,false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true);


            else if (cboFromToCus.Text == "10")

               showHideForm(false,false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false);

            else if (cboFromToCus.Text == "17")

                showHideForm(false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);
            else if (cboFromToCus.Text == "23")

                showHideForm(true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            txtValue.Text = "0.00";
            txtTotalDebit.Text = "0.00";
        }

        protected void cboFromToSupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            ClearDataSelected();

            if (cboFromToSupp.Text == "1")
               showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "2")
               showHideForm(false,true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                
            else if (cboFromToSupp.Text == "3")
               showHideForm(false,false, false, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
            
            else if (cboFromToSupp.Text == "4")
               showHideForm(false,false, false, true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false);
                
            else if (cboFromToSupp.Text == "5")
               showHideForm(false,false, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false);
                
            else if (cboFromToSupp.Text == "6")
               showHideForm(false,false, false, true, false, false, false, false, true, false, true, false, false, false, false, false, false, false, false, false);
                
            else if (cboFromToSupp.Text == "7")
               showHideForm(false,false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false);
                
            else if (cboFromToSupp.Text == "8")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "10")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "11")
               showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "12")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false);

            else if (cboFromToSupp.Text == "13")
                if (ObjSys.GetstsPusat == "4")
                   showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false);

                else
                {
                   showHideForm(false,false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
                    btnSimpan.Enabled = true;
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Hanya untuk Admin Kantor Yayasan.");
                }

            else if (cboFromToSupp.Text == "14")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "15")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);

            else if (cboFromToSupp.Text == "16")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false);

            else if (cboFromToSupp.Text == "17")
               showHideForm(false,false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false);


            if (cboFromToSupp.Text == "14" || cboFromToSupp.Text == "15")
            {
                for (int i = 0; i < grdKasBank.Rows.Count; i++)
                {
                    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                    kdRekBank.Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < grdKasBank.Rows.Count; i++)
                {
                    TextBox kdRekBank = (TextBox)grdKasBank.Rows[i].Cells[1].FindControl("txtAccount");
                    kdRekBank.Enabled = true;
                }
            }

            SetInitialRow();
            for (int i = 1; i < 5; i++)
            {
                AddNewRow();
            }

            txtValue.Text = "0.00";
            txtTotalDebit.Text = "0.00";
        }

        protected void btnPO_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPO.Show();
        }

        protected void btnPR_Click(object sender, ImageClickEventArgs e)
        {
            loadData();
            dlgPR.Show();
        }


        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
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

            DataSet protekpos = ObjDb.GetRows("SELECT protekpos as protekpos FROM mcabang WHERE nocabang = '" + ObjSys.GetCabangId + "' and protekpos=1");
            if (protekpos.Tables[0].Rows.Count > 0)
            {
                DataSet dataSaldobln1 = ObjDb.GetRows("select distinct month(tgl) as bln from tsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtKas.Text).Year + "' and month(tgl)='" + Convert.ToDateTime(dtKas.Text).Month + "'");
                if (dataSaldobln1.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK = dataSaldobln1.Tables[0].Rows[0];
                    int blnDb = int.Parse(myRowSK["bln"].ToString());

                    if (Convert.ToDateTime(dtKas.Text).Month != blnDb)
                    {
                        message += ObjSys.CreateMessage("Sudah Posting Bulanan GL");
                        valid = false;
                    }

                }

                DataSet dataSaldobln = ObjDb.GetRows("select distinct year(tgl) as thn from btsaldobln where noCabang = '" + ObjSys.GetCabangId + "' and sts=0 and year(tgl)='" + Convert.ToDateTime(dtKas.Text).Year + "'");
                if (dataSaldobln.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSK = dataSaldobln.Tables[0].Rows[0];
                    int thnDb = int.Parse(myRowSK["thn"].ToString());

                    if (Convert.ToDateTime(dtKas.Text).Year == thnDb)
                    {
                        message += ObjSys.CreateMessage("Sudah Posting Tahunan GL");
                        valid = false;
                    }

                }
            }

            if (dtKas.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Kas tidak boleh kosong.");
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
            else if (txtOther.Text == "" && cboFromToSupp.Text == "1")
            {
                message += ObjSys.CreateMessage("Catatan Lain-lain harus diisi.");
                valid = false;
            }
            else if (txtDanaBOS.Text == "" && cboFromToCus.Text == "10")
            {
                message += ObjSys.CreateMessage("Catatan Dana BOS harus diisi.");
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
            if (valid == true)
            {
                try
                {
                    bool validate = true;

                    for (int x = 0; x < grdKasBank.Rows.Count; x++)
                    {


                        HiddenField hdnAccount2 = (HiddenField)grdKasBank.Rows[x].FindControl("hdnAccount");
                        TextBox txtAccount2 = (TextBox)grdKasBank.Rows[x].FindControl("txtAccount");
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
                                message += ObjSys.CreateMessage("Rekening Harus yang Jenisnya Transfer Bank.");
                                validate = false;
                            }else
                            {
                                validate = true;

                            }
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

                            string kdrekkas = "-";
                            DataSet dataSN = ObjDb.GetRows("SELECT kdrek FROM mRekening WHERE norek = '" + cboAccount.Text + "'");
                            if (dataSN.Tables[0].Rows.Count > 0)
                            {
                                DataRow myRowSn = dataSN.Tables[0].Rows[0];
                                kdrekkas = myRowSn["kdrek"].ToString();
                            }

                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noKas", hdnId.Value);
                            string jenistran = "-";
                            decimal totdebetkas = 0;
                            decimal totkreditkas = 0;
                            decimal totdebetkasrp = 0;
                            decimal totkreditkasrp = 0;
                            if (cboTransaction.Text == "1")
                            {
                                jenistran = "Kas/Bank Masuk";
                                totdebetkas = Convert.ToDecimal(txtValue.Text);
                                totkreditkas = 0;
                                totdebetkasrp = hasilakhir;
                                totkreditkasrp = 0;
                            }

                            if (cboTransaction.Text == "2")
                            {
                                jenistran = "Kas/Bank Keluar";
                                totdebetkas = 0;
                                totkreditkas = Convert.ToDecimal(txtValue.Text);
                                totdebetkasrp = 0;
                                totkreditkasrp = hasilakhir;
                            }

                            ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Data.Add("noRek", cboAccount.Text);
                            ObjDb.Data.Add("kdrek", kdrekkas);
                            if (cboFromToCus.Text == "1" || cboFromToSupp.Text == "1")
                            {
                                ObjDb.Data.Add("noCus", "0");
                                ObjDb.Data.Add("Cust", txtOther.Text);
                            }
                            if (cboFromToSupp.Text == "2")
                            {
                                ObjDb.Data.Add("noCus", hdnnoPO.Value);
                                ObjDb.Data.Add("Cust", txtPO.Text);
                            }
                            if (cboFromToSupp.Text == "3")
                            {
                                ObjDb.Data.Add("noCus", hdnnoPR.Value);
                                ObjDb.Data.Add("Cust", txtPR.Text);
                            }
                            if (cboFromToSupp.Text == "4")
                            {
                                ObjDb.Data.Add("noCus", hdnnoGaji.Value);
                                ObjDb.Data.Add("Cust", txtKodeGaji.Text);
                            }
                            if (cboFromToSupp.Text == "5")
                            {
                                ObjDb.Data.Add("noCus", hdnnoIuran.Value);
                                ObjDb.Data.Add("Cust", txtKodeIuran.Text);
                            }
                            if (cboFromToSupp.Text == "6")
                            {
                                ObjDb.Data.Add("noCus", hdnnoKasBon.Value);
                                ObjDb.Data.Add("Cust", txtKodeKasbon.Text);
                            }
                            if (cboFromToSupp.Text == "7")
                            {
                                ObjDb.Data.Add("noCus", hdnnoTHR.Value);
                                ObjDb.Data.Add("Cust", txtKodeTHR.Text);
                            }
                            if (cboFromToSupp.Text == "8")
                            {
                                ObjDb.Data.Add("noCus", hdnnoAbsensi.Value);
                                ObjDb.Data.Add("Cust", txtKodeAbsensi.Text);
                            }
                            if (cboFromToCus.Text == "9")
                            {
                                ObjDb.Data.Add("noCus", hdnnoPRKas.Value);
                                ObjDb.Data.Add("Cust", txtKodePRKas.Text);
                            }
                            if (cboFromToCus.Text == "10" || cboFromToSupp.Text == "10")
                            {
                                ObjDb.Data.Add("noCus", "0");
                                ObjDb.Data.Add("Cust", txtDanaBOS.Text);
                            }
                            if (cboFromToSupp.Text == "12")
                            {
                                ObjDb.Data.Add("noCus", "12");
                                ObjDb.Data.Add("Cust", "Transfer Dana");
                                ObjDb.Data.Add("noCabangTransfer", cboUnitTransfer.Text);
                            }
                            if (cboFromToSupp.Text == "11")
                            {
                                ObjDb.Data.Add("noCus", "0");
                                ObjDb.Data.Add("Cust", txtOther.Text);
                            }
                            if (cboFromToCus.Text == "17" || cboFromToSupp.Text == "17")
                            {
                                ObjDb.Data.Add("noCus", "0");
                                ObjDb.Data.Add("Cust", "Kegiatan");
                                ObjDb.Data.Add("noKegiatan", cboKegiatan.Text);
                            }
                            if (cboFromToCus.Text == "50")
                            {
                                ObjDb.Data.Add("noCus", "0");
                                ObjDb.Data.Add("Cust", "Kolekte");
                                ObjDb.Data.Add("nojnskolekte", cbokolekte.Text);
                            }
                            if (cboTransaction.Text == "1")
                            {
                                ObjDb.Data.Add("dari", cboFromToCus.Text);
                            }
                            else
                            {
                                ObjDb.Data.Add("dari", cboFromToSupp.Text);
                            }
                            if (cboFromToCus.Text == "23")
                            {
     
                                ObjDb.Data.Add("noCabangTransfer", cbounitpenerimaan.Text);
                            }
                            ObjDb.Data.Add("Uraian", txtRemark.Text);
                            ObjDb.Data.Add("noMataUang", cboCurrency.Text);
                            ObjDb.Data.Add("kursKas", Convert.ToDecimal(kurs).ToString());
                            ObjDb.Data.Add("Nilai", Convert.ToDecimal(txtValue.Text).ToString());
                            ObjDb.Data.Add("nilaiRp", Convert.ToDecimal(hasilakhir).ToString());
                            ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                            ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                            ObjDb.Data.Add("stsapv", "1");
                            ObjDb.Update("tKas", ObjDb.Data, ObjDb.Where);

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("kdtran", txtKode.Text);
                            ObjDb.Delete("tKasdetil", ObjDb.Where);

                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noKas", hdnId.Value);
                            ObjDb.Data.Add("kdTran", txtKode.Text);
                            ObjDb.Data.Add("jenisTran", jenistran);
                            ObjDb.Data.Add("noTran", hdnId.Value);
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
                                    ObjDb.Data.Add("noKas", hdnId.Value);
                                    ObjDb.Data.Add("kdTran", txtKode.Text);
                                    ObjDb.Data.Add("jenisTran", jenistran);
                                    ObjDb.Data.Add("noTran", hdnId.Value);
                                    ObjDb.Data.Add("noRek", hdnAccount.Value);
                                    ObjDb.Data.Add("kdRek", txtAccount.Text);
                                    ObjDb.Data.Add("Uraian", txtRemarkDetil.Text);
                                    ObjDb.Data.Add("Debet", Convert.ToDecimal(nilairpdebet).ToString());
                                    ObjDb.Data.Add("Kredit", Convert.ToDecimal(nilairpkredit).ToString());
                                    ObjDb.Data.Add("kurs", Convert.ToDecimal(kurs).ToString());
                                    ObjDb.Data.Add("nomatauang", cboCurrency.Text);
                                    ObjDb.Data.Add("debetasing", Convert.ToDecimal(Debit).ToString());
                                    ObjDb.Data.Add("kreditasing", Convert.ToDecimal(Kredit).ToString());
                                    ObjDb.Data.Add("sts", "0");
                                    ObjDb.Data.Add("Tgl", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                                    ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                                    ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                    ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                    ObjDb.Insert("tKasdetil", ObjDb.Data);
                                }
                            }

                            if (cboFromToSupp.Text == "2")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noKas", hdnId.Value);
                                ObjGlobal.Param.Add("noPO", hdnnoPO.Value);
                                ObjGlobal.GetDataProcedure("SPupdatejurnalPO", ObjGlobal.Param);

                                string sqlpo = "update tHutang set nsaldoHutang = (nsaldoHutang + '" + Convert.ToDecimal(hdnValue.Value) + "') - '" + Convert.ToDecimal(txtValue.Text) + "', nSaldoRpHutang = (nSaldoRpHutang + '" + Convert.ToDecimal(hdnValue.Value) + "' ) - '" + Convert.ToDecimal(txtValue.Text) + "' where noHutang = '" + hdnnoPO.Value + "'";
                                ObjDb.ExecQuery(sqlpo);
                            }

                            if (cboFromToSupp.Text == "3")
                            {
                                DataSet myPinta = ObjDb.GetRows("select a.peminta, b.noKaryawan from TransPR_H a inner join MstKaryawan b on a.peminta = b.nama where a.noPR = '" + hdnnoPR.Value + "'");
                                DataRow myRowPinta = myPinta.Tables[0].Rows[0];
                                string IdPinta = myRowPinta["noKaryawan"].ToString();

                                string sqlPRDana = "update Transkasbondana set tgl = '" + Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd") + "', peminta = '" + IdPinta + "', nilai = '" + Convert.ToDecimal(txtValue.Text).ToString() + "', uraian = '" + txtRemark.Text + "' Where noKas = '" + hdnId.Value + "' and sts='1'";
                                ObjDb.ExecQuery(sqlPRDana);
                            }

                            if (cboFromToSupp.Text == "4")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noKas", hdnId.Value);
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
                                string sqlkasbon = "update tKasBon set tgl = '" + Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd") + "',noKaryawan = '" + hdnnoKasBon.Value + "',nilai = '" + Convert.ToDecimal(txtValue.Text) + "',angsuran = '" + Convert.ToDecimal(txtAngsuran.Text) + "' Where saldo = 0 and noKas = '" + hdnId.Value + "' ";
                                ObjDb.ExecQuery(sqlkasbon);
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
                            }

                            if (cboFromToSupp.Text == "12")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noKas", hdnId.Value);
                                ObjGlobal.GetDataProcedure("SPupdatejurnaltransfer2", ObjGlobal.Param);

                            }

                            if (cboFromToSupp.Text == "11")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noKas", hdnId.Value);
                                ObjGlobal.GetDataProcedure("SPupdatejurnaltransfer1", ObjGlobal.Param);

                            }

                            if (cboFromToCus.Text == "15")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add("noKas", hdnId.Value);
                                ObjGlobal.GetDataProcedure("SPupdatejenistran", ObjGlobal.Param);
                            }

                            showHideFormKas(true, false);

                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("success", "Data berhasil diubah.");
                            loadDataFirst();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", "Total debit dan kredit tidak sama.");
                        }
                    }else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
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

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtAccount"] = txtAccount.Text;
                        dtCurrentTable.Rows[i]["hdnAccount"] = hdnAccount.Value;
                        dtCurrentTable.Rows[i]["lblDescription"] = lblDescription.Text;
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

        protected void btnSearchPO_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
            dlgPO.Show();
        }

        protected void btnCariReimbersment_Click(object sender, EventArgs e)
        {
            loadDataReimbersment();
            dlgReimbersment.Show();
        }

        protected void loadDataReimbersment()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtKodeReimbersment.Text);
            grdReimbersment.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataReimbersment", ObjGlobal.Param);
            grdReimbersment.DataBind();
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

                hdnnoPR.Value = Id;
                txtPR.Text = kodePR;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("noPR", Id);
                DataSet mySetH = ObjGlobal.GetDataProcedure("SPLoadKasBankPR", ObjGlobal.Param);
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
                    TextBox txtDebit = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtDebit");

                    hdnAccount.Value = myRowH["noRek"].ToString();
                    txtAccount.Text = myRowH["kdRek"].ToString();
                    lblDescription.Text = myRowH["Ket"].ToString();
                    txtDebit.Text = ObjSys.IsFormatNumber(myRowH["hargasatuan"].ToString());

                    txtDebit.Enabled = false;
                    totaldebit += Convert.ToDecimal(myRowH["hargasatuan"].ToString());
                }
                DataSet mySet = ObjDb.GetRows("select a.noPR, c.noRek, c.kdRek, c.Ket, isnull(a.budget,0) as hargasatuan " +
                    "from TransPR_D a join mRekening " +
                    "c on c.noRek = a.norek where a.noPR = '" + Id + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];
                txtValue.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(myRow["hargasatuan"]).ToString());

                txtTotalDebit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(totaldebit).ToString());
                txtTotalKredit.Text = ObjSys.IsFormatNumber(Convert.ToDecimal(myRow["hargasatuan"]).ToString());

                loadData();
                dlgPR.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }

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

                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;
                   // txtRemarkGrid.Text = txtRemark.Text;

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

        protected void grdKasBon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdKasBon.PageIndex = e.NewPageIndex;
            loadData();
            dlgKasBon.Show();
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

        protected void btnSearchAkun_Click(object sender, EventArgs e)
        {
            loadData();
            dlgBank.Show();
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
                int i = 0;
                decimal totaldebit = 0;
                foreach (DataRow myRowH in mySetH.Tables[0].Rows)
                {
                    i++;
                    HiddenField hdnAccount = (HiddenField)grdKasBank.Rows[i - 1].FindControl("hdnAccount");
                    TextBox txtAccount = (TextBox)grdKasBank.Rows[i - 1].FindControl("txtAccount");
                    Label lblDescription = (Label)grdKasBank.Rows[i - 1].FindControl("lblDescription");
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
                dlgPRKas.Hide();
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
                    ///txtRemarkDetil.Text = txtRemark.Text;
                }
            }
        }
    }
}