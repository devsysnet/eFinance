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
    public partial class RkesanggupanBayar1 : System.Web.UI.Page
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
                loadDataCombo();

                //LoadData(cboPerwakilan.Text, cboUnit.Text, cboKelas.Text, cbotahunajaran.Text);

            }
        }

        protected void LoadIndexYayasan()
        {
            DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataIndexYayasan");
            DataRow myRow = mySet.Tables[0].Rows[0];
            hdnnoYysn.Value = myRow["noCabang"].ToString();
        }

        protected void tabelDinamis(DataSet ds)
        {
            // Clear and add initial columns
            grdAccount.Columns.Clear();

            DataTable headerTable = ds.Tables[0];
            // Add new columns
            foreach (DataColumn column in headerTable.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = column.ColumnName;
                bfield.DataField = column.ColumnName;
                grdAccount.Columns.Add(bfield);
            }

            grdAccount.DataSource = ds;



            // Fill data if exists
            if (ds.Tables.Count > 1)
            {
                DataTable dataTable = ds.Tables[1];
                grdAccount.DataSource = dataTable;
            }
            grdAccount.DataBind();
        }

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("perwakilan", cboPerwakilan.Text);
            ObjGlobal.Param.Add("noCabang", cboUnit.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("tahunajaran", cbotahunajaran.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("ReportDataKesanggupanBayar1", ObjGlobal.Param));
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
                            cmd = new SqlCommand("ReportDataKesanggupanBayar1", con);
                            cmd.Parameters.Add(new SqlParameter("@perwakilan", cboPerwakilan.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboUnit.Text));
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            cmd.Parameters.Add(new SqlParameter("@tahunajaran", cbotahunajaran.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Kesanggupanbyr_" + ObjSys.GetNow + ".xls";
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


        protected void loadDataCombo()
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
            LoadDataCombo2();



        }

        protected void LoadDataCombo2()
        {
            if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "2")
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + ObjSys.GetParentCabang + " and nocabang=" + ObjSys.GetCabangId + " and stspusat=0 and stscabang in(2,3)) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }
            else
            {
                cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'Semua Unit' name union all SELECT distinct noCabang id, namaCabang name FROM vcabang where parent=" + cboPerwakilan.Text + " and stspusat=0 and stscabang in(2,3)) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }

            LoadDataCombo3(cboUnit.Text);
        }

        protected void LoadDataCombo3(string nocabang = "")
        {
            cbotahunajaran.DataSource = ObjDb.GetRows("select a.* from (select distinct tahunAjaran as id,tahunAjaran as name from parameter) a");
            cbotahunajaran.DataValueField = "id";
            cbotahunajaran.DataTextField = "name";
            cbotahunajaran.DataBind();
            loadDataComboKelas(cboUnit.Text, cbotahunajaran.Text);
        }
        protected void loadDataComboKelas(string nocabang = "", string thnAjaran = "")
        {
            if (cboUnit.Text == "0")
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, 'Semua Kelas' as name union select distinct kelas as id, kelas as name from TransKelas where tahunAjaran='" + thnAjaran + "')x order by id");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
            else
            {
                cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, 'Semua Kelas' as name union select distinct kelas as id,kelas as name from TransKelas where tahunAjaran='" + thnAjaran + "' and nocabang='" + nocabang + "')x order by id");
                cboKelas.DataValueField = "id";
                cboKelas.DataTextField = "name";
                cboKelas.DataBind();
            }
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataCombo2();
        }

        protected void cboUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas(cboUnit.Text, cbotahunajaran.Text);
            LoadDataCombo3(cboUnit.Text);
           
        }

        protected void cbothnAjaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataComboKelas(cboUnit.Text, cbotahunajaran.Text);
        
        }

    }
}