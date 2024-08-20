using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransPindahAsset : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCombo();
            }
        }

        protected void LoadCombo()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Lokasi---' name union all SELECT distinct noLokasi id, Lokasi name FROM mLokasi ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            cboSubLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Sub Lokasi---' name union all SELECT distinct noSubLokasi id, SubLokasi name FROM mSubLokasi ) a");
            cboSubLokasi.DataValueField = "id";
            cboSubLokasi.DataTextField = "name";
            cboSubLokasi.DataBind();

            cbocabang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih Unit---' name union all SELECT distinct nocabang id, namacabang name FROM mcabang where stsCabang in(2,3) ) a");
            cbocabang.DataValueField = "id";
            cbocabang.DataTextField = "name";
            cbocabang.DataBind();


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

        protected void btnBrowseAsset_Click(object sender, ImageClickEventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void LoadDataAsset()
        {
            //tampilin asset
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            ObjGlobal.Param.Add("noCabang", ObjSys.GetCabangId);
            grdDataAsset.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAset", ObjGlobal.Param);
            grdDataAsset.DataBind();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtNamaBarangAset.Text == "")
            {
                message += ObjSys.CreateMessage("Aset harus dipilih.");
                valid = false;
            }
            if (dtPindah.Text == "")
            {
                message += ObjSys.CreateMessage("Tanggal harus diisi.");
                valid = false;
            }
            if (txtAlasan.Text == "")
            {
                message += ObjSys.CreateMessage("Alasan harus diisi.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    string Kode = "";

                    ObjGlobal.Param.Clear();
                    Kode = ObjSys.GetCodeAutoNumberNew("1", Convert.ToDateTime(dtPindah.Text).ToString("yyyy-MM-dd"));

                    ObjGlobal.Param.Add("noAset", hdnBarangAset.Value);
                    ObjGlobal.Param.Add("KdPindah", Kode);
                    ObjGlobal.Param.Add("tglPindah", Convert.ToDateTime(dtPindah.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("alasan", txtAlasan.Text);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("nocabangtujuan", cbocabang.Text);
                    ObjGlobal.Param.Add("createdBy", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPTranspindahasset", ObjGlobal.Param);

                    ObjSys.UpdateAutoNumberCodeNew("1", Convert.ToDateTime(dtPindah.Text).ToString("yyyy-MM-dd"));

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
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
            clearData();
        }

        protected void clearData()
        {
            hdnBarangAset.Value = "0";
            txtNamaBarangAset.Text = "";
            txtKodeAset.Text = "";
            cboLokasi.Text = "0";
            cboSubLokasi.Text = "0";
            dtPindah.Text = "";
            txtAlasan.Text = "";
        }

        protected void btnAsset_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void grdDataAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRow = grdDataAsset.SelectedIndex;
            txtNamaBarangAset.Text = grdDataAsset.Rows[selectedRow].Cells[2].Text;
            txtKodeAset.Text = grdDataAsset.Rows[selectedRow].Cells[1].Text;
            hdnBarangAset.Value = (grdDataAsset.SelectedRow.FindControl("hdnnoAset") as HiddenField).Value;
            cboLokasi.Text = (grdDataAsset.SelectedRow.FindControl("hdnnoLokasi") as HiddenField).Value;
            cboSubLokasi.Text = (grdDataAsset.SelectedRow.FindControl("hdnnoSubLokasi") as HiddenField).Value;
        }

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
        }
    }
}