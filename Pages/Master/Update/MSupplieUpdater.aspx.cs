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
    public partial class MSupplieUpdater : System.Web.UI.Page
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


        protected void IndexPakai()
        {
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string itemId = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                DataSet mySet = ObjDb.GetRows("Select top 1 nosup from TransPO_H Where nosup = '" + itemId + "'");
                if (mySet.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;
            }

        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataSuplier", ObjGlobal.Param);
            grdCabang.DataBind();
            IndexPakai();
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            PopulateCheckedValues_View();
        }

        private void loadDataCombo()
        {
            cboKota.DataSource = ObjDb.GetRows("select 0 as noKota, '---Pilih Kota---' as Kota union all select noKota, Kota from mKota");
            cboKota.DataValueField = "noKota";
            cboKota.DataTextField = "Kota";
            cboKota.DataBind();

            cbosupp.DataSource = ObjDb.GetRows("select 0 as nojnssup, '---Pilih jenis supplier---' as Jenissup union all select nojnssup, Jenissup from mJenissupplier");
            cbosupp.DataValueField = "nojnssup";
            cbosupp.DataTextField = "Jenissup";
            cbosupp.DataBind();

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (txtnamaSupplier.Text == "")
            {
                message += ObjSys.CreateMessage("Nama Supplier tidak boleh kosong.");
                valid = false;
            }

            if (ObjDb.GetRows("SELECT * FROM mSupplier WHERE namaSupplier = '" + txtnamaSupplier.Text + "' and noSupplier <> '" + hdnID.Value + "'").Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama Supplier sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("Id", hdnID.Value);
                    ObjGlobal.Param.Add("namaSupplier", txtnamaSupplier.Text);
                    ObjGlobal.Param.Add("npwp", txtnpwp.Text);
                    ObjGlobal.Param.Add("Alamat", txtAlamat.Text);
                    ObjGlobal.Param.Add("alamatnpwp", txtAlamatNPWP.Text);
                    ObjGlobal.Param.Add("nojnssupp", cbosupp.Text);
                    ObjGlobal.Param.Add("Kota", cboKota.Text);
                    ObjGlobal.Param.Add("telpKantor", txttelpKantor.Text);
                    ObjGlobal.Param.Add("Fax", txtFax.Text);
                    ObjGlobal.Param.Add("email", txtemail.Text);
                    ObjGlobal.Param.Add("namaPIC", txtnamaPIC.Text);
                    ObjGlobal.Param.Add("telpPIC", txttelpPIC.Text);
                    ObjGlobal.Param.Add("noaccount", txtnoaccount.Text);
                    ObjGlobal.Param.Add("Bank", txtBank.Text);
                    ObjGlobal.Param.Add("keterangan", keterangan.Text);
                    ObjGlobal.Param.Add("keterangan1", keterangan1.Text);
                    ObjGlobal.Param.Add("keterangan2", keterangan2.Text);
                    ObjGlobal.Param.Add("modifiedBy", ObjSys.GetCabangId);
                    ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
                    ObjGlobal.GetDataProcedure("SPUpdateSupplier", ObjGlobal.Param);

                    showHideForm(true, false);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            showHideForm(true, false);
            CloseMessage();
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
            txtnamaSupplier.Text = "";
            txtnpwp.Text = "";
            txtAlamat.Text = "";
            txtAlamatNPWP.Text = "";
            cboKota.SelectedValue = "";
            txttelpKantor.Text = "";
            txtFax.Text = "";
            txtemail.Text = "";
            txtnamaPIC.Text = "";
            txttelpPIC.Text = "";
            txtnoaccount.Text = "";
            txtBank.Text = "";
            keterangan.Text = "";
            keterangan1.Text = "";
            keterangan2.Text = "";            
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
                ObjGlobal.Param.Add("noSupplier", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatasSupplierDetil", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtKode.Text = myRow["kodeSupplier"].ToString();
                txtnamaSupplier.Text = myRow["namaSupplier"].ToString();
                txtnpwp.Text = myRow["NPWP"].ToString();
                txtAlamat.Text = myRow["Alamat"].ToString();
                txtAlamatNPWP.Text = myRow["alamatnpwp"].ToString();
                cboKota.SelectedValue = myRow["kota"].ToString();
                cbosupp.SelectedValue = myRow["Jenissup"].ToString();
                txttelpKantor.Text = myRow["telpKantor"].ToString();
                txtFax.Text = myRow["Fax"].ToString();
                txtemail.Text = myRow["email"].ToString();
                txtnamaPIC.Text = myRow["namaPIC"].ToString();
                txttelpPIC.Text = myRow["telpPIC"].ToString();
                txtnoaccount.Text = myRow["noaccount"].ToString();
                txtBank.Text = myRow["Bank"].ToString();
                keterangan.Text = myRow["keterangan"].ToString();
                keterangan1.Text = myRow["keterangan1"].ToString();
                keterangan2.Text = myRow["keterangan2"].ToString();
                loadDataCombo();
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
                            ObjDb.Where.Add("noSupplier", itemRow);
                            ObjDb.Delete("mSupplier", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCabang.Rows.Count; i++)
                    {
                        string itemId = grdCabang.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {

                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noSupplier", itemId);
                            ObjDb.Delete("mSupplier", ObjDb.Where);
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
    }
}