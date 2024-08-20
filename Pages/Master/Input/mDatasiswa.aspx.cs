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
    public partial class mDatasiswa : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("mDatasiswa.aspx");
                loadDataCombo();
            }
        }

        private void loadDataCombo()
        {
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union select noKota, Kota from mKota");
            cboKota.DataValueField = "Kota";
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
            if (nisn.Text == "")
            {
                message += ObjSys.CreateMessage("NISN harus diisi.");
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
            if (dttglmasuk.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal Masuk harus diisi.");
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
                if (ObjDb.GetRows("SELECT * FROM mSiswa WHERE namasiswa = '" + nama.Text + "' and noCabang = '" + ObjSys.GetCabangId + "'").Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Siswa sudah ada.");
                }
                else
                {
               
                    try
                    {
                        string Kode = "";
                        Kode = ObjSys.GetCodeAutoNumberNew("15", Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd"));

                        ObjGlobal.Param.Clear();
                        ObjGlobal.Param.Add("namaSiswa", nama.Text);
                        ObjGlobal.Param.Add("nik", nik.Text);
                        ObjGlobal.Param.Add("nis", nis.Text);
                        ObjGlobal.Param.Add("nisn", nisn.Text);
                        ObjGlobal.Param.Add("gender", cbojnskelamin.Text);
                        ObjGlobal.Param.Add("agama", cboAgama.Text);
                        ObjGlobal.Param.Add("Alamat", Alamat.Text);
                        ObjGlobal.Param.Add("kota", cboKota.Text);
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
                        ObjGlobal.Param.Add("discount", discount.Text);
                        ObjGlobal.GetDataProcedure("SPInsertDataSiswa", ObjGlobal.Param);

                        ObjSys.UpdateAutoNumberCodeNew("15", Convert.ToDateTime(ObjSys.GetDate).ToString("yyyy-MM-dd"));
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
            nama.Text = "";
            nik.Text = "";
            nis.Text = "";
            nisn.Text = "";
            cbojnskelamin.Text = "0";
            cboAgama.Text = "0";
            Alamat.Text = "";
            cboKota.Text = "---Pilih Kota---";
            dtTgllahir.Text = "";
            dttglmasuk.Text = "";
            cboKotalahir.Text = "---Pilih Kota---";
            namaOrangtua.Text = "";
            telp.Text = "";
            novirtual.Text = "";
            keterangan.Text = "";
            keterangan1.Text = "";
            keterangan2.Text = "";
        }
    }
}