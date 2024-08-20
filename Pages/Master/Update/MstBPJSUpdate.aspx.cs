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
    public partial class MstBPJSUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
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
            grdBPJS.DataSource = ObjDb.GetRows("SELECT a.noBPJS, a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstBPJS a");
            grdBPJS.DataBind();
        }
        protected void LoadDataSearch()
        {
            grdBPJS.DataSource = ObjDb.GetRows("SELECT a.noBPJS, a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstBPJS a WHERE a.BPJS like '%" + txtSearch.Text + "%'");
            grdBPJS.DataBind();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("noBPJS", hdnId.Value);
                ClearData();
                if (hdnMode.Value.ToLower() == "edit")
                {
                    this.ShowHideGridAndForm(false, true, false);

                    DataSet mySet = ObjDb.GetRows("SELECT a.noBPJS, a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan,a.sts FROM MstBPJS a WHERE a.noBPJS = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtNamaArea.Text = myRow["BPJS"].ToString();
                    txtpersenperusahan.Text = myRow["persenperusahan"].ToString();
                    Textpersenkaryawan.Text = myRow["persenkaryawan"].ToString();
                    cboStatus.SelectedValue = myRow["sts"].ToString();


                    this.ShowHideGridAndForm(false, true, false);

                }
                else if (hdnMode.Value.ToLower() == "delete")
                {
                    ObjDb.Delete("MstBPJS", ObjDb.Where);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false, false);
                }
                else if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noBPJS", itemRow);
                            ObjDb.Delete("MstBPJS", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdBPJS.Rows)
                    {
                        string itemId = grdBPJS.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdBPJS.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noBPJS", itemId);
                            ObjDb.Delete("MstBPJS", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData();
                    ShowMessage("success", "Data yang dipilih berhasil dihapus.");
                    this.ShowHideGridAndForm(true, false, false);
                }
                else if (hdnMode.Value.ToLower() == "view")
                {
                    this.ShowHideGridAndForm(false, false, true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }
        protected void ClearData()
        {
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
            this.ShowHideGridAndForm(true, false, false);
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            if (txtNamaArea.Text == "")
            {
                message += ObjSys.CreateMessage("Nama BPJS tidak boleh kosong.");
                valid = false;
            }
            if (cboStatus.Text == "")
            {
                message += ObjSys.CreateMessage("Status harus dipilih.");
                valid = false;
            }
            DataSet mySet = ObjDb.GetRows("SELECT a.noBPJS, a.BPJS,CAST(CONVERT(VARCHAR, CAST(a.persenperusahan AS MONEY), 1) AS VARCHAR) as persenperusahan,CAST(CONVERT(VARCHAR, CAST(a.persenkaryawan AS MONEY), 1) AS VARCHAR) as persenkaryawan, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstBPJS a where BPJS= '" + txtNamaArea.Text + "' and noBPJS<> '" + hdnId.Value + "'");
            if (mySet.Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama BPJS <b>" + txtNamaArea.Text + "</b> sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noBPJS", hdnId.Value);
                    ObjDb.Data.Add("BPJS", txtNamaArea.Text);
                    ObjDb.Data.Add("persenperusahan", Convert.ToDecimal(txtpersenperusahan.Text).ToString());
                    ObjDb.Data.Add("persenkaryawan", Convert.ToDecimal(Textpersenkaryawan.Text).ToString());
                    ObjDb.Data.Add("sts", cboStatus.Text);
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Update("MstBPJS", ObjDb.Data, ObjDb.Where);

                    this.ShowHideGridAndForm(true, false, false);

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data.");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void grdBPJS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdBPJS.PageIndex = e.NewPageIndex;
            if (txtSearch.Text != "")
            {
                LoadDataSearch();
            }
            else
            {
                LoadData();
            }
            PopulateCheckedValues();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }
        /*START SAVE CHECKBOX SELECTED IN ROWS*/
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdBPJS.Rows)
                {
                    string index = grdBPJS.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues()
        {
            ArrayList userdetails = new ArrayList();
            foreach (GridViewRow gvrow in grdBPJS.Rows)
            {
                string index = grdBPJS.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdBPJS.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
        /*END SAVE CHECKBOX SELECTED IN ROWS*/
    }
}