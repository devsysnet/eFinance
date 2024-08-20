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
    public partial class Rdetailtagihanpertgl : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();


            }
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
                            cmd = new SqlCommand("SPRReportdetailpertanggal", con);
                            cmd.Parameters.Add(new SqlParameter("@dtmulai", dtmulai.Text));
                            cmd.Parameters.Add(new SqlParameter("@dtsampai", dtsampai.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Detail_Pembayaran_pertanggal" + ObjSys.GetNow + ".xls";
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
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("dtmulai", dtmulai.Text);
            ObjGlobal.Param.Add("dtsampai", dtsampai.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("SPRReportdetailpertanggal", ObjGlobal.Param));

            DataSet mySet = ObjDb.GetRows("SELECT CONVERT(varchar, CAST(sum(CAST(CAST(a.[Value] AS float) AS decimal(18,0))) AS Money), 1) AS nilaiBayar FROM Thistoribayar hb CROSS APPLY (SELECT pos, Value FROM [dbo].[SplitString](hb.nopiutang,',') ) c CROSS APPLY (SELECT pos, Value FROM [dbo].[SplitString](hb.nilaibayar,',') ) a left join TransPiutang pt on pt.noPiutang = c.[Value] left join tKas tk on tk.noKas = hb.nokas inner join mJenisTransaksi mjt on mjt.noTransaksi = pt.noTransaksi inner join mCabang mc on mc.noCabang = pt.nocabang WHERE c.pos = a.pos  and pt.nocabang =" + cboCabang.Text + " and tk.tgl BETWEEN '" + dtmulai.Text + "' and '" + dtsampai.Text + "'");
            DataRow myRow = mySet.Tables[0].Rows[0];
            string systembudget = myRow["nilaiBayar"].ToString();
            txtTotal.Text = systembudget;
        }

        protected void loadDataCombo()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("GetstsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("GetstsPusat", ObjSys.GetstsPusat);
            cboCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadCabangTagihanSiswa", ObjGlobal.Param);
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat


        }



    }
}