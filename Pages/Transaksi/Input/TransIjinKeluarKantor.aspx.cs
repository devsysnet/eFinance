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
    public partial class TransIjinKeluarKantor : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        public Dictionary<string, string> Param = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearData();
            }
        }

        private void SetInitialRow()
        {

        }
        private void SetPreviousData()
        {
        }

        private void loadDataCombo()
        {

        }

        protected void LoadDataPeminta()
        {
            DataTable myData = ObjDb.GetRowsDataTable("select a.nokaryawan,a.nik,a.nama,b.departemen,isnull(c.saldocuti,(select cutitahunan from Parameter)) as saldocuti from mstkaryawan a left join Mstdepartemen b on a.dept=b.noDepartemen left join transcuti c on a.nokaryawan=c.nokaryawan where a.nocabang='" + ObjSys.GetCabangId + "' and (nik like '%" + txtSearchMinta.Text.Trim() + "%' or nama like '%" + txtSearchMinta.Text.Trim() + "%')");
            grdDataPeminta.DataSource = myData;
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
            hdnNoUser.Value = "";
            txtNamaPeminta.Text = "";
            txtKeterangan.Text = "";
            txtjammasuk.Text = "";
            SetInitialRow();

        }

        protected void btnBrowsePeminta_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void grdDataPeminta_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataPeminta.SelectedIndex;
            txtNamaPeminta.Text = grdDataPeminta.Rows[selectedRow].Cells[2].Text;
            hdnNoUser.Value = (grdDataPeminta.SelectedRow.FindControl("hdnNoUserD") as HiddenField).Value;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "", alert = "";


            if (dtDate.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Pengajuan harus di isi.");
                alert = "error";
                valid = false;
            }

            if (txtjammasuk.Text == "")
            {
                message = ObjSys.CreateMessage("Tanggal Mulai Cuti harus di isi.");
                alert = "error";
                valid = false;
            }




            else if (txtNamaPeminta.Text == "")
            {
                message = ObjSys.CreateMessage("Peminjam harus di pilih.");
                alert = "error";
                valid = false;
            }


            try
            {

                if (valid == true)
                {

                    string Kode = ObjSys.GetCodeAutoNumberNew("37", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("kodeCuti", Kode);
                    ObjGlobal.Param.Add("tglpengajuan", dtDate.Text);
                    ObjGlobal.Param.Add("nokaryawan", hdnNoUser.Value);
                    ObjGlobal.Param.Add("tglmulaicuti", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd") + " " + Convert.ToString(txtjammasuk.Text));
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.Param.Add("sts", "0");
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetDate);

                    ObjGlobal.ExecuteProcedure("SPInsertTransijinkeluar", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("37", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan");
                    ClearData();
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage(alert, message);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }

        }

        protected void LoadDataBiaya()
        {

        }

        protected void grdDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        protected void grdDataBiaya_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCari_Click(object sender, EventArgs e)
        {

        }

        protected void btnMinta_Click(object sender, EventArgs e)
        {
            LoadDataPeminta();
            dlgAddData.PopupControlID = "panelAddDataPeminta";
            dlgAddData.Show();
        }

        protected void grdDataBiaya_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}