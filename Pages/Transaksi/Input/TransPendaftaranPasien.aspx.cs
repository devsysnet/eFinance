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
    public partial class TransPendaftaranPasien : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtDate.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadDataCombo();
            }
        }

        protected void LoadDataPasien()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearchMinta.Text);
            ObjGlobal.Param.Add("cabang", ObjSys.GetCabangId);
            grdDataPasien.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataPasien", ObjGlobal.Param);
            grdDataPasien.DataBind();
        }

        private void loadDataCombo()
        {
            cboDept.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Klinik---' name union all SELECT distinct noDepartemen id, departemen name FROM Mstdepartemen where transaksi ='1') a");
            cboDept.DataValueField = "id";
            cboDept.DataTextField = "name";
            cboDept.DataBind();
        }


        protected void cboLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbodokter.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Dokter---' name union all SELECT distinct nokaryawan id, nama name FROM MstKaryawan where dept = '" + cboDept.Text + "' and nocabang='" + ObjSys.GetCabangId + "') a");
            cbodokter.DataValueField = "id";
            cbodokter.DataTextField = "name";
            cbodokter.DataBind();

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
            hdnNopasien.Value = "";
            txtNamaPasien.Text = "";
            txtKeterangan.Text = "";
           
        }

        protected void btnBrowsePasien_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPasien();
            dlgAddData.Show();
        }

        protected void grdDataPasien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = grdDataPasien.SelectedRow.RowIndex;
            string ID = grdDataPasien.DataKeys[rowIndex].Values[0].ToString();
            string Nama = grdDataPasien.SelectedRow.Cells[2].Text;
            txtNamaPasien.Text = Nama;
            hdnNopasien.Value = ID;
            dlgAddData.Hide();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";

            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Pemeriksaan harus di isi.");
                alert = "error";
                valid = false;
            }
            if (txtNamaPasien.Text == "")
            {
                message = ObjSys.CreateMessage("Pasien harus di pilih.");
                alert = "error";
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    string Kode = ObjSys.GetCodeAutoNumberNew("18", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("KodePeriksa", Kode);
                    ObjGlobal.Param.Add("tglperiksa", dtDate.Text);
                    ObjGlobal.Param.Add("Pasien", hdnNopasien.Value);
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.Param.Add("klinik", cboDept.Text);
                    ObjGlobal.Param.Add("dokter", cbodokter.Text);
                    ObjGlobal.Param.Add("stsApv", "0");
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetNow);

                    ObjGlobal.ExecuteProcedure("SPInsertPendaftaranPasien", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("18", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

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
            LoadDataPasien();
            dlgAddData.Show();
        }

    }
}