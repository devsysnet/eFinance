using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class DownloadARSiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                ShowHideGridAndForm(true, false);
            }
        }

        private void loadDataCombo()
        {
            if (ObjSys.GetstsPusat == "3")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2 and parent = '" + ObjSys.GetParentCabang + "') a order by a.noUrut");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            else if (ObjSys.GetstsPusat == "2")
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("SELECT distinct nocabang id, namaCabang name FROM vCabang WHERE stsCabang = 2 and noCabang = '" + ObjSys.GetCabangId + "'");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }
            else
            {
                cboPerwakilanUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-Pilih Unit-' name, 0 noUrut union all SELECT distinct nocabang id, namaCabang name, noUrut FROM vCabang WHERE stsCabang = 2) a order by a.noUrut");
                cboPerwakilanUnit.DataValueField = "id";
                cboPerwakilanUnit.DataTextField = "name";
                cboPerwakilanUnit.DataBind();
            }

            cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name union select distinct year(tgl) as id, CONVERT(varchar,year(tgl)) as name from TransPiutang )x");
            cboTahun.DataValueField = "id";
            cboTahun.DataTextField = "name";
            cboTahun.DataBind();

            cboBulan.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Bulan-' as name union select distinct month(tgl) as id, DATENAME(mm, tgl) as name from TransPiutang)x");
            cboBulan.DataValueField = "id";
            cboBulan.DataTextField = "name";
            cboBulan.DataBind();
        }

        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                "NoVirtual",
                "NamaSiswa"
            };

            // Clear and add initial columns
            grdDownloadARSiswa.Columns.Clear();
            foreach (String datacolumn in datacolumns)
            {
                BoundField bfield = new BoundField();
                bfield.HeaderText = datacolumn;
                bfield.DataField = datacolumn;
                grdDownloadARSiswa.Columns.Add(bfield);
            }

            // Add new columns
            DataTable headerTable = ds.Tables[0];
            int headerLength = headerTable.Rows.Count;
            for (int i = 0; i < headerLength; i++)
            {
                String labeljenisTransaksi = headerTable.Rows[i]["labeljenisTransaksi"].ToString();
                String jenisTransaksi = headerTable.Rows[i]["jenisTransaksi"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = labeljenisTransaksi;
                bfield.DataField = jenisTransaksi;
                bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                grdDownloadARSiswa.Columns.Add(bfield);
            }

            // Fill data if exists
            if (ds.Tables.Count > 1)
            {
                DataTable dataTable = ds.Tables[1];
                grdDownloadARSiswa.DataSource = dataTable;
            }
            grdDownloadARSiswa.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("bln", cboBulan.SelectedValue);
            ObjGlobal.Param.Add("thn", cboTahun.SelectedValue);
            ObjGlobal.Param.Add("noCabang", cboPerwakilanUnit.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("SPDownloadARSiswa", ObjGlobal.Param));
            ShowHideGridAndForm(true, true);
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void cboPerwakilanUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }

        protected void cboBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }

        protected void cboTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }
    }
}