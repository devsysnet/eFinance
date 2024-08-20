using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections;

namespace eFinance.Pages.Master.View
{
    public partial class MstMeasurementView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                loadData();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
        
        }
        private void SetPreviousData()
        {
            
        }
        private void AddNewRow()
        {
           
        }

        private void loadData()
        {
            grdGudang.DataSource = ObjDb.GetRows("select * from mMesurement WHERE mesurement LIKE '%" + txtSearch.Text + "%'");
            grdGudang.DataBind();
        }
        protected void grdGudang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGudang.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            //tabForm.Visible = DivForm;
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

        protected void grdGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
          
        }
        private void clearData()
        {
        
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}