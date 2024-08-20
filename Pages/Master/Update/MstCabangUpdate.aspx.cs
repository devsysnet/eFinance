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

namespace eFinance.Pages.Master.Update
{
    public partial class MstCabangUpdate : System.Web.UI.Page
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

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMasterCabang", ObjGlobal.Param);
            grdCabang.DataBind();
            IndexPakai();
        }

        protected void IndexPakai()
        {
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string itemId = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");


                DataSet mySet1 = ObjDb.GetRows("Select nocabang from unioncabang Where nocabang = '" + itemId + "'");
                if (mySet1.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;

            }

        }
        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            PopulateCheckedValues_View();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtNama.Text == "")
            {
                message += ObjSys.CreateMessage("Nama cabang tidak boleh kosong.");
                valid = false;
            }

            if (ObjDb.GetRows("SELECT * FROM mCabang WHERE namaCabang = '" + txtNama.Text + "' and nocabang <> '" + hdnID.Value + "'").Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama Cabang sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("Id", hdnID.Value);
                    ObjGlobal.Param.Add("nama", txtNama.Text);
                    ObjGlobal.Param.Add("alamat", txtAlamat.Text);
                    ObjGlobal.Param.Add("Telp", txtNoTelp.Text);
                    ObjGlobal.Param.Add("noFax", txtNoFax.Text);
                    ObjGlobal.Param.Add("kodePOS", txtKodePos.Text);
                    ObjGlobal.Param.Add("email", txtEmail.Text);
                    ObjGlobal.Param.Add("kota", txtKota.Text);
                    ObjGlobal.Param.Add("kategoriusaha", Kategori.Text);
                    ObjGlobal.Param.Add("office", txtOffice.Text);
                    if (cboStatus.Text == "0")
                    {
                        ObjGlobal.Param.Add("stsPusat", "1");
                        ObjGlobal.Param.Add("stsCabang", "0");
                        ObjGlobal.Param.Add("nourut", "1");
                    }
                    if (cboStatus.Text == "1")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "1");
                        ObjGlobal.Param.Add("nourut", "2");
                    }
                    if (cboStatus.Text == "2")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "2");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    if (cboStatus.Text == "3")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "3");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    if (cboStatus.Text == "4")
                    {
                        ObjGlobal.Param.Add("stsPusat", "0");
                        ObjGlobal.Param.Add("stsCabang", "3");
                        ObjGlobal.Param.Add("nourut", "3");
                    }
                    ObjGlobal.Param.Add("jnssekolah", cboUnit.Text);
                    ObjGlobal.Param.Add("parent", cboParent.Text);
                    ObjGlobal.Param.Add("allPelunasan", allPelunasan.Text);
                    ObjGlobal.Param.Add("cetakVoucher", cetakVoucher.Text);
                    ObjGlobal.Param.Add("Kategori", Kategori.Text);
                    ObjGlobal.Param.Add("modifiedBy", ObjSys.GetCabangId);
                    ObjGlobal.GetDataProcedure("SPUpdateCabang", ObjGlobal.Param);

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
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
            txtAlamat.Text = "";
            txtEmail.Text = "";
            txtKodePos.Text = "";
            txtKota.Text = "";
            txtNama.Text = "";
            txtNoFax.Text = "";
            txtNoTelp.Text = "";
            txtOffice.Text = "";
            cboStatus.Text = "";

            LoadData();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }

        protected void grdCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdCabang.SelectedRow.RowIndex;
                hdnID.Value = grdCabang.DataKeys[rowIndex].Values[0].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDataMasterCabang_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtAlamat.Text = myRow["alamatCabang"].ToString();
                Kategori.Text=myRow["kategoriusaha"].ToString();
                txtEmail.Text = myRow["emailCabang"].ToString();
                txtKodePos.Text = myRow["kodePosCabang"].ToString();
                txtKota.Text = myRow["kotaCabang"].ToString();
                txtNama.Text = myRow["namaCabang"].ToString();
                txtNoFax.Text = myRow["noFaxCabang"].ToString();
                txtNoTelp.Text = myRow["noTelpCabang"].ToString();
                txtOffice.Text = myRow["namaOfficerFin"].ToString();
                cboStatus.Text = myRow["stscabang"].ToString();
                cboParent.Text = myRow["parent"].ToString();
                cboUnit.Text = myRow["jnssekolah"].ToString();
                mhs.Text = myRow["mhs"].ToString();
                allPelunasan.Text= myRow["allPelunasan"].ToString();
                cetakVoucher.Text = myRow["cetakVoucher"].ToString();

                LoadDataCombo();
                CloseMessage();
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        private void PopulateCheckedValues_View()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdCabang.Rows)
                {
                    string index = grdCabang.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues_View()
        {
            ArrayList userdetails = new ArrayList();
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string index = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");
                bool result = chkCheck.Checked;
                // Check in the Session
                if (ViewState["CHECKED_ITEMS"] != null)
                    userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
                if (result)
                {
                    if (!userdetails.Contains(index))
                        userdetails.Add(index);
                }
                else
                    userdetails.Remove(index);
            }
            if (userdetails != null && userdetails.Count > 0)
                ViewState["CHECKED_ITEMS"] = userdetails;
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCabang", itemRow);
                            ObjDb.Delete("mCabang", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCabang.Rows.Count; i++)
                    {
                        string itemId = grdCabang.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noCabang", itemId);
                            ObjDb.Delete("mCabang", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ObjDb.GetRows("SELECT * FROM mCabang WHERE stsCabang = '" + cboStatus.Text + "' and noCabang <> '" + hdnID.Value + "'").Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Cabang Pusat sudah ada.");
            }
        }

        protected void LoadDataCombo()
        {
            cboParent.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noCabang id, namaCabang name FROM mCabang where stscabang = '0' or stscabang = '1' ) a");
            cboParent.DataValueField = "id";
            cboParent.DataTextField = "name";
            cboParent.DataBind();

        }
    }
}