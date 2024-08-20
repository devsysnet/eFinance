using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.Sql;
using System.Data.SqlClient;
using InfoSoftGlobal;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace eFinance.Pages
{
    public partial class HomeKlinik1 : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet mySet = ObjDb.GetRows("select distinct a.*, b.namaTask, b.linkFile, b.queryAlert, b.param1, b.param2, b.param3 from MstTaskAlert_D a left join MstTaskAlert b on a.noTaskAlert = b.noTaskAlert where a.nouser='" + ObjSys.GetUserId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                {
                    string alert = "";
                    foreach (DataRow myRow in mySet.Tables[0].Rows)
                    {
                        int countAlert = 0;
                        if (myRow["queryAlert"].ToString() != "")
                        {
                            string param1 = "", param2 = "";
                            if (myRow["param1"].ToString().ToLower() == "nocabang" && myRow["param2"].ToString().ToLower() == "nouser")
                            {
                                param1 = ObjSys.GetCabangId;
                                param2 = ObjSys.GetUserId;
                            }
                            else if (myRow["param1"].ToString().ToLower() == "nocabang")
                                param1 = ObjSys.GetCabangId;
                            else if (myRow["param2"].ToString().ToLower() == "nouser")
                                param2 = ObjSys.GetUserId;

                            if (myRow["param1"].ToString() != "" && myRow["param2"].ToString() != "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param1"].ToString(), param1);
                                ObjGlobal.Param.Add(myRow["param2"].ToString(), param2);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else if (myRow["param1"].ToString() != "" && myRow["param2"].ToString() == "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param1"].ToString(), param1);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else if (myRow["param1"].ToString() == "" && myRow["param2"].ToString() != "")
                            {
                                ObjGlobal.Param.Clear();
                                ObjGlobal.Param.Add(myRow["param2"].ToString(), param2);
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString(), ObjGlobal.Param).Tables[0].Rows.Count;
                            }
                            else
                            {
                                countAlert = ObjGlobal.GetDataProcedure(myRow["queryAlert"].ToString()).Tables[0].Rows.Count;
                            }

                        }
                        if (countAlert != 0)
                            alert += "<a href='" + Func.BaseUrl + myRow["linkFile"] + "'><li class='list-group-item no-border-hr padding-xs-hr no-bg'>" + myRow["namaTask"] + " <span class='label label-danger pull-right'> " + countAlert + " </span></li></a>";
                        else
                            alert += "<a href='" + Func.BaseUrl + myRow["linkFile"] + "'><li class='list-group-item no-border-hr padding-xs-hr no-bg'>" + myRow["namaTask"] + " <span class='label label-primary pull-right'> " + countAlert + " </span></li></a>";
                    }

                    lblAlertList.Text = alert;

                }

                LoadYear();
                LoadDataCombo();
                LoadDataCombo2();
                loadDataCabang();
                LoadDataCombo4(cboUnit.Text);
            }

        }

        protected void LoadYear()
        {
            DataSet mySet = ObjDb.GetRows("select left(tahunAjaran,4) as tahun from Parameter");
            if (mySet.Tables[0].Rows.Count > 0)
            {
                DataRow myRow = mySet.Tables[0].Rows[0];
                //lbltahun.Text = myRow["tahun"].ToString();
                //lbltahun2.Text = myRow["tahun"].ToString();
                //lbltahun3.Text = myRow["tahun"].ToString();
            }
        }

        // Tab Kesiswaan

        protected void showHideCombo(bool DivPerwakilan, bool DivUnit)
        {
            comboPerwakilan.Visible = DivPerwakilan;
            comboUnit.Visible = DivUnit;
        }
        protected void LoadDataCombo()
        {
            //cboYayasan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=0 and stspusat=1 and stscabang=0) a");
            //cboYayasan.DataValueField = "id";
            //cboYayasan.DataTextField = "name";
            //cboYayasan.DataBind();

            cboYayasan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataYayasan");
            cboYayasan.DataValueField = "id";
            cboYayasan.DataTextField = "name";
            cboYayasan.DataBind();

            cboTahun.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTahun");
            cboTahun.DataValueField = "id";
            cboTahun.DataTextField = "name";
            cboTahun.DataBind();

            cboTahun.SelectedValue = (System.DateTime.Now.Year).ToString();

            LoadDataCombo2();
        }

        protected void LoadDataCombo2()
        {
            //if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboYayasan.Text + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboYayasan.Text + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}
            //else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            //{
            //    cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
            //    cboPerwakilan.DataValueField = "id";
            //    cboPerwakilan.DataTextField = "name";
            //    cboPerwakilan.DataBind();
            //}

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("yayasan", cboYayasan.Text);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboPerwakilan.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataWilayah", ObjGlobal.Param);
            cboPerwakilan.DataValueField = "id";
            cboPerwakilan.DataTextField = "name";
            cboPerwakilan.DataBind();

            LoadDataSiswa(cboPerwakilan.Text);
            LoadDataKaryawan(cboPerwakilan.Text);
            LoadDataGuru(cboPerwakilan.Text);
            loadDataAgama(cboPerwakilan.Text);
            loadDataJK(cboPerwakilan.Text);
            LoadDataCombo3();
        }

        protected void LoadDataCombo3()
        {
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                //cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and (stscabang=2 or stscabang=3)) a");
                //cboUnit.DataValueField = "id";
                //cboUnit.DataTextField = "name";
                //cboUnit.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();

                LoadDataSiswa(cboUnit.Text);
                LoadDataKaryawan(cboUnit.Text);
                LoadDataGuru(cboUnit.Text);
                loadDataAgama(cboUnit.Text);
                loadDataJK(cboUnit.Text);
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                //cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and (stscabang=2 or stscabang=3)) a");
                //cboUnit.DataValueField = "id";
                //cboUnit.DataTextField = "name";
                //cboUnit.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();

                LoadDataSiswa(cboPerwakilan.Text);
                LoadDataKaryawan(cboPerwakilan.Text);
                LoadDataGuru(cboPerwakilan.Text);
                loadDataAgama(cboPerwakilan.Text);
                loadDataJK(cboPerwakilan.Text);
            }
            else
            {
                //cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and (stscabang=2 or stscabang=3)) a");
                //cboUnit.DataValueField = "id";
                //cboUnit.DataTextField = "name";
                //cboUnit.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
                ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
                ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
                ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
                cboUnit.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnit", ObjGlobal.Param);
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();

                LoadDataSiswa(cboPerwakilan.Text);
                LoadDataKaryawan(cboPerwakilan.Text);
                LoadDataGuru(cboPerwakilan.Text);
                loadDataAgama(cboPerwakilan.Text);
                loadDataJK(cboPerwakilan.Text);
            }

        }

        protected void LoadDataCombo4(string cabang = "0")
        {
            cboAkunBC.DataSource = ObjDb.GetRows("select a.* from (SELECT 0 id, '--- Pilih Akun Unit ---' name union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + cabang + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
            cboAkunBC.DataValueField = "id";
            cboAkunBC.DataTextField = "name";
            cboAkunBC.DataBind();
        }
        protected void cboYayasan_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadDataCombo2();

            if (cboYayasan.Text == "0")
            {
                lblTotalPasien.Text = "0";
            }
            else
            {
                LoadDataSiswa(cboYayasan.Text);
                LoadDataKaryawan(cboYayasan.Text);
                LoadDataGuru(cboYayasan.Text);
                loadDataAgama(cboYayasan.Text);
                loadDataJK(cboYayasan.Text);
            }

        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo3();
            LoadDataCombo4(cboUnit.Text);

            if (cboPerwakilan.Text == "0")
            {
                LoadDataSiswa(cboYayasan.Text);
                LoadDataKaryawan(cboYayasan.Text);
                LoadDataGuru(cboYayasan.Text);
                loadDataAgama(cboYayasan.Text);
                loadDataJK(cboYayasan.Text);

                string cabang = cboYayasan.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;
                //loadChart(cabang, user, tahun);

                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }
            else
            {
                LoadDataSiswa(cboPerwakilan.Text);
                LoadDataKaryawan(cboPerwakilan.Text);
                LoadDataGuru(cboPerwakilan.Text);
                loadDataAgama(cboPerwakilan.Text);
                loadDataJK(cboPerwakilan.Text);

                string cabang = cboPerwakilan.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;
                //loadChart(cabang, user, tahun);
                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }

        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo4(cboUnit.Text);

            if (cboUnit.Text == "0")
            {
                LoadDataSiswa(cboPerwakilan.Text);
                LoadDataKaryawan(cboPerwakilan.Text);
                LoadDataGuru(cboPerwakilan.Text);
                loadDataAgama(cboPerwakilan.Text);
                loadDataJK(cboPerwakilan.Text);

                string cabang = cboPerwakilan.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;
                //loadChart(cabang, user, tahun);
                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }
            else
            {
                LoadDataSiswa(cboUnit.Text);
                LoadDataKaryawan(cboUnit.Text);
                LoadDataGuru(cboUnit.Text);
                loadDataAgama(cboUnit.Text);
                loadDataJK(cboUnit.Text);

                string cabang = cboUnit.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;
                //loadChart(cabang, user, tahun);
                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }
        }

        protected void LoadDataSiswa(string cabang = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }

            DataSet mySet = ObjGlobal.GetDataProcedure("SPCountSPasien", ObjGlobal.Param);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                lblTotalPasien.Text = Convert.ToInt32(mySet.Tables[0].Rows[0]["jml"]).ToString();
            }

        }


        // Tab Keuangan
        protected void LoadPendapatan(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataSet mySetOutstandingAr = ObjGlobal.GetDataProcedure("SPAlertPendapatan", ObjGlobal.Param);
            if (mySetOutstandingAr.Tables[0].Rows.Count > 0)
            {
                //lblOutstandingAr.Text = ObjSys.IsFormatNumber(mySetOutstandingAr.Tables[0].Rows[0]["Nilai"].ToString());
            }

        }

        protected void LoadBiaya(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataSet mySetTotBiaya = ObjGlobal.GetDataProcedure("SPAlerttotbiaya", ObjGlobal.Param);
            if (mySetTotBiaya.Tables[0].Rows.Count > 0)
            {
                lblTotBiaya.Text = ObjSys.IsFormatNumber(mySetTotBiaya.Tables[0].Rows[0]["Nilai"].ToString());
            }

        }

        protected void LoadPenerimaan(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataSet mySetTerima = ObjGlobal.GetDataProcedure("SPAlertPenerimaanklinik", ObjGlobal.Param);
            if (mySetTerima.Tables[0].Rows.Count > 0)
            {
                lblTotTerima.Text = ObjSys.IsFormatNumber(mySetTerima.Tables[0].Rows[0]["Nilai"].ToString());
            }

        }

        protected void LoadPenerimaanKas(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataSet mySetTerimaKas = ObjGlobal.GetDataProcedure("SPAlertPenerimaan_non_pendapatankas", ObjGlobal.Param);
            if (mySetTerimaKas.Tables[0].Rows.Count > 0)
            {
                lblTerimaKas.Text = ObjSys.IsFormatNumber(mySetTerimaKas.Tables[0].Rows[0]["Nilai"].ToString());
            }

        }

        protected void LoadPenerimaanBank(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataSet mySetTerimaBank = ObjGlobal.GetDataProcedure("SPAlertPenerimaan_non_pendapatanbank", ObjGlobal.Param);
            if (mySetTerimaBank.Tables[0].Rows.Count > 0)
            {
                lblTerimaBank.Text = ObjSys.IsFormatNumber(mySetTerimaBank.Tables[0].Rows[0]["Nilai"].ToString());
            }

        }
        protected void loadDataCabang()
        {
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                //loadChart(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPendapatan(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadBiaya(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaan(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaanKas(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaanBank(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadDataTracking(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);

                LoadChartData(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadLineChartData(cboUnit.Text, ObjSys.GetUserId, cboTahun.Text);
                loadDataBC(cboUnit.Text, cboAkunBC.Text, cboTahun.Text);
            }
            else
            {
                //loadChart(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPendapatan(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadBiaya(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaan(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaanKas(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadPenerimaanBank(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadDataTracking(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);

                LoadChartData(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                LoadLineChartData(cboPerwakilan.Text, ObjSys.GetUserId, cboTahun.Text);
                loadDataBC(cboUnit.Text, cboAkunBC.Text, cboTahun.Text);
            }
        }

        protected void loadChart(string cabang = "0", string user = "")
        {
            //ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("Cabang", cabang);
            //ObjGlobal.Param.Add("User", user);
            //if (cabang != "0" || cabang != "")
            //{
            //    string stsCabang = "";
            //    DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
            //    if (mySetSts.Tables[0].Rows.Count > 0)
            //    {
            //        DataRow myRowSts = mySetSts.Tables[0].Rows[0];
            //        stsCabang = myRowSts["stsCabang"].ToString();
            //        ObjGlobal.Param.Add("stsCabang", stsCabang);
            //    }
            //}
            //DataSet myGrafik = ObjGlobal.GetDataProcedure("SPBudgetReal", ObjGlobal.Param);
            //string[] x = new string[myGrafik.Tables[0].Rows.Count];
            //int[] y = new int[myGrafik.Tables[0].Rows.Count];
            //for (int i = 0; i < myGrafik.Tables[0].Rows.Count; i++)
            //{
            //    x[i] = myGrafik.Tables[0].Rows[i][0].ToString();
            //    y[i] = Convert.ToInt32(myGrafik.Tables[0].Rows[i][1]);
            //}
            //Chart2.Series[0].Points.DataBindXY(x, y);
            ////Chart2.Series[0].Label = "#PERCENT{P2}";
            ////Chart2.Series[0].LegendText = "#VALX";
            //Chart2.Series[0].ChartType = SeriesChartType.Pie;
            //Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;
            //Chart2.Legends[0].Enabled = true;
            ////Chart2.Legends[0].LegendStyle = LegendStyle.Column;
            ////Chart2.Legends[0].Docking = Docking.Right;
            //Chart2.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

        }

        // tab HRD

        protected void LoadDataKaryawan(string cabang = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }

            DataSet mySet = ObjGlobal.GetDataProcedure("SPCountEmployee", ObjGlobal.Param);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                lbltotKaryawan.Text = Convert.ToInt32(mySet.Tables[0].Rows[0]["jml"]).ToString();
            }

        }

        protected void LoadDataGuru(string cabang = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }

            DataSet mySet = ObjGlobal.GetDataProcedure("SPCountTeacher", ObjGlobal.Param);
            if (mySet.Tables[0].Rows.Count > 0)
            {
                lbltotGuru.Text = Convert.ToInt32(mySet.Tables[0].Rows[0]["jml"]).ToString();
            }

            DataSet mySet2 = ObjGlobal.GetDataProcedure("SPCountNonTeacher", ObjGlobal.Param);
            if (mySet2.Tables[0].Rows.Count > 0)
            {
                lbltotNonGuru.Text = Convert.ToInt32(mySet2.Tables[0].Rows[0]["jml"]).ToString();
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

        public void SetColorChartData()
        {
            Color[] myPalette = new Color[5]{
                Color.FromKnownColor(KnownColor.DodgerBlue),
                Color.FromKnownColor(KnownColor.Orange),
                Color.FromKnownColor(KnownColor.OrangeRed),
                Color.FromKnownColor(KnownColor.MediumSeaGreen),
                Color.FromKnownColor(KnownColor.Purple)
            };
            this.Chart1.Palette = ChartColorPalette.None;
            this.Chart1.PaletteCustomColors = myPalette;
        }
        private void LoadChartData(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Data", Type.GetType("System.String"));
            dt.Columns.Add("Pengeluaran", Type.GetType("System.Int32"));
            dt.Columns.Add("Pendapatan", Type.GetType("System.Int32"));
            dt.Columns.Add("Penerimaan", Type.GetType("System.Int32"));
            dt.Columns.Add("Saldo_Kas", Type.GetType("System.Int32"));
            dt.Columns.Add("Saldo_Bank", Type.GetType("System.Int32"));
            DataSet myGrafik = ObjGlobal.GetDataProcedure("SPAlertGrafikBiayaKlinik", ObjGlobal.Param);
            foreach (DataRow myRow in myGrafik.Tables[0].Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Data"] = myRow["tglInv"].ToString();
                dr["Pengeluaran"] = Convert.ToInt32(myRow["totalBiaya"]);
                dr["Pendapatan"] = Convert.ToInt32(myRow["totalPendapatan"]);
                dr["Penerimaan"] = Convert.ToInt32(myRow["kasbankmasuk"]);
                dr["Saldo_Kas"] = Convert.ToInt32(myRow["penerimaan_non_dptkas"]);
                dr["Saldo_Bank"] = Convert.ToInt32(myRow["penerimaan_non_dptbank"]);
                dt.Rows.Add(dr);
            }

            List<string> columns = new List<string>();
            columns.Add("Pengeluaran");
            columns.Add("Pendapatan");
            columns.Add("Penerimaan");
            columns.Add("Saldo_Kas");
            columns.Add("Saldo_Bank");
            string[] x = (from p in dt.AsEnumerable()
                          select p.Field<string>("Data")).Distinct().ToArray();
            for (int i = 0; i < columns.Count; i++)
            {
                int[] y = (from p in dt.AsEnumerable()
                           select p.Field<int>(columns[i])).ToArray();
                Chart1.Series.Add(new Series(columns[i]));
                //Chart1.Series[columns[i]].IsValueShownAsLabel = true;
                //Chart1.Series[columns[i]].LabelFormat = "{0:N0}";
                //Chart1.Series[columns[i]].ChartType = SeriesChartType.Column;
                Chart1.Series[columns[i]].Points.DataBindXY(x, y);
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{0:N0}";
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
                //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            }
            Chart1.Legends[0].Enabled = true;

            SetColorChartData();
        }

        public void SetColorLineChartData()
        {
            Color[] myPalette = new Color[2]{
                Color.FromKnownColor(KnownColor.DarkViolet),
                Color.FromKnownColor(KnownColor.Fuchsia)
            };
            this.Chart2.Palette = ChartColorPalette.None;
            this.Chart2.PaletteCustomColors = myPalette;
        }
        private void LoadLineChartData(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Data", Type.GetType("System.String"));
            dt.Columns.Add("Budget", Type.GetType("System.Int32"));
            dt.Columns.Add("Realisasi", Type.GetType("System.Int32"));
            DataSet myGrafik = ObjGlobal.GetDataProcedure("SPAlertGrafikBudgetvsRealisasi", ObjGlobal.Param);
            foreach (DataRow myRow in myGrafik.Tables[0].Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Data"] = myRow["bulan"].ToString();
                dr["Budget"] = Convert.ToInt32(myRow["budget"]);
                dr["Realisasi"] = Convert.ToInt32(myRow["realisasi"]);
                dt.Rows.Add(dr);
            }

            List<string> columns = new List<string>();
            columns.Add("Budget");
            columns.Add("Realisasi");
            string[] x = (from p in dt.AsEnumerable()
                          select p.Field<string>("Data")).Distinct().ToArray();
            for (int i = 0; i < columns.Count; i++)
            {
                int[] y = (from p in dt.AsEnumerable()
                           select p.Field<int>(columns[i])).ToArray();
                //Chart2.Series[columns[i]].LabelFormat = "{0:N0}";
                //Chart2.Series[columns[i]].ChartType = SeriesChartType.Line;


                Chart2.Series.Add(new Series(columns[i]));
                //Chart2.Series[columns[i]].IsValueShownAsLabel = true;
                Chart2.Series[columns[i]].BorderWidth = 2;
                Chart2.Series[columns[i]].ChartType = SeriesChartType.Line;
                Chart2.Series[columns[i]].Points.DataBindXY(x, y);
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{0:N0}";
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
                //Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  
                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            }
            Chart1.Legends[0].Enabled = true;
            SetColorLineChartData();
        }

        protected void loadDataAgama(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);


                }
            }
            DataSet myGrafik = ObjGlobal.GetDataProcedure("SPCountBPJS", ObjGlobal.Param);
            string[] x = new string[myGrafik.Tables[0].Rows.Count];
            int[] y = new int[myGrafik.Tables[0].Rows.Count];
            for (int i = 0; i < myGrafik.Tables[0].Rows.Count; i++)
            {
                x[i] = myGrafik.Tables[0].Rows[i][0].ToString();
                y[i] = Convert.ToInt32(myGrafik.Tables[0].Rows[i][1]);
            }
            Chart3.Series[0].Points.DataBindXY(x, y);
            Chart3.Series[0].Label = "#PERCENT{P2}";
            Chart3.Series[0].LegendText = "#VALX";
            Chart3.Series[0].ChartType = SeriesChartType.Doughnut;
            //Chart3.ChartAreas["ChartArea3"].Area3DStyle.Enable3D = true;
            //Chart3.Legends[0].Enabled = true;
            Chart3.Legends[0].LegendStyle = LegendStyle.Column;
            Chart3.Legends[0].Docking = Docking.Right;
            Chart3.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
        }

        protected void loadDataJK(string cabang = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);


                }
            }
            DataSet myGrafik = ObjGlobal.GetDataProcedure("SPCountJK", ObjGlobal.Param);
            string[] x = new string[myGrafik.Tables[0].Rows.Count];
            int[] y = new int[myGrafik.Tables[0].Rows.Count];
            for (int i = 0; i < myGrafik.Tables[0].Rows.Count; i++)
            {
                x[i] = myGrafik.Tables[0].Rows[i][0].ToString();
                y[i] = Convert.ToInt32(myGrafik.Tables[0].Rows[i][1]);
            }
            Chart4.Series[0].Points.DataBindXY(x, y);
            Chart4.Series[0].Label = "#PERCENT{P2}";
            Chart4.Series[0].LegendText = "#VALX";
            Chart4.Series[0].ChartType = SeriesChartType.Pie;
            //Chart4.ChartAreas["ChartArea3"].Area3DStyle.Enable3D = true;
            //Chart4.Legends[0].Enabled = true;
            Chart4.Legends[0].LegendStyle = LegendStyle.Column;
            Chart4.Legends[0].Docking = Docking.Right;
            Chart4.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
        }

        protected void LoadDataTracking(string cabang = "0", string user = "", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cabang);
            ObjGlobal.Param.Add("noUser", user);
            ObjGlobal.Param.Add("tahun", tahun);
            if (cabang != "0" || cabang != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cabang + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            grdTracking.DataSource = ObjGlobal.GetDataProcedure("SPViewtreakinginput", ObjGlobal.Param);
            grdTracking.DataBind();
        }

        protected void grdTracking_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "";
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jan";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Feb";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Mar";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Apr";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "May";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jun";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Jul";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Aug";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Sep";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Okt";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Nov";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Dec";
            cell.CssClass = "text-center";
            cell.ColumnSpan = 2;
            cell.BorderWidth = 1;
            row.Controls.Add(cell);

            grdTracking.HeaderRow.Parent.Controls.AddAt(0, row);
        }

        protected void grdTracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "cursor:help;");
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#96c6ea'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
                    e.Row.BackColor = Color.FromName("#ffffff");
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#96c6ea'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#d7e9f7'");
                    e.Row.BackColor = Color.FromName("#d7e9f7");
                }
            }
        }

        protected void cboTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnit.Text == "0")
            {
                LoadDataSiswa(cboPerwakilan.Text);
                LoadDataKaryawan(cboPerwakilan.Text);
                LoadDataGuru(cboPerwakilan.Text);
                loadDataAgama(cboPerwakilan.Text);
                loadDataJK(cboPerwakilan.Text);

                string cabang = cboPerwakilan.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;

                //loadChart(cabang, user, tahun);
                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }
            else
            {
                LoadDataSiswa(cboUnit.Text);
                LoadDataKaryawan(cboUnit.Text);
                LoadDataGuru(cboUnit.Text);
                loadDataAgama(cboUnit.Text);
                loadDataJK(cboUnit.Text);

                string cabang = cboUnit.Text;
                string user = ObjSys.GetUserId;
                string tahun = cboTahun.Text;
                string norek = cboAkunBC.Text;

                //loadChart(cabang, user, tahun);
                LoadPendapatan(cabang, user, tahun);
                LoadBiaya(cabang, user, tahun);
                LoadPenerimaan(cabang, user, tahun);
                LoadPenerimaanKas(cabang, user, tahun);
                LoadPenerimaanBank(cabang, user, tahun);
                LoadDataTracking(cabang, user, tahun);

                LoadChartData(cabang, user, tahun);
                LoadLineChartData(cabang, user, tahun);
                loadDataBC(cabang, norek, tahun);
            }
        }

        protected void cboAkunBC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataSiswa(cboUnit.Text);
            LoadDataKaryawan(cboUnit.Text);
            LoadDataGuru(cboUnit.Text);
            loadDataAgama(cboUnit.Text);
            loadDataJK(cboUnit.Text);

            string cabang = cboUnit.Text;
            string user = ObjSys.GetUserId;
            string tahun = cboTahun.Text;
            string norek = cboAkunBC.Text;
            //loadChart(cabang, user, tahun);
            LoadPendapatan(cabang, user, tahun);
            LoadBiaya(cabang, user, tahun);
            LoadPenerimaan(cabang, user, tahun);
            LoadPenerimaanKas(cabang, user, tahun);
            LoadPenerimaanBank(cabang, user, tahun);
            LoadDataTracking(cabang, user, tahun);
            LoadChartData(cabang, user, tahun);
            LoadLineChartData(cabang, user, tahun);
            loadDataBC(cabang, norek, tahun);
        }
        protected void loadDataBC(string cabang = "0", string norek = "0", string tahun = "")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cabang);
            ObjGlobal.Param.Add("thn", tahun);
            ObjGlobal.Param.Add("norek", norek);
            grdHarianGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceKas", ObjGlobal.Param);
            grdHarianGL.DataBind();
        }
        protected void grdHarianGL_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "SelectHarian")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");

                DataSet mySetRek = ObjDb.GetRows("SELECT ket FROM mRekening where norek = " + hdnnorek.Value + " ");
                DataRow myRowRek = mySetRek.Tables[0].Rows[0];
                string ket = myRowRek["ket"].ToString();

                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                lblbln.Text = hdnbulan.Value;
                lblthn.Text = hdnthn.Value;
                lblrek.Text = ket;
                lblunit.Text = unit;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_BukuHarian", ObjGlobal.Param);
                grdAccount.DataBind();

                dlgHarianKas.Show();
            }
            else if (e.CommandName == "SelectDetail")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");

                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                lblbln2.Text = hdnbulan.Value;
                lblthn2.Text = hdnthn.Value;
                lblunit2.Text = unit;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashPendapatan", ObjGlobal.Param);
                GridView1.DataBind();


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashPengeluaran", ObjGlobal.Param);
                grdAccount1.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashRKMasuk", ObjGlobal.Param);
                grdAccount2.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                grdAccount3.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilCashRKKeluar", ObjGlobal.Param);
                grdAccount3.DataBind();

                dlgDetilKas.Show();
            }

            if (e.CommandName == "SelectDetailkas")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");


                DataSet mySetUnit = ObjDb.GetRows("SELECT namaCabang FROM mCabang where nocabang = " + hdnnocabang.Value + " ");
                DataRow myRowUnit = mySetUnit.Tables[0].Rows[0];
                string unit = myRowUnit["namaCabang"].ToString();

                Label5.Text = hdnbulan.Value;
                Label7.Text = hdnthn.Value;
                Label6.Text = unit;


                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView2.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilPenerimaan", ObjGlobal.Param);
                GridView2.DataBind();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView3.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_DetilPengeluaran", ObjGlobal.Param);
                GridView3.DataBind();

                grddeatailbln.Show();
            }
        }

    }
}