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
    public partial class rSaldotagihansiswa : System.Web.UI.Page
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
            }
        }


        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                "namasiswa",
                "kelas"
            };

            // Clear and add initial columns
            grdAccount.Columns.Clear();
            foreach (String datacolumn in datacolumns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = datacolumn;
                bfield.DataField = datacolumn;
                grdAccount.Columns.Add(bfield);
            }

            // Add new columns
            DataTable headerTable = ds.Tables[0];
            int headerLength = headerTable.Rows.Count;
            for (int i = 0; i < headerLength; i++)
            {
                String labelJenis = headerTable.Rows[i]["jenistransaksi"].ToString();
                String jenis = headerTable.Rows[i]["jenistransaksi"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = jenis;
                bfield.DataField = jenis;
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdAccount.Columns.Add(bfield);
            }


            // Fill data if exists
            if (ds.Tables.Count > 1)
            {
                DataTable dataTable = ds.Tables[1];
                grdAccount.DataSource = dataTable;
            }
            grdAccount.DataBind();
        }

        #region LoadData

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
                            cmd = new SqlCommand("SPSaldotagihansiswa", con);
                            cmd.Parameters.Add(new SqlParameter("@kelas", cboKelas.Text));
                            //cmd.Parameters.Add(new SqlParameter("@sdtgl", dtSampai.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Report_saldoTagihan_" + ObjSys.GetNow + ".xls";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            //ObjGlobal.Param.Add("sdtgl", dtSampai.Text);
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("SPSaldotagihansiswa", ObjGlobal.Param));
        }

        protected void loadDataPerwakilan()
        {
            loadDataUnit();
        }

        protected void loadDataUnit()
        {


            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("parentCabang", ObjSys.GetParentCabang);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUnitPerwakilan1", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            loadDataDetil(cboCabang.Text);

        }

        protected void loadDataDetil(string cabang = "")
        {
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, '--Semua Kelas--' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cabang + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();
        }
        #endregion

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataDetil(cboCabang.Text);
        }

        protected void cboPerwakilan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataUnit();
        }

    }
}