using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class mBarang : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mBarang.aspx");
                loadDataCombo();
                showhidekode.Visible = false;
                showhideharga.Visible = false;
            }
        }

        private void loadDataCombo()
        {
            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang.Text + "') a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

            if (ObjSys.GetstsCabang == "2")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang where nocabang = '" + ObjSys.GetCabangId + "') a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }
            if (ObjSys.GetstsCabang == "1")
            {
                cboCabang.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct nocabang id, namaCabang name, noUrut as urutan FROM vCabang) a order by urutan");
                cboCabang.DataValueField = "id";
                cboCabang.DataTextField = "name";
                cboCabang.DataBind();
            }

        }

        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojnsBarang.Text == "1")
                showhidekode.Visible = true;
            else
                showhidekode.Visible = false;


            if (cbojnsBarang.Text == "5")
                showhideharga.Visible = true;
            else
                showhideharga.Visible = false;

            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang.Text + "') a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

         }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (namaBarang.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Barang tidak boleh kosong.");
                valid = false;
            }
            
            if (cbojnsBarang.SelectedValue == "0")
            {
                message += ObjSys.CreateMessage("Jenis Barang harus dipilih.");
                valid = false;
            }

           

            if (cboKategori.SelectedValue == "0")
            {
                message += ObjSys.CreateMessage("Kategori Barang harus dipilih.");
                valid = false;
            }

            if (cbojnsBarang.SelectedValue == "1")
            {
                if (txtKodeAsset.Text == "")
                {
                    message += ObjSys.CreateMessage("Kode Asset harus diisi.");
                    valid = false;
                }     
                else
                    valid = true;
            }

            if (satuan1.Text == "")
            {
                message += ObjSys.CreateMessage("Satuan Kecil Tidak Boleh Kosong");
                valid = false;
            }

            if (valid == true)
                
            {
                if (ObjDb.GetRows("SELECT * FROM mBarang WHERE namaBarang = '" + namaBarang.Text + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Barang sudah ada.");
                }
                else
                {

                    try
                    {

                        ObjGlobal.Param.Clear();
                        decimal harga = 0;

                        if (cbojnsBarang.Text == "5")
                        { 
                            harga = Convert.ToDecimal(hargajual.Text);
                        }
                        else 
                           {
                                harga = 0;
                            }
                        

                        ObjGlobal.Param.Add("namaBarang", namaBarang.Text);
                        ObjGlobal.Param.Add("jenisBrg", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("kodeAset", txtKodeAsset.Text);
                        ObjGlobal.Param.Add("kategoriBarang", cboKategori.Text);
                        ObjGlobal.Param.Add("satuan", satuan1.Text);
                        ObjGlobal.Param.Add("konfersi", Convert.ToDecimal(konfersi1.Text).ToString());
                        ObjGlobal.Param.Add("satuanbesar", satuan2.Text);
                        ObjGlobal.Param.Add("konfersibesar", Convert.ToDecimal(konfersi2.Text).ToString());
                        ObjGlobal.Param.Add("satuanbesar1", satuan3.Text);
                        ObjGlobal.Param.Add("konfersi1", Convert.ToDecimal(konfersi3.Text).ToString());
                        ObjGlobal.Param.Add("satuanbesar2", satuan4.Text);
                        ObjGlobal.Param.Add("konfersi2", Convert.ToDecimal(konfersi4.Text).ToString());
                        ObjGlobal.Param.Add("satuanbesar3", satuan5.Text);
                        ObjGlobal.Param.Add("konfersi3", Convert.ToDecimal(konfersi5.Text).ToString());
                        ObjGlobal.Param.Add("Uraian", keterangan.Text);
                        ObjGlobal.Param.Add("harga", harga.ToString());             
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("nocabang", cboCabang.Text);
                        ObjGlobal.GetDataProcedure("SPInsertmBarang", ObjGlobal.Param);

                        ClearData();
                        
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil disimpan.");
                    }
                    catch (Exception ex)
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            ClearData();
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
            namaBarang.Text = "";
            keterangan.Text = "";
            cbojnsBarang.Text = "0";
            cboKategori.Text = "0";
            satuan1.Text = "";
            satuan2.Text = "";
            satuan3.Text = "";
            satuan4.Text = "";
            satuan5.Text = "";
            konfersi1.Text = "";
            konfersi2.Text = "";
            konfersi3.Text = "";
            konfersi4.Text = "";
            konfersi5.Text = "";
            hargajual.Text = "0.00";
            showhidekode.Visible = false;
            showhideharga.Visible = false;
        }
    }
}