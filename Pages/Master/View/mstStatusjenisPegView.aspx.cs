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
    public partial class mstStatusjenisPegView1 : System.Web.UI.Page
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
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();
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
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");

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
                        TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtAgama.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }

        private void loadData()
        {
            grdGudang.DataSource = ObjDb.GetRows("select * from Mmststatuspegawai WHERE statuspegawai LIKE '%" + txtSearch.Text + "%'");
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


                grdInstansi.DataSource = ObjDb.GetRows("select a.*,b.ket,b.kdrek from Mmststatuspegawai a inner join mrekening b on a.norek=b.norek WHERE statuspegawai ='" + hdnId.Value + "'");
                grdInstansi.DataBind();

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
                            ObjDb.Where.Add("statuspegawai", itemRow);
                            ObjDb.Delete("Mmststatuspegawai", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdGudang.Rows)
                    {
                        string itemId = grdGudang.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdGudang.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("statuspegawai", itemId);
                            ObjDb.Delete("Mmststatuspegawai", ObjDb.Where);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;


            try
            {

                TextBox txtAgama = (TextBox)grdInstansi.Rows[0].FindControl("txtAgama");
                HiddenField hdnAgama = (HiddenField)grdInstansi.Rows[0].FindControl("hdnAgama");
                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[0].FindControl("hdnAccount");


                ObjDb.Data.Clear();
                ObjDb.Where.Clear();
                ObjDb.Where.Add("statuspegawai", hdnAgama.Value);
                ObjDb.Data.Add("statuspegawai", txtAgama.Text);
                ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                ObjDb.Data.Add("norek", hdnAccount.Value);
                ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                ObjDb.Update("Mmststatuspegawai", ObjDb.Data, ObjDb.Where);



                this.ShowHideGridAndForm(true, false);
                ShowMessage("success", "Data berhasil diupdate.");
                clearData();
                loadData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }

        }
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                TextBox txtAgama = (TextBox)grdInstansi.Rows[i].FindControl("txtAgama");
                HiddenField hdnAgama = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAgama");
                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                TextBox kdRekBank = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                Label lblDescription = (Label)grdInstansi.Rows[i].FindControl("lblDescription");
                txtAgama.Text = "";
                hdnAccount.Value = "";
                kdRekBank.Text = "";
                lblDescription.Text = "";

            }
        }
        protected void LoadDataBank(string kdRek = "")
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", kdRek);

            ObjGlobal.Param.Add("jenisRekening", "0");

            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningnew", ObjGlobal.Param);
            grdBank.DataBind();
        }

        protected void txtAccount_TextChanged(object sender, EventArgs e)
        {
            autoComplete();
        }
        protected void autoComplete()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[i].FindControl("hdnAccount");
                TextBox kdRekBank = (TextBox)grdInstansi.Rows[i].FindControl("txtAccount");
                Label lblDescription = (Label)grdInstansi.Rows[i].FindControl("lblDescription");

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("kdRek", kdRekBank.Text.Replace(" ", ""));
                DataSet mySet = ObjGlobal.GetDataProcedure("SPmrekening", ObjGlobal.Param);

                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    kdRekBank.Text = myRow["kdRek"].ToString();
                    hdnAccount.Value = myRow["noRek"].ToString();
                    lblDescription.Text = myRow["Ket"].ToString();

                }
            }
        }
        protected void loadDataSearch()
        {
            ObjGlobal.Param.Clear();
            //ObjGlobal.Param.Add("noRekInduk", cboAccount.Text);
            ObjGlobal.Param.Add("Search", txtSearch1.Text);

            ObjGlobal.Param.Add("jenisRekening", "0");

            grdBank.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataRekeningnew", ObjGlobal.Param);
            grdBank.DataBind();
        }
        protected void grdBank_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();

            grdBank.PageIndex = e.NewPageIndex;
            loadData();
            dlgBank.Show();
        }
        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            loadDataSearch();
            dlgBank.Show();
        }

        protected void grdBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string message = "";
                bool valid = true;

                int rowIndexHdn = Convert.ToInt32(hdnParameterProd.Value);
                int rowIndex = grdBank.SelectedRow.RowIndex;

                string kdRek = (grdBank.SelectedRow.FindControl("lblKdRek") as Label).Text;
                string Ket = (grdBank.SelectedRow.FindControl("lblKet") as Label).Text;
                string noRek = (grdBank.SelectedRow.FindControl("hdnNoRek") as HiddenField).Value;

                HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndexHdn].FindControl("hdnAccount");
                TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndexHdn].FindControl("txtAccount");
                Label lblDescription = (Label)grdInstansi.Rows[rowIndexHdn].FindControl("lblDescription");




                if (valid == true)
                {
                    hdnAccount.Value = noRek;
                    txtAccount.Text = kdRek;
                    lblDescription.Text = Ket;


                    txtSearch1.Text = "";
                    loadDataSearch();
                    dlgBank.Hide();

                    lblMessageError.Visible = false;
                }
                else
                {
                    dlgBank.Show();
                    lblMessageError.Text = ObjSys.GetMessage("error", message);
                    lblMessageError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }

        }
        protected void grdInstansi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnParameterProd.Value = rowIndex.ToString();
                if (e.CommandName == "Select")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccount");
                    if (txtAccount.Text != "")
                    {
                        CloseMessage();
                        LoadDataBank(txtAccount.Text);
                        dlgBank.Show();
                    }
                    if (txtAccount.Text == "")
                    {
                        CloseMessage();
                        loadDataSearch();
                        //string value = (grdKasBank.SelectedRow.FindControl("hdnParameter") as HiddenField).Value;
                        //hdnParameterProd.Value = value;
                        dlgBank.Show();
                    }


                }
                else if (e.CommandName == "Clear")
                {
                    TextBox txtAccount = (TextBox)grdInstansi.Rows[rowIndex].FindControl("txtAccount");
                    HiddenField hdnAccount = (HiddenField)grdInstansi.Rows[rowIndex].FindControl("hdnAccount");
                    Label lblDescription = (Label)grdInstansi.Rows[rowIndex].FindControl("lblDescription");

                    txtAccount.Text = "";
                    hdnAccount.Value = "";
                    lblDescription.Text = "";

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}