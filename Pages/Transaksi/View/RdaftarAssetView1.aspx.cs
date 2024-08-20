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
    public partial class RdaftarAssetView1 : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataUnit();
                LoadData(cboCabang.Text, cbojnsbrg.Text);

            }
        }
        protected void cboCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text, cbojnsbrg.Text);
        }
        protected void cbojnsbrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cboCabang.Text,cbojnsbrg.Text);
        }
        protected void loadDataUnit()
        {

            if (ObjSys.GetstsPusat == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (3,4) and parent = " + ObjSys.GetCabangId + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //unit
            else if (ObjSys.GetstsPusat == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id,namaCabang name FROM mcabang where nocabang='" + ObjSys.GetCabangId + "' and stscabang in(3)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            //admin Kantor Perwakilan Bogor GetstsPusat=3
            else if (ObjSys.GetstsPusat == "3")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (3,4) and parent = " + ObjSys.GetParentCabang + ") a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            else
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (select 0 as id,'---Pilih Cabang---' as name Union SELECT distinct nocabang id,namaCabang name FROM mcabang where stsCabang in (3,4)) a");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

            LoadData(cboCabang.Text, cbojnsbrg.Text);

        }
        protected void LoadData(string nocabang = "0",string jnsbrg = "0")
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", nocabang);
            ObjGlobal.Param.Add("stscabang", ObjSys.GetstsPusat);
            ObjGlobal.Param.Add("parent", ObjSys.GetParentCabang);
            ObjGlobal.Param.Add("search", txtSearchAsset.Text);
            ObjGlobal.Param.Add("jenisBrg", jnsbrg);
            grdAssetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadAssetView", ObjGlobal.Param);
            grdAssetUpdate.DataBind();

        }
        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='" + ObjSys.GetCabangId + "' ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

            cboKelompok.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kelompok---' name union all SELECT distinct nomAsset id, kelompok+'  ( '+case when jenis=1 then 'Garis Lurus' when jenis=2 then 'Saldo Menurun' end+'  )' as  name FROM mAsset ) a");
            cboKelompok.DataValueField = "id";
            cboKelompok.DataTextField = "name";
            cboKelompok.DataBind();

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi where noLokasi = '" + cboLokasi.Text + "') a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtNamaBarang.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Barang harus dipilih.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    if (cbojnsBarang.Text == "1")
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noAsset", hdnId.Value);
                        ObjGlobal.Param.Add("noBarangAsset", hdnBarang.Value);
                        ObjGlobal.Param.Add("namaAsset", txtNamaBarang.Text);
                        //ObjGlobal.Param.Add("jnsBarang", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("penggunaan", txtpenggunaan.Text);
                        ObjGlobal.Param.Add("alamat", txtalamat.Text);
                        ObjGlobal.Param.Add("kelurahan", txtkelurahan.Text);
                        ObjGlobal.Param.Add("kecamatan", txtkecamatan.Text);
                        ObjGlobal.Param.Add("kota", txtkota.Text);
                        if (hdnkatbarang.Value == "1")
                        {
                            ObjGlobal.Param.Add("status", cboStatus.Text);
                        }
                        else if (hdnkatbarang.Value == "2")
                        {
                            ObjGlobal.Param.Add("status", cbostatusB.Text);
                        }
                        else
                        {
                            ObjGlobal.Param.Add("status", "");
                        }

                        ObjGlobal.Param.Add("luas", txtLuas.Text);
                        ObjGlobal.Param.Add("tglPerolehan", dtPerolehan.Text);
                        ObjGlobal.Param.Add("nilaiPerolehan", Convert.ToDecimal(txtnilaiP.Text).ToString());
                        ObjGlobal.Param.Add("nomor", txtnomor.Text);
                        ObjGlobal.Param.Add("atasnama", txtnama.Text);
                        ObjGlobal.Param.Add("peruntukan", txtperuntukan.Text);


                        ObjGlobal.Param.Add("tgltempo", dtTanggal.Text);


                        ObjGlobal.GetDataProcedure("SPUpdateTransAssetTidakBergerak", ObjGlobal.Param);
                    }
                    else if (cbojnsBarang.Text == "2")
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noAsset", hdnId.Value);
                        ObjGlobal.Param.Add("noBarangAsset", hdnBarang.Value);
                        ObjGlobal.Param.Add("namaAsset", txtNamaBarang.Text);
                        // ObjGlobal.Param.Add("jnsBarang", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("penggunaan", txtpenggunaan1.Text);
                        ObjGlobal.Param.Add("atasnama", txtnamapemilik.Text);
                        ObjGlobal.Param.Add("jns", txtjnskendaraan.Text);
                        ObjGlobal.Param.Add("namaKendaraan", txtnamakendaraan.Text);
                        ObjGlobal.Param.Add("tglPerolehan", dtPerolehan1.Text);
                        ObjGlobal.Param.Add("tglTempo", dttempopajak.Text);
                        ObjGlobal.Param.Add("nilaiPerolehan", Convert.ToDecimal(txtnilai1.Text).ToString());

                        ObjGlobal.GetDataProcedure("SPUpdateTransAssetBergerak", ObjGlobal.Param);
                    }
                    else
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noAsset", hdnId.Value);
                        ObjGlobal.Param.Add("noBarangAsset", hdnBarang.Value);
                        ObjGlobal.Param.Add("namaAsset", txtNamaBarang.Text);
                        ObjGlobal.Param.Add("nolokasi", cboLokasi.Text);
                        ObjGlobal.Param.Add("noSubLokasi", cboSubLokasi.Text);
                        ObjGlobal.Param.Add("tglAsset", dtTanggal.Text);
                        ObjGlobal.Param.Add("nokelompok", cboKelompok.Text);
                        ObjGlobal.Param.Add("kategori", cbokategori.Text);
                        ObjGlobal.Param.Add("uraian", txtUraian.Text);
                        ObjGlobal.Param.Add("nilaiPerolehan", Convert.ToDecimal(txtNilai.Text).ToString());
                        ObjGlobal.Param.Add("nilaiBuku", Convert.ToDecimal(txtNilai.Text).ToString());
                        ObjGlobal.Param.Add("danaBOS", cboDana.Text);
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("modifiedBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("ketLainnya", txtKetLainnya.Text);
                        ObjGlobal.Param.Add("keadaanAset", cboKeadaan.Text);
                        ObjGlobal.GetDataProcedure("SPUpdateTransAsset", ObjGlobal.Param);
                    }
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    LoadData();
                    ShowHideGridAndForm(true, false);

                }
                catch (Exception ex)
                {
                    if (valid == false)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", message);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void clearData()
        {
            LoadCombo();
            txtNamaBarang.Text = "";
            cboKelompok.Text = "0";
            cboLokasi.Text = "0";
            cboSubLokasi.Text = "0";
            txtNilai.Text = "";
            txtUraian.Text = "";
            hdnBarang.Value = "";
            cboDana.Text = "";
            txtKetLainnya.Text = "";
            cboKeadaan.Text = "";
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ShowHideGridAndForm(true, false);
        }

        protected void btnBrowseAsset_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void LoadDataAsset()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            ObjGlobal.Param.Add("jns", cbojnsBarang.Text);
            grdDataAsset.DataSource = ObjGlobal.GetDataProcedure("SPLoadAsetInventori", ObjGlobal.Param);
            grdDataAsset.DataBind();
        }
        protected void btnAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void grdDataAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataAsset.SelectedIndex;
            txtNamaBarang.Text = grdDataAsset.Rows[selectedRow].Cells[2].Text;
            hdnBarang.Value = (grdDataAsset.SelectedRow.FindControl("hdnnoBarang") as HiddenField).Value;
            hdnkatbarang.Value = (grdDataAsset.SelectedRow.FindControl("hdnkategori") as HiddenField).Value;

            if (hdnkatbarang.Value == "1")
            {
                stsTanah.Visible = true;
                stsbangunan.Visible = false;

            }
            else if (hdnkatbarang.Value == "2")
            {
                stsTanah.Visible = false;
                stsbangunan.Visible = true;

            }
        }

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void btnSearchAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdAssetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAssetUpdate.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void cbojnsbarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojnsBarang.Text == "1")
            {
                divassettidakbergerak.Visible = true;
                divAssetBergerak.Visible = false;
                divAsset.Visible = false;
            }
            else if (cbojnsBarang.Text == "2")
            {
                divassettidakbergerak.Visible = false;
                divAssetBergerak.Visible = true;
                divAsset.Visible = false;
            }
            else
            {
                divassettidakbergerak.Visible = false;
                divAssetBergerak.Visible = false;
                divAsset.Visible = true;
            }
        }
        protected void grdAssetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {

                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noAset = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noAset;

                    DataSet mySet = ObjDb.GetRows("select isnull(a.noBarangAsset,0) as noBarangAsset, a.kodeAsset, a.namaAsset, a.tglAsset, " +
                        "isnull(a.nolokasi,0) as nolokasi, isnull(a.noSubLokasi,0) as noSubLokasi, isnull(a.nilaiPerolehan,0) as nilaiPerolehan, " +
                        "a.uraian, isnull(a.nokelompok,0) as nokelompok, isnull(a.norekdb,0) as norekdb, isnull(a.norekkd,0) as norekkd, a.danaBOS, a.namaBarang," +
                        "a.ketLainnya, a.keadaanAset,a.kategori,b.kategoribarang,a.atasnama,a.alamat,a.kelurahan,a.kecatamatan,b.jenisBrg,a.kota,a.penggunaan,a.luas,a.status,a.nomorids,a.peruntukan,a.tgljthtempo " +
                        "from tAsset a inner join mbarang b on a.nobrg = b.nobarang where noAset = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    hdnkatbarang.Value = myRow["kategoribarang"].ToString();
                    hdnBarang.Value = myRow["noBarangAsset"].ToString();
                    txtKode.Text = myRow["kodeAsset"].ToString();
                    txtNamaBarang.Text = myRow["namaAsset"].ToString();

                    if (myRow["jenisBrg"].ToString() == "1")
                    {
                        LoadCombo();
                        this.ShowHideGridAndForm(false, true);
                        divAsset.Visible = false;
                        divAssetBergerak.Visible = false;
                        divassettidakbergerak.Visible = true;
                        cbojnsBarang.Text = myRow["jenisBrg"].ToString();
                        dtPerolehan.Text = Convert.ToDateTime(myRow["tglAsset"]).ToString("dd-MMM-yyyy");
                        txtpenggunaan.Text = myRow["penggunaan"].ToString();
                        txtalamat.Text = myRow["alamat"].ToString();
                        txtkelurahan.Text = myRow["kelurahan"].ToString();
                        txtkecamatan.Text = myRow["kecatamatan"].ToString();
                        txtkota.Text = myRow["kota"].ToString();
                        if (myRow["kategoribarang"].ToString() == "1")
                        {
                            divtgljthtempo.Visible = true;
                            stsTanah.Visible = true;
                            stsbangunan.Visible = false;
                            cboStatus.Text = myRow["status"].ToString();
                            lblnomor.InnerText = "Sertifikat";
                        }
                        else if (myRow["kategoribarang"].ToString() == "2")
                        {
                            divtgljthtempo.Visible = false;
                            stsTanah.Visible = false;
                            stsbangunan.Visible = true;
                            cbostatusB.Text = myRow["status"].ToString();
                            lblnomor.InnerText = "IMB";
                        }
                        txtLuas.Text = myRow["luas"].ToString();
                        txtnomor.Text = myRow["nomorids"].ToString();
                        txtnama.Text = myRow["atasnama"].ToString();
                        txtperuntukan.Text = myRow["peruntukan"].ToString();
                        dttgl.Text = Convert.ToDateTime(myRow["tgljthtempo"]).ToString("dd-MMM-yyyy");
                        txtnilaiP.Text = ObjSys.IsFormatNumber(myRow["nilaiPerolehan"].ToString());
                    }
                    else if (myRow["jenisBrg"].ToString() == "2")
                    {
                        LoadCombo();
                        this.ShowHideGridAndForm(false, true);
                        divAsset.Visible = false;
                        divAssetBergerak.Visible = true;
                        divassettidakbergerak.Visible = false;
                        cbojnsBarang.Text = myRow["jenisBrg"].ToString();
                        dtPerolehan1.Text = Convert.ToDateTime(myRow["tglAsset"]).ToString("dd-MMM-yyyy");
                        txtpenggunaan1.Text = myRow["penggunaan"].ToString();
                        txtnamapemilik.Text = myRow["atasnama"].ToString();
                        txtjnskendaraan.Text = myRow["namaBarang"].ToString();
                        txtnamakendaraan.Text = myRow["nomorids"].ToString();
                        dttempopajak.Text = Convert.ToDateTime(myRow["tgljthtempo"]).ToString("dd-MMM-yyyy");
                        txtnilai1.Text = ObjSys.IsFormatNumber(myRow["nilaiPerolehan"].ToString());



                    }
                    else
                    {
                        divAsset.Visible = true;
                        divAssetBergerak.Visible = false;
                        divassettidakbergerak.Visible = false;
                        cbojnsBarang.Text = myRow["jenisBrg"].ToString();
                        dtTanggal.Text = Convert.ToDateTime(myRow["tglAsset"]).ToString("dd-MMM-yyyy");
                        cboLokasi.Text = myRow["nolokasi"].ToString();
                        cboSubLokasi.Text = myRow["noSubLokasi"].ToString();
                        txtNilai.Text = ObjSys.IsFormatNumber(myRow["nilaiPerolehan"].ToString());
                        txtUraian.Text = myRow["uraian"].ToString();
                        cboKelompok.Text = myRow["nokelompok"].ToString();
                        cboDana.Text = myRow["danaBOS"].ToString();
                        if (cboDana.Text == "Lain")
                            showhideLainnya.Visible = true;
                        else
                            showhideLainnya.Visible = false;
                        LoadCombo();
                        this.ShowHideGridAndForm(false, true);
                        txtKetLainnya.Text = myRow["ketLainnya"].ToString();
                        cbokategori.Text = myRow["kategori"].ToString();
                        if (cbokategori.Text == "Asset")
                            showhideLainnya1.Visible = true;
                        else
                            showhideLainnya1.Visible = false;
                        cboKeadaan.Text = myRow["keadaanAset"].ToString();
                    }
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noAset = grdAssetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noAset;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noAset", hdnId.Value);
                    ObjDb.Delete("tAsset", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus");
                    this.ShowHideGridAndForm(true, false);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void cboDana_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDana.Text == "Lain")
                showhideLainnya.Visible = true;
            else
                showhideLainnya.Visible = false;
        }

        protected void cboDana_SelectedIndexChangedkat(object sender, EventArgs e)
        {
            if (cbokategori.Text == "Asset")

                showhideLainnya1.Visible = true;


            if (cbokategori.Text == "Invetaris")

                showhideLainnya1.Visible = false;

        }
    }
}