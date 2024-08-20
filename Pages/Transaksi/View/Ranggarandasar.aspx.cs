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
    public partial class Ranggarandasar : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadIndexYayasan();
                loadCombo();
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
 
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
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {

                        SqlCommand cmd = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        try
                        {
                            cmd = new SqlCommand("SPRanggarandasar", con);
                            cmd.Parameters.Add(new SqlParameter("@thn", cboYear.Text));
                            cmd.Parameters.Add(new SqlParameter("@from", cboMonth.Text));
                            cmd.Parameters.Add(new SqlParameter("@to", cboMonth1.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboPerwakilan.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Anggaran" + ObjSys.GetNow + ".xls";
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

        #region LoadData
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("thn", cboYear.Text);
            ObjGlobal.Param.Add("from", cboMonth.Text);
            ObjGlobal.Param.Add("to", cboMonth1.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            grdAccount.DataSource = ObjGlobal.GetDataProcedure("SPRanggarandasar", ObjGlobal.Param);
            grdAccount.DataBind();
            LoadJudul(cboCabang.Text, cboYear.Text, cboPerwakilan.Text);

        }
        protected void LoadJudul(string nocabang, string thn,string perwakilan)
        {
            //DataSet mySet = ObjDb.GetRows("select namacabang from mcabang where nocabang = '" + nocabang + "' ");
            //DataSet mySeta = ObjDb.GetRows("select namacabang from mcabang where nocabang = '" + perwakilan + "' ");
            //DataRow myRow = mySet.Tables[0].Rows[0];
            //DataRow myRowa = mySeta.Tables[0].Rows[0];
            //string namacabang = myRow["namacabang"].ToString();
            //string yayasan = myRowa["namacabang"].ToString();
            //string cabang = "";
        
            //if(nocabang == "0" ||nocabang == "")
            //{
            //    cabang = yayasan;
            //}
            //else
            //{
            //    cabang = namacabang;
            //}
           

            //judul.InnerText = "Anggaran " + cabang;

          
            periode.InnerText = "Periode " + thn;

        }
        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }
        protected void printPajak(object sender, EventArgs e)
        {
  
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenReport", "printPajak();", true);
        }
 
        #endregion
    
 
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
        }
        protected void loadDataUnit(string perwakilan = "0")
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", perwakilan);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan3", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();


        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit(cboPerwakilan.Text);
            //LoadData(cboPerwakilan.Text, cboCabang.Text);
        }
    }
}