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
    public partial class MstPangkatUpdate : System.Web.UI.Page
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
            grdPangkat.DataSource = ObjDb.GetRows("SELECT a.noPangkat, a.Pangkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPangkat a");
            grdPangkat.DataBind();
        }
        protected void LoadDataSearch()
        {
            grdPangkat.DataSource = ObjDb.GetRows("SELECT a.noPangkat, a.Pangkat, case when a.sts = 0 then 'Tidak Aktif' else 'Aktif' end sts FROM MstPangkat a WHERE a.Pangkat like '%" + txtSearch.Text + "%'");
            grdPangkat.DataBind();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("noPangkat", hdnId.Value);
                ClearData();
                if (hdnMode.Value.ToLower() == "edit")
                {
                    this.ShowHideGridAndForm(false, true, false);

                    DataSet mySet = ObjDb.GetRows("SELECT * FROM MstPangkat WHERE noPangkat = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtNamaArea.Text = myRow["Pangkat"].ToString();
                    cboStatus.SelectedValue = myRow["sts"].ToString();


                    this.ShowHideGridAndForm(false, true, false);

                }
                else if (hdnMode.Value.ToLower() == "delete")
                {
                    ObjDb.Delete("MstPangkat", ObjDb.Where);

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
                            ObjDb.Where.Add("noPangkat", itemRow);
                            ObjDb.Delete("MstPangkat", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdPangkat.Rows)
                    {
                        string itemId = grdPangkat.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdPangkat.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noPangkat", itemId);
                            ObjDb.Delete("MstPangkat", ObjDb.Where);
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
                message += ObjSys.CreateMessage("Nama Pangkat tidak boleh kosong.");
                valid = false;
            }
            if (cboStatus.Text == "")
            {
                message += ObjSys.CreateMessage("Status harus dipilih.");
                valid = false;
            }
            DataSet mySet = ObjDb.GetRows("Select * from MstPangkat where Pangkat= '" + txtNamaArea.Text + "' and noPangkat<> '" + hdnId.Value + "'");
            if (mySet.Tables[0].Rows.Count > 0)
            {
                message += ObjSys.CreateMessage("Nama Pangkat <b>" + txtNamaArea.Text + "</b> sudah ada.");
                valid = false;
            }

            if (valid == true)
            {
                try
                {

                    ObjDb.Where.Clear();
                    ObjDb.Data.Clear();
                    ObjDb.Where.Add("noPangkat", hdnId.Value);
                    ObjDb.Data.Add("Pangkat", txtNamaArea.Text);
                    ObjDb.Data.Add("sts", cboStatus.Text);
                    ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                    ObjDb.Update("MstPangkat", ObjDb.Data, ObjDb.Where);

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

        protected void grdPangkat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdPangkat.PageIndex = e.NewPageIndex;
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
                foreach (GridViewRow gvrow in grdPangkat.Rows)
                {
                    string index = grdPangkat.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdPangkat.Rows)
            {
                string index = grdPangkat.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdPangkat.Rows[gvrow.RowIndex].FindControl("chkCheck");
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