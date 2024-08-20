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

namespace eFinance.Pages.Master.View
{
    public partial class MstPotongangajiView : System.Web.UI.Page
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
            grdPTKP.DataSource = ObjDb.GetRows("SELECT nopotonganterlambat,dari,ke,case when jns=1 then 'Persen' else 'Nilai' end as jenis,CAST(CONVERT(VARCHAR, CAST(nilai AS MONEY), 1) AS VARCHAR) as nilai,case when sts=1 then 'Aktif' else 'Tidak Aktif' end as sts FROM Mstpotonganterlambat a where a.nocabang='" + ObjSys.GetCabangId + "'");
            grdPTKP.DataBind();
        }
        protected void LoadDataSearch()
        {
            grdPTKP.DataSource = ObjDb.GetRows("SELECT nopotonganterlambat,dari,ke,case when jns=1 then 'Persen' else 'Nilai' end as jenis,CAST(CONVERT(VARCHAR, CAST(nilai AS MONEY), 1) AS VARCHAR) as nilai,case when sts=1 then 'Aktif' else 'Tidak Aktif' end as sts FROM Mstpotonganterlambat a WHERE a.PTKP like '%" + txtSearch.Text + "%' and a.nocabang='" + ObjSys.GetCabangId + "'");
            grdPTKP.DataBind();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
          
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            //tabGrid.Visible = DivGrid;
            //tabForm.Visible = DivForm;
            //tabView.Visible = DivView;
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

        }

        protected void grdPTKP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdPTKP.PageIndex = e.NewPageIndex;
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
                foreach (GridViewRow gvrow in grdPTKP.Rows)
                {
                    string index = grdPTKP.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdPTKP.Rows)
            {
                string index = grdPTKP.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdPTKP.Rows[gvrow.RowIndex].FindControl("chkCheck");
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