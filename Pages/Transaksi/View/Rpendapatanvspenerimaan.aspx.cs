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
    public partial class Rpendapatanvspenerimaan : System.Web.UI.Page
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
                //cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPLoadComboTahun");
                //cboYear.DataValueField = "id";
                //cboYear.DataTextField = "name";
                //cboYear.DataBind();

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
                            cmd = new SqlCommand("SPRReportPendapatanvsPenerimaan", con);
                            cmd.Parameters.Add(new SqlParameter("@dtmulai", dtmulai.Text));
                            cmd.Parameters.Add(new SqlParameter("@dtsampai", dtsampai.Text));
                            cmd.Parameters.Add(new SqlParameter("@nocabang", cboCabang.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            String fileName = "Detail_PendapatanVSpenerimaan" + ObjSys.GetNow + ".xls";
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
            tabelDinamis(ObjGlobal.GetDataProcedure("SPRReportPendapatanvsPenerimaan", ObjGlobal.Param));
        }

        protected void loadDataCombo()
        {
            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang=3) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang where stscabang=3) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 3 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang b inner join (select parent from mcabang where nocabang = '" + ObjSys.GetCabangId + "') c on b.parent=c.parent and stscabang=3) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }



    }
}