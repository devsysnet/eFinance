using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;

namespace eFinance.Pages.Master.Update
{
    public partial class MstCurrencyUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstCurrencyUpdate.aspx");
                execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
                LoadDataSearch();
            }
        }
        protected void ClearData()
        {
            txtCurrencyName.Text = "";
            txtCode.Text = "";
            txtCountry.Text = "";
        }
        protected void LoadDataSearch()
        {
            grdCurrency.DataSource = ObjDb.GetRows("select * from mMataUang WHERE kodeMataUang like '%" + txtSearch.Text + "%'");
            grdCurrency.DataBind();
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
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdCurrency.Rows)
                {
                    string index = grdCurrency.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdCurrency.Rows)
            {
                string index = grdCurrency.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCurrency.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }

        protected void grdCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCurrency.PageIndex = e.NewPageIndex;
            LoadDataSearch();
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnMode.Value.ToLower() == "deleteall")
                {
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noMataUang", itemRow);
                            ObjDb.Delete("mMataUang", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCurrency.Rows.Count; i++)
                    {
                        string itemId = grdCurrency.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCurrency.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noMataUang", itemId);
                            ObjDb.Delete("mMataUang", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                LoadDataSearch();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            ObjDb.Where.Clear();
            ObjDb.Where.Add("noMataUang", hdnId.Value);

            if (txtCurrencyName.Text == "")
            {
                message += ObjSys.CreateMessage("Currency Name Tidak Boleh Kosong.");
                valid = false;
            }
            if (txtCode.Text == "")
            {
                message += ObjSys.CreateMessage("Code Tidak Boleh Kosong.");
                valid = false;
            }
            if (txtCountry.Text == "")
            {
                message += ObjSys.CreateMessage("Country Tidak Boleh Kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    
                        ObjDb.Where.Clear();
                        ObjDb.Data.Clear();
                        ObjDb.Where.Add("noMataUang", hdnId.Value);
                        ObjDb.Data.Add("namaMataUang", txtCurrencyName.Text);
                        ObjDb.Data.Add("kodeMataUang", txtCode.Text);
                        ObjDb.Data.Add("Negara", txtCountry.Text);
                        ObjDb.Data.Add("stsMataUang", cboStatus.Text);
                        ObjDb.Data.Add("stsDefault", cboSet.Text);
                        ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);

                        ObjDb.Update("mMataUang", ObjDb.Data, ObjDb.Where);
                    

                    this.ShowHideGridAndForm(true, false, false);

                    LoadDataSearch();
                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data to database.");
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
            ClearData();
            this.ShowHideGridAndForm(true, false, false);
        }

        protected void grdCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdCurrency.SelectedRow.RowIndex;
                string noQoutation = grdCurrency.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noQoutation;

                DataSet MySet = ObjDb.GetRows("SELECT * From mMataUang WHERE noMataUang = '" + noQoutation + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow myRow = MySet.Tables[0].Rows[0];

                    txtCurrencyName.Text = myRow["namaMataUang"].ToString();
                    txtCode.Text = myRow["kodeMataUang"].ToString();
                    txtCountry.Text = myRow["Negara"].ToString();
                    cboStatus.SelectedValue = myRow["stsMataUang"].ToString();

                    this.ShowHideGridAndForm(false, true, false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data Tidak ada.");
                }

            }
            catch (Exception ex)
            {
                if (valid == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", message);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
        }
    }
}