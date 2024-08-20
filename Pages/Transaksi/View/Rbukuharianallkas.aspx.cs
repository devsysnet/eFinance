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
    public partial class Rbukuharianallkas : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();

            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            try
            {
                if (valid == true)
                {


                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
                        ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
                        ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                        DataSet dataSet = ObjGlobal.GetDataProcedure("SPLoadDataBukuHarianall", ObjGlobal.Param);


                        String fileName = "Buku_Harian_KasALL_" + ObjSys.GetNow + ".xls";
                        ViewHelper.DownloadExcel(Response, fileName, dataSet.Tables[0]);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }

                    //}
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

        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    bool valid = true;
        //    string message = "", alert = "";
        //    try
        //    {
        //        if (valid == true)
        //        {
        //            using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
        //            {

        //                SqlCommand cmd = new SqlCommand();
        //                SqlDataAdapter da = new SqlDataAdapter();
        //                DataTable dt = new DataTable();
        //                try
        //                {
        //                    cmd = new SqlCommand("SPLoadDataBukuHarianall", con);
        //                    cmd.Parameters.Add(new SqlParameter("@dtMulai", dtMulai.Text));
        //                    cmd.Parameters.Add(new SqlParameter("@dtSampai", dtSampai.Text));
        //                    //cmd.Parameters.Add(new SqlParameter("@kdrek", cboAccount.Text));
        //                    cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    da.SelectCommand = cmd;
        //                    da.Fill(dt);

        //                    String fileName = "Buku_Harian_KasALL_" + ObjSys.GetNow + ".xls";
        //                    ViewHelper.DownloadExcel(Response, fileName, dt);
        //                }
        //                catch (Exception ex)
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //                    ShowMessage("error", ex.ToString());
        //                }

        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //            ShowMessage(alert, message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("error", ex.ToString());
        //    }
        //}

        protected void ShowMessage(string _class = "", string _message = "")
        {
            ((Label)Master.FindControl("lblMessage")).Text = ObjSys.GetMessage(_class, _message);
            ((Label)Master.FindControl("lblMessage")).Visible = true;
        }
        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            //ObjGlobal.Param.Add("kdrek", cboAccount.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBukuHarianALL", ObjGlobal.Param);
            grdAccount.DataBind();
            //grdAccount1.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBukuHarianALL", ObjGlobal.Param);
            //grdAccount1.DataBind();

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

             

            //LoadDataAccount(cboCabang.Text);
        }
        #endregion

        //protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadDataAccount(cboCabang.Text);
        //}

        //protected void LoadDataAccount(string cabang)
        //{
        //    cboAccount.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='2' and nocabang = '" + cabang + "' union all SELECT distinct noRek id, kdRek +' ( ' + Ket +' )' name FROM mRekening where stsAktif = '1' and sts='2' and jenis='1') a order by a.id");
        //    cboAccount.DataValueField = "id";
        //    cboAccount.DataTextField = "name";
        //    cboAccount.DataBind();
        //}
    }
}
