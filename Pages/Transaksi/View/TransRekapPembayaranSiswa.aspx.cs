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

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransRekapPembayaranSiswa : System.Web.UI.Page
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
            //kantor pusat
            else if (ObjSys.GetstsCabang == "0" && ObjSys.GetstsPusat == "4")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name FROM vCabang) a ");
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

            cboTahun.DataSource = ObjDb.GetRows("select * from (select '0' as id, '-Pilih Tahun-' as name union select distinct year(tgl) as id, CONVERT(varchar,year(tgl)) as name from TransPiutang )x");
            cboTahun.DataValueField = "id";
            cboTahun.DataTextField = "name";
            cboTahun.DataBind();


        }

        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                "bln",
                "thn",
                "namaCabang",
                "Total",
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
                String jenisTransaksi = headerTable.Rows[i]["jenisTransaksi"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = jenisTransaksi;
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
            ObjGlobal.Param.Add("thn", cboTahun.SelectedValue);
            ObjGlobal.Param.Add("noCabang", cboCabang.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("SPviewRekappembayaranSiswa", ObjGlobal.Param));
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