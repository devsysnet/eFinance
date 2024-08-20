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
    public partial class Rbalancekas : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadDataCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                LoadIndexYayasan();
                tabGrid.Visible = true;
                tabForm.Visible = false;
                divHariankas.Visible = false;
                loadCombo();
            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }
        protected void loadCombo()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnnoYysn.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where noCabang=" + ObjSys.GetParentCabang + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where  stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            loadDataUnit(cboPerwakilan.Text);
            //LoadDataCombo2();
        }
        //protected void LoadDataCombo2()
        //{
        //    if (ObjSys.GetstsPusat == "1")
        //    {
        //        cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
        //        cboCabang.DataValueField = "id";
        //        cboCabang.DataTextField = "name";
        //        cboCabang.DataBind();
        //    }
        //    //unit
        //    else if (ObjSys.GetstsPusat == "2")
        //    {
        //        cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
        //        cboCabang.DataValueField = "id";
        //        cboCabang.DataTextField = "name";
        //        cboCabang.DataBind();
        //    }
        //    //admin Kantor Perwakilan Bogor GetstsPusat=3
        //    else if (ObjSys.GetstsPusat == "3")
        //    {
        //        cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + cboPerwakilan.Text + ") a");
        //        cboCabang.DataValueField = "id";
        //        cboCabang.DataTextField = "name";
        //        cboCabang.DataBind();
        //    }
        //    else
        //    {
        //        cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
        //        cboCabang.DataValueField = "id";
        //        cboCabang.DataTextField = "name";
        //        cboCabang.DataBind();
        //    }
        //    LoadDataAccount(cboCabang.Text);


        //}
        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            //ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            LoadDataAccount(cboCabang.Text);

        }
        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataCombo2();
            loadDataUnit(cboPerwakilan.Text);

            // LoadData(cboPerwakilan.Text, cboCabang.Text);
        }
        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadData(cboPerwakilan.Text, cboCabang.Text);
            LoadDataAccount(cboCabang.Text);

        }

        protected void LoadData(string perwakilan = "0", string unit = "0")
        {
          
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", unit);
            ObjGlobal.Param.Add("norek", cboAccount.Text);
            ObjGlobal.Param.Add("thn", cboYear.Text);
            //ObjGlobal.Param.Add("jnsbank", ObjSys.Getjnsbank);
            grdHarianGL.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceKas", ObjGlobal.Param);
            grdHarianGL.DataBind();


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
                            cmd = new SqlCommand("SPLoadDataBalanceCash_BukuHarian1", con);
                            cmd.Parameters.Add(new SqlParameter("@bln", hdnblnHarianKas.Value));
                            cmd.Parameters.Add(new SqlParameter("@tahun", hdnthnHarianKas.Value));
                            cmd.Parameters.Add(new SqlParameter("@norek", hdnnorekHarianKas.Value));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", hdnnocabangHarianKas.Value));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            String fileName = "Balance_Kas_Harian_Kas_" + ObjSys.GetNow + ".xls";
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
     
        protected void loadDataCombo()
        {

            //Perwakilan Mardi Yuana Bogor GetstsPusat=1
            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (2,3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            LoadDataAccount(cboCabang.Text);


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
        protected void grdRekap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = int.Parse(e.CommandArgument.ToString());

                if (e.CommandName == "SelectDetailRekapTransaksi")
                {
                    HiddenField hdnId = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdnId");
                    HiddenField hdntgl = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdntgl");
                    //HiddenField hdnnorekRTransaksi = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdnnorekRTransaksi");
                    HiddenField hdnNocabangRTransaksi = (HiddenField)grdRekap.Rows[rowIndex].FindControl("hdnNocabangRTransaksi");


                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noTransaksi", hdnId.Value);
                    //ObjGlobal.Param.Add("norek", hdnnorekRTransaksi.Value);
                    ObjGlobal.Param.Add("nocabang", hdnNocabangRTransaksi.Value);
                    ObjGlobal.Param.Add("tgl", hdntgl.Value);
                    GridView4.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekapTransaksiDetail", ObjGlobal.Param);
                    GridView4.DataBind();

                    dlgDetilKasRTransaksi.Show();
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void grdHarianGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdHarianGL.PageIndex = e.NewPageIndex;
            //LoadData();
        }

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataAccount(cboCabang.Text);
        }

        protected void LoadDataAccount(string cabang)
        {

            if (ObjSys.Getjnsbank == "1")
            {
                cboAccount.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + cabang + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
                cboAccount.DataValueField = "id";
                cboAccount.DataTextField = "name";
                cboAccount.DataBind();
            }

            if (ObjSys.Getjnsbank == "0")
            {
                cboAccount.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
                cboAccount.DataValueField = "id";
                cboAccount.DataTextField = "name";
                cboAccount.DataBind();
            }


        }

        protected void btnPosting_Click(object sender, EventArgs e)
        {
            CloseMessage();
             LoadData(cboPerwakilan.Text,cboCabang.Text);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
                divHariankas.Visible = false;
            tabGrid.Visible = true;
            tabForm.Visible = false;
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
                hdnblnHarianKas.Value = hdnbln.Value;
                hdnthnHarianKas.Value = hdnthn.Value;
                hdnnorekHarianKas.Value = hdnnorek.Value;
                hdnnocabangHarianKas.Value = hdnnocabang.Value;

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("bln", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                GridView5.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBalanceCash_BukuHarian", ObjGlobal.Param);
                GridView5.DataBind();
                divHariankas.Visible = true;
                tabGrid.Visible = false;
                tabForm.Visible = false;
            }
            if (e.CommandName == "SelectDetail")
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

                Label3.Text = hdnbulan.Value;
                Label4.Text = hdnthn.Value;
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
            if (e.CommandName == "Detail")
            {

                int rowIndex = int.Parse(e.CommandArgument.ToString());
                HiddenField hdnbulan = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbulan");
                HiddenField hdnbln = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnbln");
                HiddenField hdnthn = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnthn");
                HiddenField hdnnorek = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnorek");
                HiddenField hdnnocabang = (HiddenField)grdHarianGL.Rows[rowIndex].FindControl("hdnnocabang");

                tabGrid.Visible = false;
                tabForm.Visible = true;
                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("nocabang", hdnnocabang.Value);
                ObjGlobal.Param.Add("bulan", hdnbln.Value);
                ObjGlobal.Param.Add("tahun", hdnthn.Value);
                ObjGlobal.Param.Add("norek", hdnnorek.Value);
                grdRekap.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekapTransaksi", ObjGlobal.Param);
                grdRekap.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate()", "Calculate();", true);

            }
        }

    }
}
