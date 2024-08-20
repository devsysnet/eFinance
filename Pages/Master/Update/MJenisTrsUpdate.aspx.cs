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
    public partial class MJenisTrsUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                LoadDataCombo();
                this.ShowHideGridAndForm(true, false);
                cekKategori_Usaha();
            }
        }

      

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdBarang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatajenistrs", ObjGlobal.Param);
            grdBarang.DataBind();

            IndexPakai();
        }


        protected void IndexPakai()
        {

            for (int i = 0; i < grdBarang.Rows.Count; i++)
            {
                string itemId = grdBarang.DataKeys[i].Value.ToString();
                Button btnSelectDelete = (Button)grdBarang.Rows[i].FindControl("btnSelectDelete");
                btnSelectDelete.Enabled = true;

                DataSet mySet = ObjDb.GetRows("Select distinct noTransaksi from TransPiutang Where noTransaksi = '" + itemId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                    btnSelectDelete.Enabled = false;
                
            }

        }

        protected void cekKategori_Usaha()
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                divDanaBOSH.Visible = true;
                divkodeVA.Visible = true;
                divdenda.Visible = true;
                divurutan.Visible = true;
                divlevel.Visible = true;
                divBank.Visible = true;

            }
            else
            {
                divDanaBOSH.Visible = false;
                divkodeVA.Visible = false;
                divdenda.Visible = false;
                divurutan.Visible = false;
                divlevel.Visible = false;
                divBank.Visible = false;
            }
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

        protected void LoadDataCombo()
        {
            if (ObjSys.GetKategori_Usaha == "Sekolah")
            {
                if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Cash'").Tables[0].Rows.Count > 0)
                {
                    cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2') a");
                    cbonorekDb.DataValueField = "id";
                    cbonorekDb.DataTextField = "name";
                    cbonorekDb.DataBind();

                    cbonorekKd.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('12') and sts='2') a");
                    cbonorekKd.DataValueField = "id";
                    cbonorekKd.DataTextField = "name";
                    cbonorekKd.DataBind();
                }
                if (ObjDb.GetRows("select systemkas from Parameter WHERE systemkas = 'Akrual'").Tables[0].Rows.Count > 0)
                {
                    cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('12') and sts='2') a");
                    cbonorekDb.DataValueField = "id";
                    cbonorekDb.DataTextField = "name";
                    cbonorekDb.DataBind();

                    cbonorekKd.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('11') and sts='2') a");
                    cbonorekKd.DataValueField = "id";
                    cbonorekKd.DataTextField = "name";
                    cbonorekKd.DataBind();
                }
            }
            else
            {
                cbonorekDb.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih COA Debit---' name union SELECT distinct norek id,ket name FROM mRekening where jenis in('1','2') and sts='2') a");
                cbonorekDb.DataValueField = "id";
                cbonorekDb.DataTextField = "name";
                cbonorekDb.DataBind();
            }

            cbojnskegiatan.DataSource = ObjDb.GetRows("select a.* from (SELECT '0' id, '---Pilih Jenis Kegiatan---' name union SELECT distinct noMkegiatan id,namaKegiatan name FROM mJenisKegiatan where sts='1') a");
            cbojnskegiatan.DataValueField = "id";
            cbojnskegiatan.DataTextField = "name";
            cbojnskegiatan.DataBind();

            cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='2' and nocabang=" + ObjSys.GetCabangId + " union SELECT distinct a1.norek id, a1.ket name FROM mRekening a1 inner join mCabang b1 on a1.nocabang = b1.noCabang where a1.jenis='2' and a1.sts='2' and b1.parent=" + ObjSys.GetParentCabang + " and b1.stscabang='3' union SELECT distinct norek id,ket name FROM mRekening where jenis='1' and sts='2' and nocabang=" + ObjSys.GetCabangId + ") a");
            cbobank.DataValueField = "id";
            cbobank.DataTextField = "name";
            cbobank.DataBind();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (jenisTransaksi.Text == "")
            {
                message += ObjSys.CreateMessage("Jenis Transaksi tidak boleh kosong.");
                valid = false;
            }
            if (cboposbln.Text == "")
            {
                message += ObjSys.CreateMessage("Kategori Transaksi harus dipilih.");
                valid = false;
            }
            if (cboposbln.Text == "1")
            {
                if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE posbln = '1' and noCabang = '" + ObjSys.GetCabangId + "' and  notransaksi <> '" + hdnId.Value + "' ").Tables[0].Rows.Count > 0)
                {
                    message += ObjSys.CreateMessage("Kategori Transaksi SPP sudah ada dan hanya untuk 1x input.");
                    valid = false;
                }
            }
                
            if (cbonorekDb.Text == "")
            {
                message += ObjSys.CreateMessage("COA Debit harus dipilih.");
                valid = false;
            }
            if (cbonorekKd.Text == "")
            {
                message += ObjSys.CreateMessage("COA Kredit tidak boleh kosong.");
                valid = false;
            }
            if (txtdenda.Text == "")
            {
                message += ObjSys.CreateMessage("Nilai denda tidak boleh kosong.");
                valid = false;
            }
            if (kodeva.Text == "")
            {
                message += ObjSys.CreateMessage("Kode Virtual Account tidak boleh kosong.");
                valid = false;
            }
            if (txtUrutan.Text == "")
            {
                message += ObjSys.CreateMessage("Urutan tidak boleh kosong.");
                valid = false;
            }
            if (cbopelunasan.Text == "0")
            {
                message += ObjSys.CreateMessage("Level Pelunasan harus dipilih.");
                valid = false;
            }
            if (cbobank.Text == "")
            {
                message += ObjSys.CreateMessage("Bank harus dipilih.");
                valid = false;
            }
            //if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE kodeVA = '" + kodeva.Text + "' and noCabang = '" + ObjSys.GetCabangId + "' ").Tables[0].Rows.Count > 0)
            //{
            //    if (ObjDb.GetRows("SELECT * FROM mJenistransaksi WHERE urutan = '" + txtUrutan.Text + "' and noCabang = '" + ObjSys.GetCabangId + "' and  notransaksi <> '" + hdnId.Value + "' ").Tables[0].Rows.Count > 0)
            //    {
            //        message += ObjSys.CreateMessage("Urutan sudah ada.");
            //        valid = false;
            //    }
            //}

            if (valid == true)
            {
                
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("notransaksi", hdnId.Value);
                    ObjGlobal.Param.Add("jenisTransaksi", jenisTransaksi.Text);
                    ObjGlobal.Param.Add("posbln", cboposbln.Text);
                    ObjGlobal.Param.Add("norekDB", cbonorekDb.Text);
                    ObjGlobal.Param.Add("norekKD", cbonorekKd.Text);
                    ObjGlobal.Param.Add("kodeva", kodeva.Text);
                    ObjGlobal.Param.Add("denda", Convert.ToDecimal(txtdenda.Text).ToString());
                    ObjGlobal.Param.Add("urutan", txtUrutan.Text);
                    ObjGlobal.Param.Add("pelunasan", cbopelunasan.Text);
                    ObjGlobal.Param.Add("nokegiatan", cbojnskegiatan.Text);
                    ObjGlobal.Param.Add("norekbank", cbobank.Text);
                    ObjGlobal.Param.Add("nomhs", nomhs.Text);
                    ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("closeopen", closeopen.Text);
                    ObjGlobal.GetDataProcedure("SPUpdatemjnstransaksi", ObjGlobal.Param);

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
                  

                    if (ObjSys.GetKategori_Usaha == "Sekolah")
                    {

                        int rowIndex = int.Parse(e.CommandArgument.ToString());
                        string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                        hdnId.Value = nobarang;

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noTransaksi", hdnId.Value);
                        DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatasjenistrsDetil1", ObjGlobal.Param);
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        jenisTransaksi.Text = myRow["jenisTransaksi"].ToString();
                        cboposbln.SelectedValue = myRow["posbln"].ToString();
                        cbonorekDb.SelectedValue = myRow["norekDb"].ToString();
                        cbonorekKd.SelectedValue = myRow["norekKd"].ToString();
                        cbobank.SelectedValue = myRow["norekbank"].ToString();
                        cbojnskegiatan.SelectedValue = myRow["nokegiatan"].ToString();
                        txtdenda.Text = ObjSys.IsFormatNumber(myRow["denda"].ToString());
                        kodeva.Text = myRow["kodeVA"].ToString();
                        nomhs.Text = myRow["notransaksimhs"].ToString();
                        txtUrutan.Text = myRow["urutan"].ToString();
                        cbopelunasan.SelectedValue = myRow["pelunasan"].ToString();
                        closeopen.SelectedValue = myRow["closeopen"].ToString();
                    }
                    else
                    {

                        int rowIndex = int.Parse(e.CommandArgument.ToString());
                        string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                        hdnId.Value = nobarang;

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noTransaksi", hdnId.Value);
                        DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatasjenistrsDetil1", ObjGlobal.Param);
                        DataRow myRow = mySet.Tables[0].Rows[0];

                        jenisTransaksi.Text = myRow["jenisTransaksi"].ToString();
                        cbonorekDb.SelectedValue = myRow["norekDb"].ToString();
                        cbonorekKd.SelectedValue = myRow["norekKd"].ToString();

                    }

                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string nobarang = grdBarang.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = nobarang;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noTransaksi", hdnId.Value);
                    ObjDb.Delete("mJenistransaksi", ObjDb.Where);

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

        protected void cbopelunasan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbopelunasan.Text == "2")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct norek id,ket name FROM mRekening where jenis='2' and nocabang=" + ObjSys.GetCabangId + " union SELECT distinct norek id,ket name FROM mRekening where jenis='1' and nocabang=" + ObjSys.GetCabangId + " union SELECT distinct a1.norek id, a1.ket name FROM mRekening a1 inner join mCabang b1 on a1.nocabang = b1.noCabang where a1.jenis='2' and b1.parent=" + ObjSys.GetParentCabang + " and b1.stscabang='3') a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            else if (cbopelunasan.Text == "3")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct a1.norek id, a1.ket name FROM mRekening a1 inner join mCabang b1 on a1.nocabang = b1.noCabang where a1.jenis='2' and a1.sts='2' and a1.parent=" + ObjSys.GetParentCabang + " and b1.stscabang=3) a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            else if (cbopelunasan.Text == "4")
            {
                cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name union SELECT distinct norek id, ket name FROM mRekening a inner join mcabang b on a.nocabang=b.nocabang where jenis='2' and sts='2' and b.stscabang=4) a");
                cbobank.DataValueField = "id";
                cbobank.DataTextField = "name";
                cbobank.DataBind();
            }
            //else if (cbopelunasan.Text == "")
            //{
            //    cbobank.DataSource = ObjDb.GetRows("select a.* from (SELECT '' id, '---Pilih Bank---' name) a");
            //    cbobank.DataValueField = "id";
            //    cbobank.DataTextField = "name";
            //    cbobank.DataBind();
            //}
        }
    }
}