using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.View
{
    public partial class mstStatusjenisPegView : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            grdstatuspegawai.DataSource = dt;
            grdstatuspegawai.DataBind();
        }
        private void SetPreviousData()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtAgama = (TextBox)grdstatuspegawai.Rows[i].FindControl("txtAgama");

                        txtAgama.Text = dt.Rows[i]["Column1"].ToString();
                    }
                }
            }
        }
        private void AddNewRow()
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox txtAgama = (TextBox)grdstatuspegawai.Rows[i].FindControl("txtAgama");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAgama.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdstatuspegawai.DataSource = dtCurrentTable;
                    grdstatuspegawai.DataBind();
                }
            }
            SetPreviousData();
        }

        private void loadData()
        {
            grdGudang.DataSource = ObjDb.GetRows("select * from Mstjenispegawai WHERE statuspegawai LIKE '%" + txtSearch.Text + "%'");
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
            tabForm.Visible = DivForm;
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
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdGudang.SelectedRow.RowIndex;
                string noLokasiBarang = grdGudang.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokasiBarang;
                CloseMessage();


                grdstatuspegawai.DataSource = ObjDb.GetRows("select * from Mstjenispegawai WHERE statuspegawai ='" + hdnId.Value + "'");
                grdstatuspegawai.DataBind();

                for (int i = 1; i < 1; i++)
                {
                    AddNewRow();
                }
                this.ShowHideGridAndForm(false, true);
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

        private void clearData()
        {
            for (int i = 0; i < grdstatuspegawai.Rows.Count; i++)
            {
                TextBox txtAgama = (TextBox)grdstatuspegawai.Rows[i].FindControl("txtAgama");
                HiddenField hdnAgama = (HiddenField)grdstatuspegawai.Rows[i].FindControl("hdnAgama");

                txtAgama.Text = "";

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}