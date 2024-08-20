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
    public partial class mPasienupdate : System.Web.UI.Page
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
            for (int i = 0; i < grdSiswa.Rows.Count; i++)
            {
                string itemId = grdSiswa.DataKeys[i].Value.ToString();
                Button btnSelectDel = (Button)grdSiswa.Rows[i].FindControl("btnSelectDel");

                DataSet mySet = ObjDb.GetRows("Select top 1 noSiswa from TransKelas Where noSiswa = '" + itemId + "'");
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
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatapasienUpdate", ObjGlobal.Param);
            grdSiswa.DataBind();

            IndexPakai();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdSiswa.PageIndex = e.NewPageIndex;
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
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union select nokota, Kota from mKota");
            cboKota.DataValueField = "kota";
            cboKota.DataTextField = "Kota";
            cboKota.DataBind();

            cboKotalahir.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union select noKota, Kota from mKota");
            cboKotalahir.DataValueField = "Kota";
            cboKotalahir.DataTextField = "Kota";
            cboKotalahir.DataBind();

            cboAgama.DataSource = ObjDb.GetRows("select 0 as noAgama, '---Pilih Agama---' as Agama union select noAgama, Agama from mAgama");
            cboAgama.DataValueField = "noAgama";
            cboAgama.DataTextField = "Agama";
            cboAgama.DataBind();
        }



        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (namaPasien.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Pasien harus diisi.");
                valid = false;
            }
            if (cbojnskelamin.Text == "0")
            {
                message += ObjSys.CreateMessage("Jenis Kelamin harus dipilih.");
                valid = false;
            }
            if (cboKota.Text == "0")
            {
                message += ObjSys.CreateMessage("Kota harus dipilih.");
                valid = false;
            }
            if (dtTgllahir.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Lahir harus diisi.");
                valid = false;
            }
            if (cboAgama.Text == "0")
            {
                message += ObjSys.CreateMessage("Agama harus dipilih.");
                valid = false;
            }
            if (cboKotalahir.Text == "0")
            {
                message += ObjSys.CreateMessage("Kota Lahir harus dipilih.");
                valid = false;
            }
            
            if (valid == true)
            {
                if (ObjDb.GetRows("SELECT * FROM mPasien WHERE nik = '" + nik.Text + "' " +
                    "and noCabang = '" + ObjSys.GetCabangId + "' and nopasien <> '" + hdnId.Value + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Siswa sudah ada.");
                }
                else
                {
                    try
                    {
                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("nopasien", hdnId.Value);
                        ObjGlobal.Param.Add("namaPasien", namaPasien.Text);
                        ObjGlobal.Param.Add("nik", nik.Text);
                        ObjGlobal.Param.Add("jeniskelamin", cbojnskelamin.Text);
                        ObjGlobal.Param.Add("agama", cboAgama.Text);
                        ObjGlobal.Param.Add("Alamat", Alamat.Text);
                        ObjGlobal.Param.Add("kota", cboKota.Text);
                        ObjGlobal.Param.Add("tgllahir", dtTgllahir.Text);
                        ObjGlobal.Param.Add("kotalahir", cboKotalahir.Text);
                        ObjGlobal.Param.Add("goldarah", cboGoldarah.Text);
                        ObjGlobal.Param.Add("notelp", notelp.Text);
                        ObjGlobal.Param.Add("nohp", nohp.Text);
                        ObjGlobal.Param.Add("stsBpjs", cboBpjs.Text);
                        ObjGlobal.Param.Add("noBpjs", noBpjs.Text);
                        ObjGlobal.Param.Add("uraian", uraian.Text);
                        ObjGlobal.Param.Add("sts",cbosts.Text);
                        ObjGlobal.Param.Add("modiBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.GetDataProcedure("SPUpdateDataPasien", ObjGlobal.Param);

                        ShowHideGridAndForm(true, false);
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil diubah.");
                        LoadData();
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

        protected void grdSiswa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "SelectEdit")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noSiswa = grdSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noSiswa;

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("nopasien", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatapasienUpdateDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    namaPasien.Text = myRow["namaPasien"].ToString();
                    nik.Text = myRow["nik"].ToString();
                    cbojnskelamin.SelectedValue = myRow["jeniskelamin"].ToString();
                    Alamat.Text = myRow["alamat"].ToString();
                    if (myRow["nokota"] != null)
                        cboKota.SelectedValue = myRow["nokota"].ToString();
                    else
                        cboKota.SelectedValue = "---Pilih Kota---";
                    
                    cboAgama.SelectedValue = myRow["agama"].ToString();
                    if (myRow["tempatlahir"] != null)
                        cboKotalahir.SelectedValue = myRow["tempatlahir"].ToString();
                    else
                        cboKotalahir.SelectedValue = "---Pilih Kota---";

                    dtTgllahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                    cboGoldarah.Text = myRow["goldarah"].ToString();
                    notelp.Text = myRow["notelp"].ToString();
                    nohp.Text = myRow["nohp"].ToString();
                    cboBpjs.Text = myRow["stsBpjs"].ToString();
                    noBpjs.Text = myRow["noBpjs"].ToString();
                    uraian.Text = myRow["uraian"].ToString();

                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noSiswa = grdSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noSiswa;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noPasien", hdnId.Value);
                    ObjDb.Delete("mPasien", ObjDb.Where);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    this.ShowHideGridAndForm(true, false);
                    LoadData();
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