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
    public partial class AdmFileSetup : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("AdmFileSetup.aspx");        
                LoadData();
            }
        }
        protected void LoadData()
        {
            cboMenuOne.DataSource = ObjDb.GetRows("select a.* from (select '' id,'-------' name union all select noMenu id, namaMenu name from menu where tingkatMenu = '2') a");
            cboMenuOne.DataValueField = "id";
            cboMenuOne.DataTextField = "name";
            cboMenuOne.DataBind();

            grdMenu.DataSource = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '4' and parentMenu = '000000000000'");
            grdMenu.DataBind();
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

        protected void cboMenuOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMenuTwo.DataSource = ObjDb.GetRows("select a.* from (select '' id,'-------' name union all select noMenu id, namaMenu name from menu where tingkatMenu = '3' and parentMenu = '" + cboMenuOne.SelectedValue + "') a");
            cboMenuTwo.DataValueField = "id";
            cboMenuTwo.DataTextField = "name";
            cboMenuTwo.DataBind();
        }
        protected void cboMenuTwo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdMenu.DataSource = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '4' and parentMenu = '" + cboMenuTwo.SelectedValue + "' order by noUrut asc");
            grdMenu.DataBind();
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            DataSet Data = null;
            if (cboMenuTwo.Text != "0")
            {
                ObjDb.ExecQuery("insert into menu(noUrut,namaMenu,judulMenu,parentMenu,namaFileMenu,iconMenu,siteMapMenu,tingkatMenu,createdBy,createdDate)values(99,null,null,'" + cboMenuTwo.Text + "',null,null,null,'4','" + ObjSys.GetUserId + "',GETDATE())");
                Data = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '4' and parentMenu = '" + cboMenuTwo.SelectedValue + "' order by noUrut asc");
            }
            else
            {
                ObjDb.ExecQuery("insert into menu(noUrut,namaMenu,judulMenu,parentMenu,namaFileMenu,iconMenu,siteMapMenu,tingkatMenu,createdBy,createdDate)values(99,null,null,'" + cboMenuOne.Text + "',null,null,null,'3','" + ObjSys.GetUserId + "',GETDATE())");
                Data = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '3' and parentMenu = '" + cboMenuOne.Text + "' order by noUrut asc");
            }
            grdMenu.DataSource = Data;
            grdMenu.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool valid = true;
            string message = "";


            try
            {
                int countValid = 0, countSelect = 0;
                for (int i = 0; i < grdMenu.Rows.Count; i++)
                {
                    CheckBox chkCheck = (CheckBox)grdMenu.Rows[i].FindControl("chkCheck");
                    if (chkCheck.Checked == true)
                    {
                        countSelect++;
                    }
                }
                if (valid == true)
                {
                    if (countSelect == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Tidak ada data yang dipilih.");
                    }
                    else
                    {
                        for (int i = 0; i < grdMenu.Rows.Count; i++)
                        {
                            string itemId = grdMenu.DataKeys[i].Value.ToString();
                            CheckBox chkCheck = (CheckBox)grdMenu.Rows[i].FindControl("chkCheck");
                            if (chkCheck.Checked == true)
                            {
                                TextBox txtSortNumber = (TextBox)grdMenu.Rows[i].FindControl("txtSortNumber");
                                TextBox txtMenuName = (TextBox)grdMenu.Rows[i].FindControl("txtMenuName");
                                TextBox txtMenuTitle = (TextBox)grdMenu.Rows[i].FindControl("txtMenuTitle");
                                TextBox txtSiteMap = (TextBox)grdMenu.Rows[i].FindControl("txtSiteMap");
                                TextBox txtUrlMenu = (TextBox)grdMenu.Rows[i].FindControl("txtUrlMenu");
                                TextBox txtIcon = (TextBox)grdMenu.Rows[i].FindControl("txtIcon");
                                DropDownList cboSubMenu = (DropDownList)grdMenu.Rows[i].FindControl("cboSubMenu");
                                if (cboMenuTwo.Text != "0")
                                    ObjDb.ExecQuery("UPDATE menu SET noUrut = '" + txtSortNumber.Text + "',namaMenu='" + txtMenuName.Text.ToUpper() + "',judulMenu='" + txtMenuTitle.Text + "',parentMenu='" + cboSubMenu.Text + "',namaFileMenu='" + txtUrlMenu.Text + "',iconMenu='" + txtIcon.Text + "',siteMapMenu='" + txtSiteMap.Text + "' WHERE noMenu = '" + itemId + "'");
                                else
                                    ObjDb.ExecQuery("UPDATE menu SET noUrut = '" + txtSortNumber.Text + "',namaMenu='" + txtMenuName.Text.ToUpper() + "',judulMenu='" + txtMenuTitle.Text + "',namaFileMenu='" + txtUrlMenu.Text + "',iconMenu='" + txtIcon.Text + "',siteMapMenu='" + txtSiteMap.Text + "' WHERE noMenu = '" + itemId + "'");

                            }
                        }
                        grdMenu.DataSource = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '4' and parentMenu = '" + cboMenuTwo.SelectedValue + "' order by noUrut asc");
                        grdMenu.DataBind();
                    }
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdMenu.Rows.Count; i++)
            {
                string itemId = grdMenu.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdMenu.Rows[i].FindControl("chkCheck");
                if (chkCheck.Checked == true)
                    ObjDb.ExecQuery("DELETE FROM menu WHERE noMenu = '" + itemId + "'");
            }
            grdMenu.DataSource = ObjDb.GetRows("SELECT * FROM menu WHERE tingkatMenu = '4' and parentMenu = '" + cboMenuTwo.SelectedValue + "' order by noUrut asc");
            grdMenu.DataBind();
        }

        protected void grdMenu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnNoParent = (HiddenField)e.Row.FindControl("hdnNoParent");
                DropDownList cboSubMenu = (DropDownList)e.Row.FindControl("cboSubMenu");

                cboSubMenu.DataSource = ObjDb.GetRows("select a.* from (select '' id,'-------' name union all select noMenu id, namaMenu name from menu where tingkatMenu = '3') a");
                cboSubMenu.DataValueField = "id";
                cboSubMenu.DataTextField = "name";
                cboSubMenu.DataBind();

                cboSubMenu.Text = hdnNoParent.Value;
            }
        }
    }
}