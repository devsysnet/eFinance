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

namespace eFinance.Pages.Master.Update
{
    public partial class MstKomponengajiUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadData();
                LoadDatacombo();
            }
        }
        protected void LoadData()
        {
            grdkomponengaji.DataSource = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,case when a.kategori = 1 then 'Pendapatan' when a.kategori = 2 then 'Potongan' when a.kategori = 3 then 'Claim' when a.kategori = 4 then 'Pinjaman'  end kategori,case when a.jenis = 0 then 'Tidak Tetap' else 'Tetap' end jenis,case when a.pph21 = 0 then 'Tidak' else 'Ya' end pph21,case when a.absensi = 1 then 'Ya' else 'Tidak' end absensi,case when a.bpjs = 0 then 'Tidak' else 'Ya' end bpjs,case when a.bulanan = 1 then 'Bulanan' else 'Tahunan' end bulanan,case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts,b.ket,a.jeniskegiatan FROM Mstkomponengaji a left join mRekening b on a.norek=b.norek");
            grdkomponengaji.DataBind();
        }

        protected void LoadDatacombo()
        {
            cbnorek.DataSource = ObjDb.GetRows("SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='1' and sts='2'");
            cbnorek.DataValueField = "id";
            cbnorek.DataTextField = "name";
            cbnorek.DataBind();

            cboKategori.DataSource = ObjDb.GetRows("SELECT distinct nokategori id, kategori name FROM MstkategorikomponenGaji");
            cboKategori.DataValueField = "id";
            cboKategori.DataTextField = "name";
            cboKategori.DataBind();

            cbojeniskegiatan.DataSource = ObjDb.GetRows("SELECT distinct namakegiatan id, namakegiatan name FROM mJenisKegiatan");
            cbojeniskegiatan.DataValueField = "id";
            cbojeniskegiatan.DataTextField = "name";
            cbojeniskegiatan.DataBind();
        }
        protected void LoadDataSearch()
        {
            grdkomponengaji.DataSource = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,case when a.kategori = 1 then 'Pendapatan' when a.kategori = 2 then 'Potongan' when a.kategori = 3 then 'Claim' when a.kategori = 4 then 'Pinjaman'  end kategori,case when a.jenis = 0 then 'Tidak Tetap' else 'Tetap' end jenis,case when a.pph21 = 0 then 'Tidak' else 'Ya' end pph21,case when a.absensi = 1 then 'Ya' else 'Tidak' end absensi,case when a.bpjs = 0 then 'Tidak' else 'Ya' end bpjs,case when a.bulanan = 1 then 'Bulanan' else 'Tahunan' end bulanan,case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM Mstkomponengaji a WHERE a.komponengaji like '%" + txtSearch.Text + "%'");
            grdkomponengaji.DataBind();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("nokomponengaji", hdnId.Value);
                ClearData();
                if (hdnMode.Value.ToLower() == "edit")
                {
                    this.ShowHideGridAndForm(false, true, false);

                    DataSet mySet = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,a.kategori,a.norek,a.jenis,a.pph21,a.bulanan,a.absensi,a.bpjs,a.sts,a.jeniskegiatan FROM Mstkomponengaji a WHERE a.nokomponengaji = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtNamaArea.Text = myRow["komponengaji"].ToString();
                    cboKategori.SelectedValue = myRow["kategori"].ToString();
                    cboJenis.SelectedValue = myRow["jenis"].ToString();
                    cbopph21.SelectedValue = myRow["pph21"].ToString();
                    cboabsensi.SelectedValue = myRow["absensi"].ToString();
                    cbobpjs.SelectedValue = myRow["bpjs"].ToString();
                    cbobulanan.SelectedValue = myRow["bulanan"].ToString();
                    cbnorek.SelectedValue = myRow["norek"].ToString();
                    cbojeniskegiatan.SelectedValue = myRow["jeniskegiatan"].ToString();
                    cboStatus.SelectedValue = myRow["sts"].ToString();


                    this.ShowHideGridAndForm(false, true, false);

                }
                else if (hdnMode.Value.ToLower() == "delete")
                {
                    ObjDb.Delete("Mstkomponengaji", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false, false);
                }
                else if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokomponengaji", itemRow);
                            ObjDb.Delete("Mstkomponengaji", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdkomponengaji.Rows)
                    {
                        string itemId = grdkomponengaji.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdkomponengaji.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokomponengaji", itemId);
                            ObjDb.Delete("Mstkomponengaji", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData();
                    ShowMessage("success", "Data yang dipilih berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false, false);
                }
                else if (hdnMode.Value.ToLower() == "view")
                {
                    this.ShowHideGridAndForm(false, false, true);
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                //ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }
        protected void ClearData()
        {
            CloseMessage();
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
            ClearData();
            this.ShowHideGridAndForm(true, false, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtNamaArea.Text == "")
            {
                message += ObjSys.CreateMessage("Nama komponengaji tidak boleh kosong.");
                valid = false;
            }
            if (cboStatus.Text == "")
            {
                message += ObjSys.CreateMessage("Status harus dipilih.");
                valid = false;
            }
            DataSet mySet = ObjDb.GetRows("SELECT a.nokomponengaji, a.komponengaji,a.sts,a.kategori,a.jenis FROM Mstkomponengaji a where komponengaji= '" + txtNamaArea.Text + "' and nokomponengaji<> '" + hdnId.Value + "'");
            if (mySet.Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama komponengaji <b>" + txtNamaArea.Text + "</b> sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("nokomponengaji", hdnId.Value);
                    ObjDb.Data.Add("komponengaji", txtNamaArea.Text);
                    ObjDb.Data.Add("kategori", cboKategori.Text);
                    ObjDb.Data.Add("jenis", cboJenis.Text);
                    ObjDb.Data.Add("pph21", cbopph21.Text);
                    ObjDb.Data.Add("bpjs", cbobpjs.Text);
                    ObjDb.Data.Add("absensi", cboabsensi.Text);
                    ObjDb.Data.Add("bulanan", cbobulanan.Text);
                    ObjDb.Data.Add("jeniskegiatan", cbojeniskegiatan.Text);
                    ObjDb.Data.Add("sts", cboStatus.Text);
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Update("Mstkomponengaji", ObjDb.Data, ObjDb.Where);

                    this.ShowHideGridAndForm(true, false, false);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data.");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void grdkomponengaji_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdkomponengaji.PageIndex = e.NewPageIndex;
            if (txtSearch.Text != "")
            {
                LoadDataSearch();
            }
            else
            {
                LoadData();
            }
            PopulateCheckedValues();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        /*START SAVE CHECKBOX SELECTED IN ROWS*/
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdkomponengaji.Rows)
                {
                    string index = grdkomponengaji.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdkomponengaji.Rows)
            {
                string index = grdkomponengaji.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdkomponengaji.Rows[gvrow.RowIndex].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }
        /*END SAVE CHECKBOX SELECTED IN ROWS*/
    }
}