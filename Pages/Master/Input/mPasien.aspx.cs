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
    public partial class mPasien : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("mPasien.aspx");
                loadDataCombo();
            }
        }

        private void loadDataCombo()
        {
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union select nokota, Kota from mKota");
            cboKota.DataValueField = "kota";
            cboKota.DataTextField = "Kota";
            cboKota.DataBind();

            cboKotalahir.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union select nokota, Kota from mKota");
            cboKotalahir.DataValueField = "kota";
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
                if (ObjDb.GetRows("SELECT * FROM mPasien WHERE (namaPasien = '" + namaPasien.Text + "' or nik='" + nik.Text + "') and noCabang = '" + ObjSys.GetCabangId + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Pasien sudah ada.");
                }
                else
                {

                    try
                    {
                        string Kode = "";
                        Kode = ObjSys.GetCodeAutoNumberNew("37", Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd"));

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("NoRegister", Kode);
                        ObjGlobal.Param.Add("namaPasien", namaPasien.Text);
                        ObjGlobal.Param.Add("nik", nik.Text);
                        ObjGlobal.Param.Add("jeniskelamin", cbojnskelamin.Text);
                        ObjGlobal.Param.Add("agama", cboAgama.Text);
                        ObjGlobal.Param.Add("Alamat", Alamat.Text);
                        ObjGlobal.Param.Add("kota", cboKota.Text);
                        ObjGlobal.Param.Add("tgllahir", dtTgllahir.Text);
                        ObjGlobal.Param.Add("tempatLahir", cboKotalahir.Text);
                        ObjGlobal.Param.Add("notelp", notelp.Text);
                        ObjGlobal.Param.Add("noHP", nohp.Text);
                        ObjGlobal.Param.Add("stsBpjs", cboBpjs.Text);
                        ObjGlobal.Param.Add("noBpjs", noBpjs.Text);
                        ObjGlobal.Param.Add("sts", "1");
                        ObjGlobal.Param.Add("goldarah", cboGoldarah.Text);
                        ObjGlobal.Param.Add("uraian", uraian.Text);
                        ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                        ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                        ObjGlobal.GetDataProcedure("SPInsertDataPasien", ObjGlobal.Param);

                        ObjSys.UpdateAutoNumberCodeNew("37", Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd"));
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
            namaPasien.Text = "";
            nik.Text = "";
            cbojnskelamin.Text = "0";
            cboAgama.Text = "0";
            Alamat.Text = "";
            notelp.Text = "";
            nohp.Text = "";
            noBpjs.Text = "";
            uraian.Text = "";
            cboGoldarah.Text = "0";
            cboKota.Text = "---Pilih Kota---";
            dtTgllahir.Text = "";
            cboKotalahir.Text = "---Pilih Kota---";
        }
    }
}