using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class AdmRoleAkses : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("AdmRoleAkses.aspx");
                LoadData();
            }
        }

        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdAkses.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRoleAccess", ObjGlobal.Param);
            grdAkses.DataBind();
        }
        protected void LoadDataAddMenu()
        {
            grdAddMenu.DataSource = ObjDb.GetRows("select * from menu where noMenu not in (select noMenu from tHakMenu where noAkses = '" + hdnId.Value + "') and tingkatMenu in (1,2) order by noUrut asc");
            grdAddMenu.DataBind();
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
        protected void ShowHideGridForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        protected void SaveData()
        {
            ObjDb.ExecQuery("delete a from tHakMenu a inner join menu b on a.noMenu = b.noMenu inner join menu c on b.parentMenu = c.noMenu where a.noAkses = '" + hdnId.Value + "' and c.parentMenu = '" + hdnNoMenu.Value + "'");
            ObjDb.ExecQuery("delete a from tHakMenu a inner join menu b on a.noMenu = b.noMenu where a.noAkses = '" + hdnId.Value + "' and b.parentMenu = '" + hdnNoMenu.Value + "'");
            for (int i = 0; i < grdMenuTwo.Rows.Count; i++)
            {
                string itemId = grdMenuTwo.DataKeys[i].Value.ToString();
                GridView grdChildMenuTwo = grdMenuTwo.Rows[i].FindControl("grdChildMenuTwo") as GridView;
                if (grdChildMenuTwo.Rows.Count > 0)
                {
                    int cekCount = 0;
                    for (int a = 0; a < grdChildMenuTwo.Rows.Count; a++)
                    {
                        string itemId2 = grdChildMenuTwo.DataKeys[a].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdChildMenuTwo.Rows[a].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.ExecQuery("INSERT INTO tHakMenu (noAkses,noMenu)VALUES('" + hdnId.Value + "','" + itemId2 + "')");
                            cekCount++;
                        }
                    }
                    if (cekCount > 0)
                        ObjDb.ExecQuery("INSERT INTO tHakMenu (noAkses,noMenu)VALUES('" + hdnId.Value + "','" + itemId + "')");
                }
                else
                {
                    CheckBox chkCheck = (CheckBox)grdMenuTwo.Rows[i].FindControl("chkCheck");
                    if (chkCheck.Checked == true)
                        ObjDb.ExecQuery("INSERT INTO tHakMenu (noAkses,noMenu)VALUES('" + hdnId.Value + "','" + itemId + "')");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            LoadData();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            ShowHideGridForm(true, false);
            CloseMessage();
        }
        protected void btnAddMenu_Click(object sender, EventArgs e)
        {
            LoadDataAddMenu();
            dlgAddMenu.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                SaveData();
                ShowMessage("success", "Data has been save.");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnSubmitMenu_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdAddMenu.Rows.Count; i++)
            {
                string itemId = grdAddMenu.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdAddMenu.Rows[i].FindControl("chkCheck");
                if (chkCheck.Checked == true)
                {
                    ObjDb.ExecQuery("INSERT INTO tHakMenu (noAkses,noMenu)VALUES('" + hdnId.Value + "','" + itemId + "')");
                }
            }
            grdMenu.DataSource = ObjDb.GetRows("select b.* from tHakMenu  a inner join menu b on a.noMenu = b.noMenu where a.noAkses = '" + hdnId.Value + "' and b.tingkatMenu in (1,2) order by noUrut asc");
            grdMenu.DataBind();
        }

        protected void grdAkses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CloseMessage();

                if (e.CommandName == "DetailRow")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string itemId = grdAkses.DataKeys[rowIndex].Value.ToString();
                    hdnId.Value = itemId;
                    hdnNoMenu.Value = "99999";
                    lblHakMenu.Text = grdAkses.Rows[rowIndex].Cells[1].Text;
                    lblKeterangan.Text = grdAkses.Rows[rowIndex].Cells[3].Text;
                    grdMenu.DataSource = ObjDb.GetRows("select b.* from tHakMenu  a inner join menu b on a.noMenu = b.noMenu where a.noAkses = '" + itemId + "' and b.tingkatMenu in (1,2) order by noUrut asc");
                    grdMenu.DataBind();
                    grdMenuTwo.DataSource = ObjDb.GetRows("select a.*, case when b.noAkses is null then 0 else b.noAkses end noAkses from menu a left join (select * from tHakMenu where noAkses = '" + itemId + "') b on a.noMenu = b.noMenu where a.parentMenu = '001'  order by a.noUrut asc");
                    grdMenuTwo.DataBind();
                    ShowHideGridForm(false, true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", "Invalid transaction data.");
            }
        }
        protected void grdAkses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAkses.PageIndex = e.NewPageIndex;
            LoadData();
        }
        protected void grdMenuTwo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string itemId = grdMenuTwo.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView grdChildMenuTwo = e.Row.FindControl("grdChildMenuTwo") as GridView;
                grdChildMenuTwo.DataSource = ObjDb.GetRows("select a.*, case when b.noAkses is null then 0 else b.noAkses end noAkses from menu a left join (select * from tHakMenu where noAkses = '" + hdnId.Value + "') b on a.noMenu = b.noMenu where a.parentMenu = '" + itemId + "'  order by a.noUrut asc");
                grdChildMenuTwo.DataBind();

                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");
                if (grdChildMenuTwo.Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;


                HiddenField hdnNoAkses = (HiddenField)e.Row.FindControl("hdnNoAkses");
                if (hdnNoAkses.Value == "0")
                    chkCheck.Checked = false;
                else
                    chkCheck.Checked = true;
            }
        }
        protected void grdChildMenuTwo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkCheck = (CheckBox)e.Row.FindControl("chkCheck");
                HiddenField hdnNoAkses = (HiddenField)e.Row.FindControl("hdnNoAkses");
                if (hdnNoAkses.Value == "0")
                    chkCheck.Checked = false;
                else
                    chkCheck.Checked = true;
            }
        }
        protected void grdMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                CloseMessage();

                if (e.CommandName == "SelectRow")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    hdnNoMenu.Value = grdMenu.DataKeys[rowIndex].Value.ToString();
                    grdMenuTwo.DataSource = ObjDb.GetRows("select a.*, case when b.noAkses is null then 0 else b.noAkses end noAkses from menu a left join (select * from tHakMenu where noAkses = '" + hdnId.Value + "') b on a.noMenu = b.noMenu where a.parentMenu = '" + grdMenu.DataKeys[rowIndex].Value.ToString() + "' order by a.noUrut asc");
                    grdMenuTwo.DataBind();
                }
                else if (e.CommandName == "DeleteRow")
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string itemId = grdMenu.DataKeys[rowIndex].Value.ToString();
                    ObjDb.ExecQuery("delete a from tHakMenu a inner join menu b on a.noMenu = b.noMenu inner join menu c on b.parentMenu = c.noMenu where a.noAkses = '" + hdnId.Value + "' and c.parentMenu = '" + hdnNoMenu.Value + "'");
                    ObjDb.ExecQuery("delete a from tHakMenu a inner join menu b on a.noMenu = b.noMenu where a.noAkses = '" + hdnId.Value + "' and b.parentMenu = '" + hdnNoMenu.Value + "'");
                    ObjDb.ExecQuery("delete from tHakMenu where noAkses = '" + hdnId.Value + "' and noMenu = '" + itemId + "'");
                    grdMenu.DataSource = ObjDb.GetRows("select b.* from tHakMenu  a inner join menu b on a.noMenu = b.noMenu where a.noAkses = '" + hdnId.Value + "' and b.tingkatMenu in (1,2) order by noUrut asc");
                    grdMenu.DataBind();
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