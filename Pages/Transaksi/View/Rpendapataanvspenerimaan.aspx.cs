using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using eFinance.GlobalApp;
using System.Web.UI.DataVisualization.Charting;

namespace eFinance.Pages.Transaksi.View
{
    public partial class Rpendapataanvspenerimaan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboYear.DataSource = ObjGlobal.GetDataProcedureDataTable("SPpilihanthn");
                cboYear.DataValueField = "id";
                cboYear.DataTextField = "name";
                cboYear.DataBind();
                loadDataCombo();
            }
        }


        #region LoadData
        protected void btnPosting_Click(object sender, EventArgs e)
        {
            Chart2.Visible = true;
            linkBR.Visible = true;
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            ObjGlobal.Param.Add("tahun", cboYear.Text);
            if (cboCabang.Text != "0" || cboCabang.Text != "")
            {
                string stsCabang = "";
                DataSet mySetSts = ObjDb.GetRows("SELECT stsCabang FROM mCabang where noCabang = '" + cboCabang.Text + "' ");
                if (mySetSts.Tables[0].Rows.Count > 0)
                {
                    DataRow myRowSts = mySetSts.Tables[0].Rows[0];
                    stsCabang = myRowSts["stsCabang"].ToString();
                    ObjGlobal.Param.Add("stsCabang", stsCabang);
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Data", Type.GetType("System.String"));
            dt.Columns.Add("Pendapatan", Type.GetType("System.Int32"));
            dt.Columns.Add("Penerimaan", Type.GetType("System.Int32"));
            DataSet myGrafik = ObjGlobal.GetDataProcedure("SPAlertGrafikdptvsterima", ObjGlobal.Param);
            foreach (DataRow myRow in myGrafik.Tables[0].Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Data"] = myRow["bulan"].ToString();
                dr["Pendapatan"] = Convert.ToInt32(myRow["budget"]);
                dr["Penerimaan"] = Convert.ToInt32(myRow["realisasi"]);
                dt.Rows.Add(dr);
            }

            List<string> columns = new List<string>();
            columns.Add("Pendapatan");
            columns.Add("Penerimaan");
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

                Chart2.Series[columns[i]].Points.DataBindXY(x, y);
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "{0:N0}";
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
                //Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;  
                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            }
            SetColorLineChartData();

        }
        protected void linkBR_Click(object sender, EventArgs e)
        {
            string cabangId = cboCabang.SelectedValue;
            string tahunId = cboYear.SelectedValue;
            string link = string.Format("~/Pages/Transaksi/View/RBudgetvsRealisasiTable.aspx?cabang={0}&tahun={1}", cabangId, tahunId);
            Response.Redirect(link);
        }
        public void SetColorLineChartData()
        {
            Color[] myPalette = new Color[2]{
                Color.FromKnownColor(KnownColor.DodgerBlue),
                Color.FromKnownColor(KnownColor.Orange)
            };
            this.Chart2.Palette = ChartColorPalette.None;
            this.Chart2.PaletteCustomColors = myPalette;
        }
        protected void loadDataCombo()
        {

            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM vCabang where nocabang='" + ObjSys.GetCabangId + "') a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            string cabang = Request.QueryString.Get("cabang");
            string tahun = Request.QueryString.Get("tahun");

            if (cabang != null && cabang != "")
            {
                cboCabang.SelectedValue = cabang;

                // Cari otomatis
                btnPosting_Click(new object(), new EventArgs());
            }
        }
        #endregion

        protected void btnCari_Click(object sender, EventArgs e)
        {
            //LoadDataGrid();
        }


    }
}
