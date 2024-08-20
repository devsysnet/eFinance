using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace eFinance.Pages.Master.View
{
    public partial class MstCurrencyView : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        private Database ObjDb = new Database();
        private Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            LoadDataSearch();
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
            this.ShowHideGridAndForm(true, false, false);
        }
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
        }

        protected void cmdMode_Click(object sender, EventArgs e)
        {
            try
            {
                ObjDb.Where.Clear();
                ObjDb.Data.Clear();
                ObjDb.Where.Add("noMataUang", hdnId.Value);
                ClearData();

                if (hdnMode.Value.ToLower() == "edit")
                {
                    DataSet mySet = ObjDb.GetRows("SELECT * From mMataUang WHERE noMataUang = '" + hdnId.Value + "'");
                    DataRow myRow = mySet.Tables[0].Rows[0];

                    txtCurrencyName.Text = myRow["namaMataUang"].ToString();
                    txtCode.Text = myRow["kodeMataUang"].ToString();
                    txtCountry.Text = myRow["Negara"].ToString();
                    hdnSts.Value = myRow["stsMataUang"].ToString();

                    if(hdnSts.Value == "1")
                    {
                        lblSts.Text = "Aktif";
                    }
                    else
                    {
                        lblSts.Text = "No";
                    }
                    this.ShowHideGridAndForm(false, true, false);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
        }

        protected void grdCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCurrency.PageIndex = e.NewPageIndex;
            LoadDataSearch();
        }
    }
}