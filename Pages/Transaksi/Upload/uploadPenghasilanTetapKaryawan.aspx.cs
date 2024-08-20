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


namespace eFinance.Pages.Transaksi.Upload
{
    public partial class uploadPenghasilanTetapKaryawan : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                ShowHideGridAndForm(false, false);
                showhidefile.Visible = false;

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

        }

        /**
         * Contoh logic upload mstgaji h & d
         */
        protected void UploadExcel()
        {
            // Read kolom dan row excel nya
            // Kolom excel: noKaryawan, namaKaryawan, KomponenGaji1, KomponenGaji2, KomponenGaji3
            List<string> columns = new List<string>(); // masukkan nama2 kolom ke variable columns
            // Karena komponen gaji dimulai dari index ke 2, maka buang kolom index ke 0 dan 1 (noKar dan nama)
            columns.RemoveAt(0);
            columns.RemoveAt(1);
            // Sekarang columns berisi nama komponen gaji

            List<string> values = new List<string>();

            // Panggil SP untuk insert MstGaji_H, dapatkan nomor gaji dari MstGaji_H yang barusan di insert
            //var noGajiKaryawan = InsertGajiH(); // noGajiKaryawan diisi dengan no   ---- nanti dibuka lagi

            List<DataRow> rows = new List<DataRow>(); //dapatkan value dari setiap row
            // Loop tiap row
            foreach (DataRow row in rows)
            {
                // Loop tiap komponen gaji
                foreach (string namaKomponenGaji in columns)
                {
                    // Panggil SP untuk insert MstGaji_D
                    using (SqlConnection con = new SqlConnection(ObjDb.ConDb()))
                    {
                        using (SqlCommand cmd = new SqlCommand("")) //SP untuk insert MstGaji_D
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@noGajiKaryawan", noGajiKaryawan); ini nanti dibuka lagi
                            cmd.Parameters.AddWithValue("@namaKomponenGaji", namaKomponenGaji);
                            cmd.Parameters.AddWithValue("@nilai", row[namaKomponenGaji]);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }

                    }
                }
            }
        }

        protected void tabelDinamis(DataSet ds)
        {
            // Initialize columns
            List<String> datacolumns = new List<String> {
                "noKaryawan",
                "nama"
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
                String komponengaji = headerTable.Rows[i]["komponengaji"].ToString();
                BoundField bfield = new BoundField();
                bfield.HeaderText = komponengaji;
                bfield.DataField = komponengaji;
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
        protected void cboAksi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAksi.Text == "Import")
            {
                showhidefile.Visible = true;
                tabGrid.Visible = false;
            }
            else if (cboAksi.Text == "Export")
            {
                showhidefile.Visible = false;
                tabGrid.Visible = true;
            }
            else
            {
                showhidefile.Visible = false;
                tabGrid.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ObjGlobal.Param.Clear();
    
            ObjGlobal.Param.Add("noCabang", cboPerwakilanUnit.Text);
            tabelDinamis(ObjGlobal.GetDataProcedure("spdownloadkomponengaji", ObjGlobal.Param));
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