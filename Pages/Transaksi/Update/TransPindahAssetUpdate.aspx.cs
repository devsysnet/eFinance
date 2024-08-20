using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransPindahAssetUpdate : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtMulai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                dtSampai.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
                loadData();
            }
        }

        protected void loadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            ObjGlobal.Param.Add("dtMulai", dtMulai.Text);
            ObjGlobal.Param.Add("dtSampai", dtSampai.Text);
            ObjGlobal.Param.Add("search", txtCariAset.Text);
            grdAsetUpdate.DataSource = ObjGlobal.GetDataProcedure("SPLoadUpdatePindahAsset", ObjGlobal.Param);
            grdAsetUpdate.DataBind();
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

        protected void showHideFormKas(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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
            if (dtMusnah.Text == "")
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
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("noPindahasset", hdnnopindah.Value);
                    ObjGlobal.Param.Add("noAssetlm", hdnBarangAsetlm.Value);
                    ObjGlobal.Param.Add("noAsset", hdnBarangAset.Value);
                    ObjGlobal.Param.Add("tglPindah", Convert.ToDateTime(dtMusnah.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("noCabangTujuan", cbocabang.Text);
                    ObjGlobal.Param.Add("uraian", txtAlasan.Text);
                    ObjGlobal.Param.Add("modiby", ObjSys.GetUserId);
                    ObjGlobal.GetDataProcedure("SPUpdatePindahAsset", ObjGlobal.Param);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                    showHideFormKas(true, false);
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
            showHideFormKas(true, false);
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
            hdnnopindah.Value = (grdDataAsset.SelectedRow.FindControl("hdnnoPindahAsset") as HiddenField).Value;
            hdnBarangAsetlm.Value = (grdDataAsset.SelectedRow.FindControl("hdnnoAset") as HiddenField).Value;
            hdnBarangAset.Value = (grdDataAsset.SelectedRow.FindControl("hdnnoAset") as HiddenField).Value;
            cboLokasi.Text = (grdDataAsset.SelectedRow.FindControl("hdnnoLokasi") as HiddenField).Value;
            cboSubLokasi.Text = (grdDataAsset.SelectedRow.FindControl("hdnnoSubLokasi") as HiddenField).Value;
            dlgAddData.Hide();
        }

        protected void grdDataAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdDataAsset.PageIndex = e.NewPageIndex;
            LoadDataAsset();
            dlgAddData.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }

        protected void grdAsetUpdate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdAsetUpdate.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdAsetUpdate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectEdit")
                {
                    CloseMessage();
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdAsetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    DataSet myData = ObjDb.GetRows("select * from TranspindahAsset a inner join tasset b on a.noasset=b.noAset where nopindahAsset = '" + id + "'");
                    DataRow myRow = myData.Tables[0].Rows[0];

                    hdnnopindah.Value= myRow["noPindahasset"].ToString();
                    hdnBarangAsetlm.Value= myRow["noAset"].ToString();
                    hdnBarangAset.Value = myRow["noAset"].ToString();
                    txtNamaBarangAset.Text = myRow["namaAsset"].ToString();
                    txtKodeAset.Text = myRow["kodeAsset"].ToString();
                    cboLokasi.Text = myRow["noLokasi"].ToString();
                    cboSubLokasi.Text = myRow["noSubLokasi"].ToString();
                    cbocabang.Text = myRow["noCabangTujuan"].ToString();
                    dtMusnah.Text = Convert.ToDateTime(myRow["tglpindah"]).ToString("dd-MMM-yyyy");
                    txtAlasan.Text = myRow["uraian"].ToString();
                    LoadCombo();
                    showHideFormKas(false, true);

                }
                else if (e.CommandName == "SelectDelete")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string id = grdAsetUpdate.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = id;

                    string sql = "update Tasset set tglMusnah = null, alasanMusnah = null where noAset = '" + hdnId.Value + "'";
                    ObjDb.ExecQuery(sql);

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Success Deleted");
                    showHideFormKas(true, false);
                    loadData();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
            }
        }
    }
}