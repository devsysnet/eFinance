using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransAsset : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCombo();
                showhideLainnya.Visible = false;
                showhideLainnya1.Visible = false;
                divassettidakbergerak.Visible = false;
                divAssetBergerak.Visible = false;
                divlainnya.Visible = true;
            }
        }
        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi where nocabang='"+ ObjSys.GetCabangId + "' ) a");
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

        protected void cbojnsbarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbojnsBarang.Text == "1")
            {
                divassettidakbergerak.Visible = true;
                divAssetBergerak.Visible = false;
                divlainnya.Visible = false;
            }
            else if(cbojnsBarang.Text == "2")
            {
                divassettidakbergerak.Visible = false;
                divAssetBergerak.Visible = true;
                divlainnya.Visible = false;
            }
            else
            {
                divassettidakbergerak.Visible = false;
                divAssetBergerak.Visible = false;
                divlainnya.Visible = true;
            }
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

                    //string Kode = ObjSys.GetCodeAutoNumberNew("4", Convert.ToDateTime(dtTanggal.Text).ToString("yyyy-MM-dd"));
                    if (cbojnsBarang.Text == "1")
                    {
                        ObjGlobal.Param.Clear();
                        //ObjGlobal.Param.Add("kodeAsset", Kode);
                        ObjGlobal.Param.Add("noBarangAsset", hdnBarang.Value);
                        ObjGlobal.Param.Add("namaAsset", txtNamaBarang.Text);
                        ObjGlobal.Param.Add("jnsBarang", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("penggunaan", txtpenggunaan.Text);
                        ObjGlobal.Param.Add("lokasi", "");
                        ObjGlobal.Param.Add("alamat", txtalamat.Text);
                        ObjGlobal.Param.Add("kelurahan", txtkelurahan.Text);
                        ObjGlobal.Param.Add("kecamatan", txtkecamatan.Text);
                        ObjGlobal.Param.Add("kota", txtkota.Text);
                        if(hdnkatbarang.Value == "1")
                        {
                            ObjGlobal.Param.Add("status", cboStatus.Text);
                        }
                        else if(hdnkatbarang.Value == "2")
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
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("tgltempo", dtTanggal.Text);
             
                      
                        ObjGlobal.GetDataProcedure("SPInsertTransAssetTidakBergerak", ObjGlobal.Param);
                    }
                    else if (cbojnsBarang.Text == "2")
                    {
                        ObjGlobal.Param.Clear();
                        //ObjGlobal.Param.Add("kodeAsset", Kode);
                        ObjGlobal.Param.Add("noBarangAsset", hdnBarang.Value);
                        ObjGlobal.Param.Add("namaAsset", txtNamaBarang.Text);
                        ObjGlobal.Param.Add("jnsBarang", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("penggunaan", txtpenggunaan1.Text);
                        ObjGlobal.Param.Add("atasnama", txtnamapemilik.Text);
                        ObjGlobal.Param.Add("jns", txtjnskendaraan.Text);
                        ObjGlobal.Param.Add("namaKendaraan", txtnamakendaraan.Text);
                        ObjGlobal.Param.Add("tglPerolehan", dtPerolehan1.Text);
                        ObjGlobal.Param.Add("tglTempo", dttempopajak.Text);
                        ObjGlobal.Param.Add("nilaiPerolehan", Convert.ToDecimal(txtnilai1.Text).ToString());
                        ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPInsertTransAssetBergerak", ObjGlobal.Param);
                    }
                    else
                    {
                        ObjGlobal.Param.Clear();
                        //ObjGlobal.Param.Add("kodeAsset", Kode);
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
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("ketLainnya", txtKetLainnya.Text);
                        ObjGlobal.Param.Add("keadaanAset", cboKeadaan.Text);
                        ObjGlobal.GetDataProcedure("SPInsertTransAsset", ObjGlobal.Param);
                    }
                  

                    //ObjSys.UpdateAutoNumberCodeNew("4", Convert.ToDateTime(dtTanggal.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
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
            hdnkatbarang.Value = "";

            //cbojnsBarang.Text = "";
            txtpenggunaan.Text = "";
            txtalamat.Text = "";
            txtkelurahan.Text = "";
            txtkecamatan.Text = "";
            txtkota.Text = "";

          

            txtLuas.Text = "";
            dtPerolehan.Text = "";
            txtnilaiP.Text = "";
            txtnomor.Text = "";
            txtperuntukan.Text = "";


            dtTanggal.Text = "";

            txtpenggunaan1.Text = "";
            txtnamapemilik.Text = "";
            txtjnskendaraan.Text = "";
            txtnamakendaraan.Text = "";
            dtPerolehan1.Text = "";
            dttempopajak.Text = "";
            txtnilai1.Text = "";



        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            clearData();
        }

        protected void btnBrowseAsset_Click(object sender, ImageClickEventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void LoadDataAsset()
        {
            //tampilin asset dan inventaris
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
                divtgl.Visible = true;
                lblnomor.InnerText = "Sertifikat";
            }
            else if (hdnkatbarang.Value == "2")
            {
                stsTanah.Visible = false;
                stsbangunan.Visible = true;
                divtgl.Visible = false;
                lblnomor.InnerText = "IMB";
            }      

        }

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
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

                showhideLainnya1.Visible  = true;
              

            if (cbokategori.Text == "Invetaris")

                showhideLainnya1.Visible = false;
               
        }
    }
}