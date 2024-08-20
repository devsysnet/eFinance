using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.View
{
    public partial class RSanggupBayar : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadCombo();
                loadCombo2();
                loadData();

            }
        }

        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                "NIS",
                "NamaSiswa",
                "NoVirtual"
              

            };

            // Clear and add initial columns
            grdSanggupBayar.Columns.Clear();
            foreach (String datacolumn in datacolumns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = datacolumn;
                bfield.DataField = datacolumn;
                bfield.HeaderStyle.CssClass = "text-center";
                grdSanggupBayar.Columns.Add(bfield);
            }

            // Add new columns
            DataTable headerTable = ds.Tables[0];
            int headerLength = headerTable.Rows.Count;
            for (int i = 0; i < headerLength; i++)
            {
                String labelTransaksi = headerTable.Rows[i]["labeltransaksi"].ToString();
                String transaksi = headerTable.Rows[i]["transaksi"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = labelTransaksi;
                bfield.DataField = transaksi;
                bfield.HeaderStyle.CssClass = "text-center";
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdSanggupBayar.Columns.Add(bfield);
            }

            List<String> datacolumns2 = new List<String> {
                "TotalBayar",
                "saldo"

            };

            foreach (String datacolumn2 in datacolumns2)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = datacolumn2;
                bfield.DataField = datacolumn2;
                bfield.HeaderStyle.CssClass = "text-center";
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdSanggupBayar.Columns.Add(bfield);
            }

            // Fill data if exists
            if (ds.Tables.Count > 1)
            {
                DataTable dataTable = ds.Tables[1];
                grdSanggupBayar.DataSource = dataTable;
            }
            grdSanggupBayar.DataBind();
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", cboCabang.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("tahunajaran", cboTA.Text);

            //tabelDinamis(ObjGlobal.GetDataProcedure("SPSanggupBayar", ObjGlobal.Param));
            grdSanggupBayar.DataSource = ObjGlobal.GetDataProcedure("SPSanggupBayar", ObjGlobal.Param);
            grdSanggupBayar.DataBind();

        }

        protected void loadCombo()
        {

            //GetstsCabang = stsCabang, GetstsPusat = stsPusat di mCabang
            //pusat
            if (ObjSys.GetstsCabang == "1" && ObjSys.GetstsPusat == "0")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where stscabang=2) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //perwakilan
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 1 and noCabang = '" + ObjSys.GetCabangId + "' " +
                        "union " +
                        "SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsPusat = 0 and stsCabang = 2 and parent = '" + ObjSys.GetCabangId + "') a ");
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

        protected void loadCombo2()
        {
            cboKelas.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Pilih Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + cboCabang.Text + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cboTA.DataSource = ObjDb.GetRows("select * from (select '' as id, '---Pilih Tahun Ajaran---' name union select distinct tahunAjaran as id, tahunAjaran as name from TransKelas where nocabang='" + cboCabang.Text + "')x");
            cboTA.DataValueField = "id";
            cboTA.DataTextField = "name";
            cboTA.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdSanggupBayar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSanggupBayar.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCombo2();
        }
    }
}