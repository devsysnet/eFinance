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
using System.IO;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class Transurattugas : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void LoadDataPeminta()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearchMinta.Text);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("stsCabang", ObjSys.GetstsCabang);
            ObjGlobal.Param.Add("stsPusat", ObjSys.GetstsPusat);
            grdDataPeminta.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKaryawanPinjam", ObjGlobal.Param);
            grdDataPeminta.DataBind();
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
            dtDate.Text = "";
            hdnNoKaryawan.Value = "";
            txtNamaPeminta.Text = "";
            txtKeterangan.Text = "";
            dtmulai.Text = "";
            dtselesai.Text = "";
        }

        protected void btnBrowsePeminta_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.Show();
        }

        protected void grdDataPeminta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataPeminta.SelectedRow.RowIndex;
            string ID = grdDataPeminta.DataKeys[rowIndex].Values[0].ToString();
            string Nama = grdDataPeminta.SelectedRow.Cells[2].Text;
            txtNamaPeminta.Text = Nama;
            hdnNoKaryawan.Value = ID;
            dlgAddData.Hide();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Penugasan");
                alert = "error";
                valid = false;
            }
            if (dtmulai.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Mulai Penugasan");
                alert = "error";
                valid = false;
            }
            if (dtselesai.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Selesai Penugasan");
                alert = "error";
                valid = false;
            }
            if (txtNamaPeminta.Text == "")
            {
                message = ObjSys.CreateMessage("Nama Karyawan harus di pilih.");
                alert = "error";
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("30", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kodesrttugas", Kode);
                    ObjGlobal.Param.Add("tgltugas", dtDate.Text);
                    ObjGlobal.Param.Add("karyawan", hdnNoKaryawan.Value);
                    ObjGlobal.Param.Add("tglmulai", dtDate.Text);
                    ObjGlobal.Param.Add("tglselesai", dtDate.Text);
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetNow);

                    ObjGlobal.ExecuteProcedure("SPInsertSuratTugas", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("30", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.Message);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage(alert, message);
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.Show();
        }

    }
}