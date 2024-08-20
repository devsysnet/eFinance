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
    public partial class TransMutasiKaryawan : System.Web.UI.Page
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
                loadDataCombo();

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
            cboCabang.DataSource = ObjDb.GetRows("SELECT distinct noCabang id, namaCabang name FROM mCabang where stsCabang in (2,3,4)");
            cboCabang.DataValueField = "id";
            cboCabang.DataTextField = "name";
            cboCabang.DataBind();

            cbojabatan.DataSource = ObjDb.GetRows("select 0 id, '--Pilih Jabatan' name union SELECT distinct nojabatan id, jabatan name FROM mstJabatan");
            cbojabatan.DataValueField = "id";
            cbojabatan.DataTextField = "name";
            cbojabatan.DataBind();

        }

        protected void LoadDataPeminta()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearchMinta.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdDataPeminta.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKaryawanMutasi", ObjGlobal.Param);
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
            cboTransaction.Text = "0";
            mutasiK.Text = "0";
            txtNoSK.Text = "";
            SetInitialRow();
            formjabatan.Visible = false;
            formpindah.Visible = false;
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
                message = ObjSys.CreateMessage("Tanggal Mutasi harus di isi.");
                alert = "error";
                valid = false;
            }


           


            try
            {

                if (valid == true)
                {

                    string Kode = ObjSys.GetCodeAutoNumberNew("36", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("KodeMutasi", Kode);
                    ObjGlobal.Param.Add("tglMutasi", dtDate.Text);
                    ObjGlobal.Param.Add("noKaryawan", hdnNoUser.Value);
                    ObjGlobal.Param.Add("kategori", cboTransaction.Text);
                    ObjGlobal.Param.Add("nostatuspegawai", "0");

                    if (cboTransaction.Text == "Mutasi")
                    {
                        ObjGlobal.Param.Add("noCabangke", cboCabang.Text);

                    }
                    else {
                        ObjGlobal.Param.Add("noCabangke", "0");

                    }
                        
                    ObjGlobal.Param.Add("noSK", txtNoSK.Text);
                    ObjGlobal.Param.Add("uraian", txtKeterangan.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.Param.Add("createddate", ObjSys.GetDate);
                    ObjGlobal.Param.Add("jabatan", cbojabatan.Text);


                
                    ObjGlobal.ExecuteProcedure("SPInsertMutasiKaryawan", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("36", Convert.ToDateTime(dtDate.Text).ToString("yyyy-MM-dd"));

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
            if(cboTransaction.Text == "Mutasi")
            {
                formpindah.Visible = true;
                formjabatan.Visible = false;
            }else if (cboTransaction.Text == "Promosi" || cboTransaction.Text == "Demosi" || cboTransaction.Text == "Mutasijabatan")
            {
                formpindah.Visible = false;
                formjabatan.Visible = true;
            }else
            {
                formpindah.Visible = false;
                formjabatan.Visible = false;
            }
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