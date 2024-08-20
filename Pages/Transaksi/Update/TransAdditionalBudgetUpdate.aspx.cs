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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransAdditionalBudgetUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                loadData();
            }
        }
        protected void loadData()
        {
            grdBudget.DataSource = ObjDb.GetRows("select * from tAdditinalBudget a inner join mRekening b on a.norek = b.noRek where b.ket LIKE '%" + txtSearch.Text + "%'");
            grdBudget.DataBind();
        }
        protected void grdBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBudget.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdBudget_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdBudget.SelectedRow.RowIndex;
                string noKurs = grdBudget.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKurs;
                DataSet MySet = ObjDb.GetRows("select * from tAdditinalBudget a inner join mRekening b on a.norek = b.noRek where a.noAdditionalBudget = '" + noKurs + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    dtAdd.Text = Convert.ToDateTime(MyRow["tgl"].ToString()).ToString("dd-MMM-yyyy");
                    txtNilai.Text = MyRow["nilai"].ToString();
                    txtRekening.Text = MyRow["kdRek"].ToString();
                    txtRemark.Text = MyRow["remaks"].ToString();
                    hdnNoRek.Value = MyRow["norek"].ToString();

                    CloseMessage();
                    this.ShowHideGridAndForm(false, true);
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
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
                            ObjDb.Where.Add("noAdditionalBudget", itemRow);
                            ObjDb.Delete("tAdditinalBudget", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdBudget.Rows)
                    {
                        string itemId = grdBudget.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdBudget.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noAdditionalBudget", itemId);
                            ObjDb.Delete("tAdditinalBudget", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                loadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
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
        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdBudget.Rows)
                {
                    string index = grdBudget.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdBudget.Rows)
            {
                string index = grdBudget.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdBudget.Rows[gvrow.RowIndex].FindControl("chkCheck");
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

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (valid == true)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noAdditionalBudget", hdnId.Value);
                    ObjDb.Data.Add("tgl", dtAdd.Text);
                    ObjDb.Data.Add("norek", hdnNoRek.Value);
                    ObjDb.Data.Add("nocabag", ObjSys.GetCabangId);
                    ObjDb.Data.Add("nilai", Convert.ToDecimal(txtNilai.Text).ToString());
                    ObjDb.Data.Add("remaks", txtRemark.Text);
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                    ObjDb.Update("tAdditinalBudget", ObjDb.Data, ObjDb.Where);

                    ShowMessage("success", "Data berhasil disimpan.");
                    clearData();
                    this.ShowHideGridAndForm(true, false);
                    loadData();

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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }

        protected void btnResetRek_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
            loadData();
            clearData();
        }
        protected void LoadDataParent()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("search", txtSearch.Text);
            grdRekening.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataAdditional", ObjGlobal.Param);
            grdRekening.DataBind();
        }
        protected void imgButtonProduct_Click(object sender, ImageClickEventArgs e)
        {
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdRekening.PageIndex = e.NewPageIndex;
            LoadDataParent();
            dlgParentAccount.Show();
        }

        protected void grdRekening_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdRekening.SelectedRow.RowIndex;
                string Id = grdRekening.DataKeys[rowIndex].Values[0].ToString();
                string kdRek = grdRekening.SelectedRow.Cells[1].Text;

                hdnNoRek.Value = Id;
                txtRekening.Text = kdRek;
                LoadDataParent();
                dlgParentAccount.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
        protected void clearData()
        {
            txtRemark.Text = "";
            txtRekening.Text = "";
            txtNilai.Text = "";
            dtAdd.Text = Convert.ToDateTime(ObjSys.GetNow).ToString("dd-MMM-yyyy");
            hdnNoRek.Value = "";


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            LoadDataParent();
            dlgParentAccount.Show();
        }
    }
}