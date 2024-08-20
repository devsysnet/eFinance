using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Update
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
                LoadData();
            }
        }

        //protek index yang dipakai transaksi
        protected void IndexPakai()
        {
            for (int i = 0; i < grdBarang.Rows.Count; i++)
            {
                string itemId = grdBarang.DataKeys[i].Value.ToString();
                Button btnSelectDel = (Button)grdBarang.Rows[i].FindControl("btnSelectDel");

                DataSet mySet = ObjDb.GetRows("Select top 1 nobarang from TransPR_D Where nobarang = '" + itemId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                    btnSelectDel.Enabled = false;
                else
                    btnSelectDel.Enabled = true;

            }
        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataBarang", ObjGlobal.Param);
            grdBarang.DataBind();

            IndexPakai();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdBarang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdBarang.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CloseMessage();
            this.ShowHideGridAndForm(true, false);
            LoadData();
        }

        protected void LoadDataCombo(string cbojnsBarang)
        {
            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang + "' ) a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();
        }

        protected void cbojnsBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbojnsBarang.Text == "1")
                showhidekode.Visible = true;
            else
                showhidekode.Visible = false;

            if (cbojnsBarang.Text == "5")
                showhideharga.Visible = true;
            else
                showhideharga.Visible = false;

            cboKategori.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Kategori Barang---' name union all SELECT distinct nokategori id, kategori name FROM mkategori where jns = '" + cbojnsBarang.Text + "' ) a");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

            
        }
        
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtnamaBarang.Text == "")
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
            if (valid == true)

            {
                if (ObjDb.GetRows("SELECT * FROM mBarang WHERE namaBarang = '" + txtnamaBarang.Text + "' and nobarang <> '" + hdnId.Value + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Barang sudah ada.");
                }
                else
                {

                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noBarang", hdnId.Value);
                        ObjGlobal.Param.Add("namaBarang", txtnamaBarang.Text);
                        ObjGlobal.Param.Add("jenisBrg", cbojnsBarang.Text);
                        ObjGlobal.Param.Add("kodeAset", txtKodeAsset.Text);
                        ObjGlobal.Param.Add("kategoriBarang", cboKategori.Text);
                        ObjGlobal.Param.Add("Uraian", keterangan.Text);
                       
                        if(harga.Text == null || harga.Text == "NULL" || harga.Text == "null" || harga.Text == "")
                        {
                            ObjGlobal.Param.Add("harga", "0.00");
                        }
                        else
                        {
                            ObjGlobal.Param.Add("harga", Convert.ToDecimal(harga.Text).ToString());
                        }

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
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.GetDataProcedure("SPUpdatemBarang", ObjGlobal.Param);

                        LoadData();
                        this.ShowHideGridAndForm(true, false);
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diubah.");
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

        protected void grdBarang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noBarang", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataBarangDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtKode.Text = myRow["kodeBarang"].ToString();
                    txtnamaBarang.Text = myRow["namaBarang"].ToString();
                    
                    satuan1.Text = myRow["satuan"].ToString();
                    konfersi1.Text = myRow["konfersi"].ToString();
                    satuan2.Text = myRow["satuanbesar"].ToString();
                    konfersi2.Text = myRow["konfersibesar"].ToString();
                    satuan3.Text = myRow["satuanbesar1"].ToString();
                    konfersi3.Text = myRow["konfersi1"].ToString();
                    satuan4.Text = myRow["satuanbesar2"].ToString();
                    konfersi4.Text = myRow["konfersi2"].ToString();
                    satuan5.Text = myRow["sataunbesar3"].ToString();
                    konfersi5.Text = myRow["konfersi3"].ToString();
                    harga.Text = myRow["harga"].ToString();
                    cbojnsBarang.Text = myRow["jns"].ToString();
                    LoadDataCombo(cbojnsBarang.Text);
                    cboKategori.Text = myRow["kategoriBarang"].ToString();
                    keterangan.Text = myRow["Uraian"].ToString();
                    if (cbojnsBarang.Text == "1")
                        showhidekode.Visible = true;
                    else
                        showhidekode.Visible = false;

                    if (cbojnsBarang.Text == "5")
                        showhideharga.Visible = true;
                    else
                        showhideharga.Visible = false;

                    txtKodeAsset.Text = myRow["kodeAset"].ToString();

                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("nobarang", hdnId.Value);
                    ObjDb.Delete("mBarang", ObjDb.Where);

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
    }
}