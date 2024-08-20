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
    public partial class RTransaksiBayar : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataPerwakilan();
                loadDatathnajaran();
                LoadIndexYayasan();

            }
        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnYayasan.Value = myRow["noCabang"].ToString();
        }
        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("notransaksi", cboJnsTrans.Text);
            ObjGlobal.Param.Add("tahunajaran", cbothnajaran.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataTransaksiBayar", ObjGlobal.Param);
            grdAccount.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "getAmount();", true);
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
                            cmd = new SqlCommand("SPLoadDataTransaksiBayar", con);
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            cmd.Parameters.Add(new SqlParameter("@notransaksi", cboJnsTrans.Text));
                            cmd.Parameters.Add(new SqlParameter("@tahunajaran", cbothnajaran.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboPerwakilan.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Pembayaransiswa_" + ObjSys.GetNow + ".xls";
                            ViewHelper.DownloadExcel(Response, fileName, dt);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            //ShowMessage("error", ex.ToString());
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //ShowMessage("error", ex.ToString());
            }
        }

        protected void loadDataPerwakilan()
        {
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Perwakilan' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnYayasan.Value + " and stspusat=0 and stscabang=1) a");
                cboPerwakilan.DataValueField = "id";
                cboPerwakilan.DataTextField = "name";
                cboPerwakilan.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboPerwakilan.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + hdnYayasan.Value + " and noCabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang=1) a");
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
        }

        protected void loadDataUnit(string perwakilan = "0")
        {


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            loadDataDetil(cboCabang.Text);

        }

        protected void loadDataDetil(string cabang = "")
        {
            DataSet mySet = ObjDb.GetRows("select tahunAjaran from parameter ");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string tahunAjaran = myRow["tahunAjaran"].ToString();
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Semua Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cabang + "' and tahunAjaran = '" + tahunAjaran + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cboJnsTrans.DataSource = ObjDb.GetRows("select * from (select '' as id, '--Pilih Transaksi--' as name union select distinct noTransaksi as id, jenisTransaksi as name from mJenisTransaksi where nocabang='" + cabang + "')x");
            cboJnsTrans.DataValueField = "id";
            cboJnsTrans.DataTextField = "name";
            cboJnsTrans.DataBind();
        }
        #endregion

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataDetil(cboCabang.Text);
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit(cboPerwakilan.Text);
        }

        protected void loadDatathnajaran()
        {
            ObjGlobal.Param.Clear();

            cbothnajaran.DataSource = ObjGlobal.GetDataProcedure("SPLoadTahunajaran", ObjGlobal.Param);
            cbothnajaran.DataValueField = "id";
            cbothnajaran.DataTextField = "name";
            cbothnajaran.DataBind();
        }

    }
    
}