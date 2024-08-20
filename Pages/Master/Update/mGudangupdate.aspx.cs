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
    public partial class mGudangupdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("mGudangupdate.aspx");
                LoadData();
            }
        }
        protected void LoadData()
        {

            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            grdGudang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataUpdateGudang", ObjGlobal.Param);
            grdGudang.DataBind();
        }

        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdGudang.Rows)
                {
                    string index = grdGudang.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdGudang.Rows)
            {
                string index = grdGudang.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdGudang.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                    /*DELETE ALL SELECTED*/

                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noGudang", itemRow);
                            ObjDb.Delete("gudang", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdGudang.Rows)
                    {
                        string itemId = grdGudang.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdGudang.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noGudang", itemId);
                            ObjDb.Delete("gudang", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                LoadData();
                this.ShowHideGridAndForm(true, false);
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string message = "";
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("noGudang", hdnId.Value);
                ObjDb.Data.Add("namaGudang", txtNamaGudang.Text);
                ObjDb.Data.Add("ketGudang", txtKet.Text);
                ObjDb.Data.Add("stsGudang", cboJenisGudang.Text);
                ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                ObjDb.Update("Gudang", ObjDb.Data, ObjDb.Where);

                ClearData();
                LoadData();
                ShowMessage("success", "Data berhasil diupdate.");
                ShowHideGridAndForm(true, false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false);
        }

        protected void grdGudang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGudang.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void grdGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdGudang.SelectedRow.RowIndex;
                string noGudang = grdGudang.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noGudang;
                DataSet MySet = ObjDb.GetRows("select * from gudang where noGudang='" + noGudang + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];

                    txtNamaGudang.Text = MyRow["namaGudang"].ToString();
                    txtKet.Text = MyRow["ketGudang"].ToString();
                    cboJenisGudang.Text = MyRow["stsGudang"].ToString();
                    ShowHideGridAndForm(false, true);
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