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
    public partial class MstVBankCodeUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();

        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                ObjSys.SessionCheck("MstVBankCodeUpdate.aspx");
                loadData();
            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdVBankCode.DataSource = dt;
            grdVBankCode.DataBind();
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
                        DropDownList box1 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                        Label hid1 = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                        Label box2 = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                        TextBox box3 = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                        DropDownList box4 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        hid1.Text = dt.Rows[i]["Column2"].ToString();
                        box2.Text = dt.Rows[i]["Column3"].ToString();
                        box3.Text = dt.Rows[i]["Column4"].ToString();
                        box4.Text = dt.Rows[i]["Column5"].ToString();
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
                        DropDownList box1 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                        Label hid1 = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                        Label box2 = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                        TextBox box3 = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                        DropDownList box4 = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = hid1.Text;
                        dtCurrentTable.Rows[i]["Column3"] = box2.Text;
                        dtCurrentTable.Rows[i]["Column4"] = box3.Text;
                        dtCurrentTable.Rows[i]["Column5"] = box4.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdVBankCode.DataSource = dtCurrentTable;
                    grdVBankCode.DataBind();
                }
            }
            SetPreviousData();
        }
        private void loadData()
        {
            grdVoucher.DataSource = ObjDb.GetRows("Select * from tbank where kdrek LIKE '%" + txtSearch.Text + "%' or kodeVoucher like '%" + txtSearch.Text + "%'");
            grdVoucher.DataBind();
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdVoucher.Rows)
                {
                    string index = grdVoucher.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdVoucher.Rows)
            {
                string index = grdVoucher.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdVoucher.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                            ObjDb.Where.Add("nobank", itemRow);
                            ObjDb.Delete("tbank", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdVoucher.Rows)
                    {
                        string itemId = grdVoucher.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdVoucher.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nobank", itemId);
                            ObjDb.Delete("tbank", ObjDb.Where);
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

        protected void ShowHideGridAndForm(bool DivGrid, bool DivForm, bool DivView)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
            tabView.Visible = DivView;
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

        protected void grdVBankCode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnCurrency = (HiddenField)e.Row.FindControl("hdnCurrency");
                DropDownList cboCurrency = (DropDownList)e.Row.FindControl("cboCurrency");
                HiddenField hdnKategori = (HiddenField)e.Row.FindControl("hdnKategori");
                DropDownList cboKategori = (DropDownList)e.Row.FindControl("cboKategori");

                cboCurrency.DataSource = ObjDb.GetRows("select a.* from (SELECT distinct noMataUang id, namaMataUang name FROM mMataUang ) a");
                cboCurrency.DataValueField = "id";
                cboCurrency.DataTextField = "name";
                cboCurrency.DataBind();

                cboCurrency.Text = hdnCurrency.Value;
                cboKategori.Text = hdnKategori.Value;
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();
            for (int i = 0; i < grdVBankCode.Rows.Count; i++)
            {
                HiddenField hdnNoBank = (HiddenField)grdVBankCode.Rows[i].FindControl("hdnNoBank");
                DropDownList cboCurrency = (DropDownList)grdVBankCode.Rows[i].FindControl("cboCurrency");
                Label lblCode = (Label)grdVBankCode.Rows[i].FindControl("lblCode");
                Label lblName = (Label)grdVBankCode.Rows[i].FindControl("lblName");
                TextBox txtBank = (TextBox)grdVBankCode.Rows[i].FindControl("txtBank");
                DropDownList cboKategori = (DropDownList)grdVBankCode.Rows[i].FindControl("cboKategori");

                if (lblCode.Text == "")
                {
                    message += ObjSys.CreateMessage("Account Code tidak boleh kosong.");
                    valid = false;
                }
                if (valid == true)
                {
                    try
                    {
                        if (txtBank.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nobank", hdnNoBank.Value);
                            ObjDb.Data.Add("noMataUang", cboCurrency.SelectedValue);
                            ObjDb.Data.Add("kodeVoucher", txtBank.Text);
                            ObjDb.Data.Add("stsKasBank", cboKategori.Text);
                            ObjDb.Data.Add("modiby", ObjSys.GetUserId);
                            ObjDb.Data.Add("modidate", ObjSys.GetNow);
                            ObjDb.Update("tBank", ObjDb.Data, ObjDb.Where);

                        }
                        ShowMessage("success", "Data berhasil disimpan.");
                        this.ShowHideGridAndForm(true, false, false);
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", ex.ToString());
                    }
                }
            }
        }

        protected void grdVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdVoucher.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdVoucher.SelectedRow.RowIndex;
                string nobank = grdVoucher.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = nobank;
                DataSet MySet = ObjDb.GetRows("select * from tbank where nobank='" + nobank + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    CloseMessage();
                    grdVBankCode.DataSource = ObjDb.GetRows("select * from tbank a inner join mRekening b on a.kdrek = b.kdRek WHERE nobank = '" + hdnId.Value + "'");
                    grdVBankCode.DataBind();

                    for (int i = 1; i < 1; i++)
                    {
                        AddNewRow();
                    }

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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false, false);
            loadData();
        }
    }
}