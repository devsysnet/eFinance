using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransPR : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearData();
                cekKategori_Usaha();
            }
        }

        protected void cekKategori_Usaha()
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                divDanaBOSH.Visible = true;

                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty";
                        //grdDetail.Columns[4].Visible = true;
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[8].Visible = true;

                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "2")
                    {
                        //grdDetail.Columns[1].HeaderText = "Akun";
                        //grdDetail.Columns[2].HeaderText = "Nama Akun";
                        //grdDetail.Columns[3].HeaderText = "Nominal";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Akun";
                        grdDetail.Columns[2].HeaderText = "Nama Akun";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Nominal";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        //grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        //grdDetail.Columns[3].HeaderText = "Biaya";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Biaya";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = true;

                    }
                    else
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = true;
                    }
                }
            }
            else
            {
                divDanaBOSH.Visible = false;

                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty";
                        //grdDetail.Columns[4].Visible = true;
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[8].Visible = false;

                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "2")
                    {
                        //grdDetail.Columns[1].HeaderText = "Akun";
                        //grdDetail.Columns[2].HeaderText = "Nama Akun";
                        //grdDetail.Columns[3].HeaderText = "Nominal";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Akun";
                        grdDetail.Columns[2].HeaderText = "Nama Akun";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Nominal";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        //grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        //grdDetail.Columns[3].HeaderText = "Biaya";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Biaya";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = false;
                    }
                    else
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = false;
                    }
                }
            }

        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("txtKode", typeof(string)));
            dt.Columns.Add(new DataColumn("lblNama", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilaiBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("lblsatuanBesar", typeof(string)));
            dt.Columns.Add(new DataColumn("txtNilai", typeof(string)));
            //dt.Columns.Add(new DataColumn("cboSatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("lblsatuan", typeof(string)));
            dt.Columns.Add(new DataColumn("txtBudgetPR", typeof(string)));
            dt.Columns.Add(new DataColumn("Keterangan", typeof(string)));
            dt.Columns.Add(new DataColumn("cboDana", typeof(string)));
            dt.Columns.Add(new DataColumn("txtHargaStn", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["txtKode"] = string.Empty;
            dr["lblNama"] = string.Empty;
            dr["txtNilaiBesar"] = string.Empty;
            dr["lblsatuanBesar"] = string.Empty;
            dr["txtNilai"] = string.Empty;
            //dr["cboSatuan"] = string.Empty;
            dr["lblsatuan"] = string.Empty;
            dr["txtBudgetPR"] = string.Empty;
            dr["Keterangan"] = string.Empty;
            dr["cboDana"] = string.Empty;
            dr["txtHargaStn"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtKode = (TextBox)grdDetail.Rows[i].FindControl("txtKode");
                        Label lblNama = (Label)grdDetail.Rows[i].FindControl("lblNama");
                        TextBox txtNilaiBesar = (TextBox)grdDetail.Rows[i].FindControl("txtNilaiBesar");
                        Label lblsatuanBesar = (Label)grdDetail.Rows[i].FindControl("lblsatuanBesar");
                        TextBox txtNilai = (TextBox)grdDetail.Rows[i].FindControl("txtNilai");
                        //DropDownList cboSatuan = (DropDownList)grdDetail.Rows[i].FindControl("cboSatuan");
                        Label lblsatuan = (Label)grdDetail.Rows[i].FindControl("lblsatuan");
                        TextBox txtBudgetPR = (TextBox)grdDetail.Rows[i].FindControl("txtBudgetPR");
                        TextBox Keterangan = (TextBox)grdDetail.Rows[i].FindControl("Keterangan");
                        DropDownList cboDana = (DropDownList)grdDetail.Rows[i].FindControl("cboDana");
                        TextBox txtHargaStn = (TextBox)grdDetail.Rows[i].FindControl("txtHargaStn");

                        txtKode.Text = dt.Rows[i]["txtKode"].ToString();
                        lblNama.Text = dt.Rows[i]["lblNama"].ToString();
                        txtNilaiBesar.Text = dt.Rows[i]["txtNilaiBesar"].ToString();
                        lblsatuanBesar.Text = dt.Rows[i]["lblsatuanBesar"].ToString();
                        txtNilai.Text = dt.Rows[i]["txtNilai"].ToString();
                        //cboSatuan.Text = dt.Rows[i]["cboSatuan"].ToString();
                        lblsatuan.Text = dt.Rows[i]["lblsatuan"].ToString();
                        txtBudgetPR.Text = dt.Rows[i]["txtBudgetPR"].ToString();
                        Keterangan.Text = dt.Rows[i]["Keterangan"].ToString();
                        cboDana.Text = dt.Rows[i]["cboDana"].ToString();
                        txtHargaStn.Text = dt.Rows[i]["txtHargaStn"].ToString();
                        //loadDataCombo(cboSatuan);
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtKode = (TextBox)grdDetail.Rows[i].FindControl("txtKode");
                        Label lblNama = (Label)grdDetail.Rows[i].FindControl("lblNama");
                        TextBox txtNilaiBesar = (TextBox)grdDetail.Rows[i].FindControl("txtNilaiBesar");
                        Label lblsatuanBesar = (Label)grdDetail.Rows[i].FindControl("lblsatuanBesar");
                        TextBox txtNilai = (TextBox)grdDetail.Rows[i].FindControl("txtNilai");
                        //DropDownList cboSatuan = (DropDownList)grdDetail.Rows[i].FindControl("cboSatuan");
                        Label lblsatuan = (Label)grdDetail.Rows[i].FindControl("lblsatuan");
                        TextBox txtBudgetPR = (TextBox)grdDetail.Rows[i].FindControl("txtBudgetPR");
                        TextBox Keterangan = (TextBox)grdDetail.Rows[i].FindControl("Keterangan");
                        DropDownList cboDana = (DropDownList)grdDetail.Rows[i].FindControl("cboDana");
                        TextBox txtHargaStn = (TextBox)grdDetail.Rows[i].FindControl("txtHargaStn");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["txtKode"] = txtKode.Text;
                        dtCurrentTable.Rows[i]["lblNama"] = lblNama.Text;
                        dtCurrentTable.Rows[i]["txtNilaiBesar"] = txtNilaiBesar.Text;
                        dtCurrentTable.Rows[i]["lblsatuanBesar"] = lblsatuanBesar.Text;
                        dtCurrentTable.Rows[i]["txtNilai"] = txtNilai.Text;
                        //dtCurrentTable.Rows[i]["cboSatuan"] = cboSatuan.Text;
                        dtCurrentTable.Rows[i]["lblsatuan"] = lblsatuan.Text;
                        dtCurrentTable.Rows[i]["txtBudgetPR"] = txtBudgetPR.Text;
                        dtCurrentTable.Rows[i]["Keterangan"] = Keterangan.Text;
                        dtCurrentTable.Rows[i]["cboDana"] = Keterangan.Text;
                        dtCurrentTable.Rows[i]["txtHargaStn"] = txtHargaStn.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdDetail.DataSource = dtCurrentTable;
                    grdDetail.DataBind();
                }
            }
            SetPreviousData();
        }
        //private void loadDataCombo(DropDownList cboSatuan)
        //{
        //    cboSatuan.DataSource = ObjDb.GetRows("select '' as id, '--Pilih Satuan--' mesurement union all select mesurement as id, mesurement from mMesurement");
        //    cboSatuan.DataValueField = "id";
        //    cboSatuan.DataTextField = "mesurement";
        //    cboSatuan.DataBind();

        //}

        protected void loadDataComboProject()
        {
            cboProject.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Project--' name union all select noProject as no, noKontrak+' - '+Project as name from mProject where stsProject = '1' and nocabang='" + ObjSys.GetCabangId + "'");
            cboProject.DataValueField = "no";
            cboProject.DataTextField = "name";
            cboProject.DataBind();
        }

        protected void loadDataComboKegiatan()
        {
            cbokegiatan.DataSource = ObjDb.GetRows("select '0' as no, '--Pilih Kegiatan--' name union all select noMkegiatan as no, namaKegiatan as name from mJenisKegiatan where sts = '1' and nocabang='" + ObjSys.GetCabangId + "'");
            cbokegiatan.DataValueField = "no";
            cbokegiatan.DataTextField = "name";
            cbokegiatan.DataBind();
        }

        protected void LoadDataPeminta()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select * from mstkaryawan where nocabang='" + ObjSys.GetCabangId + "' and (idPeg like '%" + txtSearchMinta.Text.Trim() + "%' or nama like '%" + txtSearchMinta.Text.Trim() + "%')");
            grdDataPeminta.DataSource = myData;
            grdDataPeminta.DataBind();
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
        protected void ClearData()
        {
            dtDate.Text = "";
            dtDate1.Text = "";
            hdnNoUser.Value = "";
            txtNamaPeminta.Text = "";
            txtKeterangan.Text = "";
            cboTransaction.SelectedValue = "0";
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }
            txtSubTotal.Text = "0.00";
            cboKategoriDana.Text = "";
            cboDanaH.Text = "Tidak";
            showHideKatDana(false);
            showHideProject(false);
            showHideKegiatan(false);
        }

        protected void btnBrowsePeminta_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void grdDataPeminta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataPeminta.SelectedIndex;
            txtNamaPeminta.Text = grdDataPeminta.Rows[selectedRow].Cells[2].Text;
            hdnNoUser.Value = (grdDataPeminta.SelectedRow.FindControl("hdnNoUserD") as HiddenField).Value;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";
            int cekData = 0;

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Permintaan harus di isi.");
                valid = false;
            }
            else if (cboTransaction.Text == "0")
            {
                message = ObjSys.CreateMessage("Jenis Permintaan harus di pilih.");
                valid = false;
            }
            else if (cboTransaction.Text == "2")
            {
                if (cboKategoriDana.Text == "")
                {
                    message = ObjSys.CreateMessage("Kategori Dana harus di pilih.");
                    valid = false;
                }
                else if (cboKategoriDana.Text == "Project" && cboProject.Text == "0")
                {
                    message = ObjSys.CreateMessage("Project harus di pilih.");
                    valid = false;
                }
            }
            else if (txtNamaPeminta.Text == "")
            {
                message = ObjSys.CreateMessage("Peminta harus di pilih.");
                valid = false;
            }

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                TextBox txtNilai = (TextBox)grdDetail.Rows[i].FindControl("txtNilai");
                TextBox txtKode = (TextBox)grdDetail.Rows[i].FindControl("txtKode");
                //DropDownList cboSatuan = (DropDownList)grdDetail.Rows[i].FindControl("cboSatuan");
                if (txtKode.Text != "" || txtNilai.Text != "")
                {
                    cekData++;
                }
            }

            if (cekData == 0)
            {
                message = ObjSys.CreateMessage("Tidak ada data yang dipilih.");
                valid = false;
            }

            try
            {

                if (valid == true)
                {
                    if (cekData > 0)
                    {
                        string Kode = ObjSys.GetCodeAutoNumberNew("8", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("KodePR", Kode);
                        ObjDb.Data.Add("tglPR", dtDate.Text);
                        ObjDb.Data.Add("tglpakai", dtDate1.Text);
                        ObjDb.Data.Add("peminta", txtNamaPeminta.Text);
                        ObjDb.Data.Add("jenis", cboTransaction.Text);

                        if (cboKategoriDana.Text != "")
                            ObjDb.Data.Add("kategoriRutin", cboKategoriDana.Text);
                        else
                            ObjDb.Data.Add("kategoriRutin", "");

                        if (cboKategoriDana.Text == "Project" && cboProject.Text != "0")
                            ObjDb.Data.Add("noProject", cboProject.Text);

                        if (cboKategoriDana.Text == "Kegiatan" && cbokegiatan.Text != "0")
                            ObjDb.Data.Add("nokegiatan", cbokegiatan.Text);

                        ObjDb.Data.Add("uraian", txtKeterangan.Text);
                        ObjDb.Data.Add("subtotal", Convert.ToDecimal(txtSubTotal.Text).ToString());
                        ObjDb.Data.Add("saldopr", Convert.ToDecimal(txtSubTotal.Text).ToString());
                        ObjDb.Data.Add("saldoPrpst", Convert.ToDecimal(txtSubTotal.Text).ToString());
                        ObjDb.Data.Add("danaBOS", cboDanaH.Text);
                        ObjDb.Data.Add("stsApv", "0");
                        ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createddate", ObjSys.GetNow);
                        ObjDb.Insert("transPR_H", ObjDb.Data);

                        ObjSys.UpdateAutoNumberCodeNew("8", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                        DataSet mySetH = ObjDb.GetRows("Select * from transPR_H Where KodePR = '" + Kode + "'");
                        if (mySetH.Tables[0].Rows.Count > 0)
                        {
                            DataRow myRowH = mySetH.Tables[0].Rows[0];
                            string noPR = myRowH["noPR"].ToString();

                            for (int i = 0; i < grdDetail.Rows.Count; i++)
                            {
                                TextBox txtKode = (TextBox)grdDetail.Rows[i].FindControl("txtKode");
                                TextBox txtNilaiBesar = (TextBox)grdDetail.Rows[i].FindControl("txtNilaiBesar");
                                Label lblsatuanBesar = (Label)grdDetail.Rows[i].FindControl("lblsatuanBesar");
                                TextBox txtNilai = (TextBox)grdDetail.Rows[i].FindControl("txtNilai");
                                //DropDownList cboSatuan = (DropDownList)grdDetail.Rows[i].FindControl("cboSatuan");
                                Label lblsatuan = (Label)grdDetail.Rows[i].FindControl("lblsatuan");
                                TextBox txtBudgetPR = (TextBox)grdDetail.Rows[i].FindControl("txtBudgetPR");
                                DropDownList cboDana = (DropDownList)grdDetail.Rows[i].FindControl("cboDana");
                                TextBox txtHargaStn = (TextBox)grdDetail.Rows[i].FindControl("txtHargaStn");
                                TextBox Keterangan = (TextBox)grdDetail.Rows[i].FindControl("Keterangan");
                                HiddenField hdnKonversiGrid = (HiddenField)grdDetail.Rows[i].FindControl("hdnKonversiGrid");

                                if (txtKode.Text != "" && txtNilai.Text != "")
                                {
                                    ObjGlobal.Param.Clear();
                                    ObjGlobal.Param.Add("noPR", noPR);
                                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                                    {
                                        DataSet mySetH2 = ObjDb.GetRows("Select nobarang from mbarang Where Kodebarang = '" + txtKode.Text + "'");
                                        if (mySetH2.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                                            string nobarang = myRowH2["nobarang"].ToString();
                                            ObjGlobal.Param.Add("nobarang", nobarang);
                                        }
                                        ObjGlobal.Param.Add("norek", "0");
                                        ObjGlobal.Param.Add("jnstrs", cboTransaction.Text);
                                        ObjGlobal.Param.Add("qtyBesar", Convert.ToDecimal(txtNilaiBesar.Text).ToString());
                                        ObjGlobal.Param.Add("satbesar", lblsatuanBesar.Text);
                                        ObjGlobal.Param.Add("hargaSatuanPR", Convert.ToDecimal(txtHargaStn.Text).ToString());
                                        ObjGlobal.Param.Add("qty", Convert.ToDecimal(txtNilai.Text).ToString());
                                        //ObjDb.Data.Add("satuan", cboSatuan.Text);
                                        //decimal qtyTotal = (Convert.ToDecimal(txtNilaiBesar.Text) * Convert.ToDecimal(hdnKonversiGrid.Value)) + Convert.ToDecimal(txtNilai.Text);
                                        ObjGlobal.Param.Add("satuan", lblsatuan.Text);
                                        ObjGlobal.Param.Add("konversi", Convert.ToDecimal(hdnKonversiGrid.Value).ToString());
                                        ObjGlobal.Param.Add("qtyTotal", "0");
                                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtBudgetPR.Text).ToString());
                                    }
                                    else if (cboTransaction.SelectedValue == "3")
                                    {
                                        DataSet mySetH2 = ObjDb.GetRows("Select nobarang from mbarang Where Kodebarang = '" + txtKode.Text + "'");
                                        if (mySetH2.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                                            string nobarang = myRowH2["nobarang"].ToString();
                                            ObjGlobal.Param.Add("nobarang", nobarang);
                                        }
                                        ObjGlobal.Param.Add("jnstrs", cboTransaction.Text);
                                        ObjGlobal.Param.Add("qtyBesar", "0");
                                        ObjGlobal.Param.Add("satbesar", "");
                                        ObjGlobal.Param.Add("hargaSatuanPR", "0");
                                        ObjGlobal.Param.Add("qty", Convert.ToDecimal(txtNilai.Text).ToString());
                                        ObjGlobal.Param.Add("satuan", lblsatuan.Text);
                                        ObjGlobal.Param.Add("konversi", "1");
                                        ObjGlobal.Param.Add("qtyTotal", Convert.ToDecimal(txtNilai.Text).ToString());
                                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                                    }
                                    else if (cboTransaction.SelectedValue == "2" && cboKategoriDana.SelectedValue != "Kegiatan")
                                     
                                    {
                                        DataSet mySetH2 = ObjDb.GetRows("Select norek from mrekening Where kdrek = '" + txtKode.Text + "'");
                                        if (mySetH2.Tables[0].Rows.Count > 0)
                                            ObjGlobal.Param.Add("nobarang", "0");
                                        {
                                            DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                                            string norek = myRowH2["norek"].ToString();
                                            ObjGlobal.Param.Add("norek", norek);
                                        }
                                        ObjGlobal.Param.Add("jnstrs", cboTransaction.Text);
                                        ObjGlobal.Param.Add("qtyBesar", "0");
                                        ObjGlobal.Param.Add("satbesar", "");
                                        ObjGlobal.Param.Add("hargaSatuanPR", "0");
                                        ObjGlobal.Param.Add("qty", "0");
                                        ObjGlobal.Param.Add("satuan", "");
                                        ObjGlobal.Param.Add("konversi", "1");
                                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                                        ObjGlobal.Param.Add("qtyTotal", "0");
                                    }

                                    else if (cboTransaction.SelectedValue == "2" && cboKategoriDana.SelectedValue == "Kegiatan")
                                    {
                                        DataSet mySetH2 = ObjDb.GetRows("Select noMkegiatanD from mJenisKegiatanD Where kodekegiatan = '" + txtKode.Text + "'");
                                        if (mySetH2.Tables[0].Rows.Count > 0)
                                            ObjGlobal.Param.Add("nobarang", "0");
                                        {
                                            DataRow myRowH2 = mySetH2.Tables[0].Rows[0];
                                            string noMkegiatanD = myRowH2["noMkegiatanD"].ToString();
                                            ObjGlobal.Param.Add("noMkegiatanD", noMkegiatanD);
                                        }
                                        ObjGlobal.Param.Add("jnstrs", cboTransaction.Text);
                                        ObjGlobal.Param.Add("qtyBesar", "0");
                                        ObjGlobal.Param.Add("satbesar", "");
                                        ObjGlobal.Param.Add("hargaSatuanPR", "0");
                                        ObjGlobal.Param.Add("qty", "0");
                                        ObjGlobal.Param.Add("satuan", "");
                                        ObjGlobal.Param.Add("konversi", "1");
                                        ObjGlobal.Param.Add("nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                                        ObjGlobal.Param.Add("qtyTotal", "0");
                                    }


                                    ObjGlobal.Param.Add("keterangan", Keterangan.Text);
                                    ObjGlobal.Param.Add("ststerima", "0");
                                    ObjGlobal.Param.Add("sts", "0");
                                    ObjGlobal.Param.Add("danaBOS", cboDana.Text);
                                    ObjGlobal.GetDataProcedure("SPinsertTransPR_D", ObjGlobal.Param);
                                    //ObjDb.Insert("TransPR_D", ObjDb.Data);
                                }

                            }
                        }

                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan");
                        ClearData();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }

        }

        protected void LoadDataBiaya()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("transaksi", cboTransaction.Text);
            ObjGlobal.Param.Add("kategori", cboKategoriDana.Text);
            ObjGlobal.Param.Add("Dana", cboDanaH.Text);
            ObjGlobal.Param.Add("nokegiatan", cbokegiatan.Text);
            grdDataBiaya.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBiaya", ObjGlobal.Param);
            grdDataBiaya.DataBind();

        }

        protected void btnAddrow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void grdDataBiaya_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;
                int rowIndexHdn = Convert.ToInt32(txtHdnPopup.Value);
                int rowIndex = grdDataBiaya.SelectedRow.RowIndex;

                string Nama = "", Kode = "", Satuan = "", SatuanBesar = "", Konversi = "";
                Nama = (grdDataBiaya.SelectedRow.FindControl("Nama") as Label).Text;
                Kode = (grdDataBiaya.SelectedRow.FindControl("Kode") as Label).Text;
                SatuanBesar = (grdDataBiaya.SelectedRow.FindControl("hdnSatuanBesar") as HiddenField).Value;
                Satuan = (grdDataBiaya.SelectedRow.FindControl("hdnSatuan") as HiddenField).Value;
                Konversi = (grdDataBiaya.SelectedRow.FindControl("hdnkonfersi") as HiddenField).Value;

                TextBox txtKode = (TextBox)grdDetail.Rows[rowIndexHdn].FindControl("txtKode");
                TextBox txtNilaiBesar = (TextBox)grdDetail.Rows[rowIndexHdn].FindControl("txtNilaiBesar");
                TextBox txtNilai = (TextBox)grdDetail.Rows[rowIndexHdn].FindControl("txtNilai");
                //DropDownList cbosatuan = (DropDownList)grdDetail.Rows[rowIndexHdn].FindControl("cbosatuan");
                Label lblsatuanBesar = (Label)grdDetail.Rows[rowIndexHdn].FindControl("lblsatuanBesar");
                Label lblsatuan = (Label)grdDetail.Rows[rowIndexHdn].FindControl("lblsatuan");
                Label lblNama = (Label)grdDetail.Rows[rowIndexHdn].FindControl("lblNama");
                HiddenField hdnKonversiGrid = (HiddenField)grdDetail.Rows[rowIndexHdn].FindControl("hdnKonversiGrid");
                if (valid == true)
                {
                    dlgAddData.Hide();
                    txtKode.Text = Kode;
                    lblNama.Text = Nama;
                    txtNilaiBesar.Text = "0";
                    txtNilai.Text = "0";
                    lblsatuanBesar.Text = SatuanBesar;
                    lblsatuan.Text = Satuan;
                    hdnKonversiGrid.Value = Konversi;
                    if (SatuanBesar == "")
                        txtNilaiBesar.Enabled = false;
                    else
                        txtNilaiBesar.Enabled = true;
                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgAddData.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ClearData();
        }

        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();

            if (cboTransaction.SelectedValue == "2")
                showHideKatDana(true);
            else
            {
                showHideKatDana(false);
                showHideProject(false);
                showHideKegiatan(false);
            }


            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty";
                        //grdDetail.Columns[4].Visible = true;
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[8].Visible = true;

                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "2")
                    {
                        //grdDetail.Columns[1].HeaderText = "Akun";
                        //grdDetail.Columns[2].HeaderText = "Nama Akun";
                        //grdDetail.Columns[3].HeaderText = "Nominal";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Akun";
                        grdDetail.Columns[2].HeaderText = "Nama Akun";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Nominal";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = true;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        //grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        //grdDetail.Columns[3].HeaderText = "Biaya";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = true;

                        grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Biaya";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty";
                        //grdDetail.Columns[4].Visible = true;
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[8].Visible = false;

                        //grdDetail.Columns[1].HeaderText = "Kode Barang";
                        //grdDetail.Columns[2].HeaderText = "Nama Barang";
                        //grdDetail.Columns[3].HeaderText = "Qty Besar";
                        //grdDetail.Columns[3].Visible = false;
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].HeaderText = "Qty";
                        //grdDetail.Columns[5].Visible = true;
                        //grdDetail.Columns[6].Visible = true;
                        //grdDetail.Columns[7].Visible = true;
                        //grdDetail.Columns[8].Visible = true;
                        //grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        //grdDetail.Columns[10].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Kode Barang";
                        grdDetail.Columns[2].HeaderText = "Nama Barang";
                        grdDetail.Columns[3].HeaderText = "Qty Besar";
                        grdDetail.Columns[3].Visible = true;
                        grdDetail.Columns[4].Visible = true;
                        grdDetail.Columns[5].HeaderText = "Qty Kecil";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = true;
                        grdDetail.Columns[7].Visible = true;
                        grdDetail.Columns[8].Visible = true;
                        grdDetail.Columns[9].HeaderText = "Spesifikasi";
                        grdDetail.Columns[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "2")
                    {
                        //grdDetail.Columns[1].HeaderText = "Akun";
                        //grdDetail.Columns[2].HeaderText = "Nama Akun";
                        //grdDetail.Columns[3].HeaderText = "Nominal";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Akun";
                        grdDetail.Columns[2].HeaderText = "Nama Akun";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Nominal";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = false;
                    }
                    else if (cboTransaction.SelectedValue == "3")
                    {
                        //grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        //grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        //grdDetail.Columns[3].HeaderText = "Biaya";
                        //grdDetail.Columns[4].Visible = false;
                        //grdDetail.Columns[5].Visible = false;
                        //grdDetail.Columns[6].Visible = false;
                        //grdDetail.Columns[7].HeaderText = "Keterangan";
                        //grdDetail.Columns[8].Visible = false;

                        grdDetail.Columns[1].HeaderText = "Kode Jasa";
                        grdDetail.Columns[2].HeaderText = "Nama Jasa";
                        grdDetail.Columns[3].Visible = false;
                        grdDetail.Columns[4].Visible = false;
                        grdDetail.Columns[5].HeaderText = "Biaya";
                        grdDetail.Columns[5].Visible = true;
                        grdDetail.Columns[6].Visible = false;
                        grdDetail.Columns[7].Visible = false;
                        grdDetail.Columns[8].Visible = false;
                        grdDetail.Columns[9].HeaderText = "Keterangan";
                        grdDetail.Columns[10].Visible = false;

                    }
                }
            }

            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }
            txtSubTotal.Text = "0.00";

            loadDataComboProject();
            loadDataComboKegiatan();
            cboDanaH.Text = "Tidak";
        }

        protected void showHideKatDana(bool DivKatdana)
        {
            showKatDana.Visible = DivKatdana;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataBiaya();
            dlgAddData.PopupControlID = "panelAddDataBiaya";
            dlgAddData.Show();
        }

        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void grdDataBiaya_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (cboTransaction.SelectedValue == "1" || cboTransaction.SelectedValue == "4" || cboTransaction.SelectedValue == "5")
                {
                    e.Row.Cells[1].Text = "Kode Barang";
                    e.Row.Cells[2].Text = "Nama Barang";
                }
                else if (cboTransaction.SelectedValue == "2")
                {
                    e.Row.Cells[1].Text = "Akun";
                    e.Row.Cells[2].Text = "Nama Akun";
                }
                else if (cboTransaction.SelectedValue == "3")
                {
                    e.Row.Cells[1].Text = "Kode Jasa";
                    e.Row.Cells[2].Text = "Nama Jasa";
                }
            }
        }

        protected void cboKategoriDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();

            if (cboKategoriDana.SelectedValue == "Project")
                showHideProject(true);
            else
                showHideProject(false);


            if (cboKategoriDana.SelectedValue == "Kegiatan")
                showHideKegiatan(true);
            else
                showHideKegiatan(false);



            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }
        }

        protected void showHideProject(bool DivProject)
        {
            showProject.Visible = DivProject;
        }


        protected void showHideKegiatan(bool DivKegiatan)
        {
            showKegiatan.Visible = DivKegiatan;
        }


        protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                txtHdnPopup.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    if (cboTransaction.SelectedValue == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Jenis Permintaan harus dipilih!.");
                    }
                    else if (cboTransaction.SelectedValue == "2" && cboKategoriDana.SelectedValue == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Kategori Dana harus dipilih!.");
                    }
                    else
                    {
                        txtSearch.Text = "";
                        LoadDataBiaya();
                        dlgAddData.PopupControlID = "panelAddDataBiaya";
                        dlgAddData.Show();
                    }
                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtKode = (TextBox)grdDetail.Rows[rowIndex].FindControl("txtKode");
                    Label lblNama = (Label)grdDetail.Rows[rowIndex].FindControl("lblNama");
                    TextBox txtNilaiBesar = (TextBox)grdDetail.Rows[rowIndex].FindControl("txtNilaiBesar");
                    Label lblsatuanBesar = (Label)grdDetail.Rows[rowIndex].FindControl("lblsatuanBesar");
                    TextBox txtNilai = (TextBox)grdDetail.Rows[rowIndex].FindControl("txtNilai");
                    //DropDownList cboSatuan = (DropDownList)grdDetail.Rows[rowIndex].FindControl("cboSatuan");
                    Label lblsatuan = (Label)grdDetail.Rows[rowIndex].FindControl("lblsatuan");
                    TextBox txtHargaStn = (TextBox)grdDetail.Rows[rowIndex].FindControl("txtHargaStn");
                    TextBox txtBudgetPR = (TextBox)grdDetail.Rows[rowIndex].FindControl("txtBudgetPR");
                    TextBox Keterangan = (TextBox)grdDetail.Rows[rowIndex].FindControl("Keterangan");
                    DropDownList cboDana = (DropDownList)grdDetail.Rows[rowIndex].FindControl("cboDana");
                    HiddenField hdnKonversiGrid = (HiddenField)grdDetail.Rows[rowIndex].FindControl("hdnKonversiGrid");

                    txtKode.Text = "";
                    lblNama.Text = "";
                    txtNilaiBesar.Text = "";
                    lblsatuanBesar.Text = "";
                    txtNilai.Text = "";
                    //cboSatuan.Text = "";
                    lblsatuan.Text = "";
                    txtHargaStn.Text = "";
                    txtBudgetPR.Text = "";
                    Keterangan.Text = "";
                    cboDana.Text = "Tidak";
                    hdnKonversiGrid.Value = "";

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);

                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void cboDanaH_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseMessage();
            SetInitialRow();
            for (int i = 1; i < 3; i++)
            {
                AddNewRow();
            }

            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                DropDownList cboDanaX = (DropDownList)grdDetail.Rows[i].FindControl("cboDana");
                cboDanaX.Text = cboDanaH.Text;
            }
        }

        protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Visible = true;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[10].Visible = true;
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[10].Visible = false;
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[10].Visible = false;
                }
            }
        }
    }
}