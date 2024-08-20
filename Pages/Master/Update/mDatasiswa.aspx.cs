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
    public partial class mDatasiswa : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
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
            grdSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSiswaUpdate", ObjGlobal.Param);
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
            //cboKota.DataSource = ObjDb.GetRows("select kota as noKota, Kota from mKota");
            //cboKota.DataValueField = "nokota";
            //cboKota.DataTextField = "Kota";
            //cboKota.DataBind();

            //cboKotalahir.DataSource = ObjDb.GetRows("select kota as noKota, Kota from mKota");
            //cboKotalahir.DataValueField = "nokota";
            //cboKotalahir.DataTextField = "Kota";
            //cboKotalahir.DataBind();

            cboAgama.DataSource = ObjDb.GetRows("select 0 as noAgama, '---Pilih Agama---' as Agama union select noAgama, Agama from mAgama");
            cboAgama.DataValueField = "noAgama";
            cboAgama.DataTextField = "Agama";
            cboAgama.DataBind();
        }

        

        protected void cmdMode_Click(object sender, EventArgs e)
        {

            string message = "";
            bool valid = true;
            if (nis.Text == "")
            {
                message += ObjSys.CreateMessage("NIS harus diisi.");
                valid = false;
            }
            if (nama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Siswa harus diisi.");
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
            if (namaOrangtua.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Orang Tua harus diisi.");
                valid = false;
            }

            if (valid == true)
            {
               
                    //if (ObjDb.GetRows("SELECT * FROM mSiswa WHERE nis = '" + nis.Text + "' "+
                    //    "and noCabang = '" + ObjSys.GetCabangId + "' and noSiswa <> '" + hdnId.Value + "'").Tables[0].Rows.Count > 0)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    //    ShowMessage("error", "Data Siswa sudah ada.");
                    //}
                    //else
                    //{
                    try
                    {
                    if (hdnMode.Value.ToLower() == "deleteall")
                    {

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                        ObjGlobal.Param.Add("namaSiswa", nama.Text);
                        ObjGlobal.Param.Add("nik", nik.Text);
                        ObjGlobal.Param.Add("nis", nis.Text);
                        ObjGlobal.Param.Add("nisn", nisn.Text);
                        ObjGlobal.Param.Add("gender", cbojnskelamin.Text);
                        ObjGlobal.Param.Add("agama", cboAgama.Text);
                        ObjGlobal.Param.Add("Alamat", Alamat.Text);
                        ObjGlobal.Param.Add("Kota", cboKota.Text);
                        ObjGlobal.Param.Add("tgllahir", dtTgllahir.Text);
                        ObjGlobal.Param.Add("tglmasuk", dttglmasuk.Text);
                        ObjGlobal.Param.Add("kotalahir", cboKotalahir.Text);
                        ObjGlobal.Param.Add("namaOrangtua", namaOrangtua.Text);
                        ObjGlobal.Param.Add("telp", telp.Text);
                        ObjGlobal.Param.Add("novirtual", novirtual.Text);
                        ObjGlobal.Param.Add("email", emailtxt.Text);
                        ObjGlobal.Param.Add("keterangan", keterangan.Text);
                        ObjGlobal.Param.Add("keterangan1", keterangan1.Text);
                        ObjGlobal.Param.Add("keterangan2", keterangan2.Text);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.Param.Add("sts", cboaktif.Text);
                        ObjGlobal.Param.Add("discount", discount.Text);
                        ObjGlobal.Param.Add("tglkeluar", dtkeluar.Text);
                        ObjGlobal.GetDataProcedure("SPUpdateDataSiswa", ObjGlobal.Param);

                       
                    }
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
                    ObjGlobal.Param.Add("noSiswa", hdnId.Value);
                    DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataSiswaUpdateDetil", ObjGlobal.Param);
                    DataRow myRow = mySet.Tables[0].Rows[0];
                    nama.Text = myRow["namaSiswa"].ToString();
                    nik.Text = myRow["nik"].ToString();
                    nis.Text = myRow["nis"].ToString();
                    nisn.Text = myRow["nisn"].ToString();
                    cbojnskelamin.SelectedValue = myRow["gender"].ToString();
                    Alamat.Text = myRow["alamat"].ToString();
                    cboKota.Text = myRow["kota"].ToString();
                    cboAgama.SelectedValue = myRow["agama"].ToString();
                    cboKotalahir.Text = myRow["kotaLahir"].ToString();
                    cboaktif.Text = myRow["sts"].ToString();
                    dtkeluar.Text = Convert.ToDateTime(myRow["tglkeluar"]).ToString("dd-MMM-yyyy");

                    dtTgllahir.Text = Convert.ToDateTime(myRow["tgllahir"]).ToString("dd-MMM-yyyy");
                    dttglmasuk.Text = Convert.ToDateTime(myRow["tglmasuk"]).ToString("dd-MMM-yyyy");
                    namaOrangtua.Text = myRow["namaOrangtua"].ToString();
                    telp.Text = myRow["telp"].ToString();
                    novirtual.Text = myRow["novirtual"].ToString();
                    discount.Text = myRow["discount"].ToString();
                    keterangan.Text = myRow["keterangan"].ToString();
                    keterangan1.Text = myRow["keterangan1"].ToString();
                    keterangan2.Text = myRow["keterangan2"].ToString();
                    LoadDataCombo();
                    this.ShowHideGridAndForm(false, true);
                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string noSiswa = grdSiswa.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = noSiswa;

                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noSiswa", hdnId.Value);
                    ObjDb.Delete("mSiswa", ObjDb.Where);

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