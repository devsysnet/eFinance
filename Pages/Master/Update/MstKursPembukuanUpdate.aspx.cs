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
    public partial class MstKursPembukuanUpdate : System.Web.UI.Page
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
        private void SetInitialRow(string noKurs = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            DataSet mySet = ObjDb.GetRows("select * from tKursPembukuan a inner join mMataUang b on a.noMataUang = b.noMataUang where a.noKursBuku = '" + noKurs + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["nilaiKursBuku"].ToString();
                dr["Column2"] = myRow["kodeMataUang"].ToString();
                dr["Column3"] = myRow["noMataUang"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["Column2"] = string.Empty;
                dr["Column3"] = string.Empty;

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdInstansi.DataSource = dt;
            grdInstansi.DataBind();

            SetPreviousData();
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
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        Label lblCurrency = (Label)grdInstansi.Rows[i].FindControl("lblCurrency");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");

                        txtInstansi.Text = dt.Rows[i]["Column1"].ToString();
                        lblCurrency.Text = dt.Rows[i]["Column2"].ToString();
                        hdnCurrency.Value = dt.Rows[i]["Column3"].ToString();
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
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        Label lblCurrency = (Label)grdInstansi.Rows[i].FindControl("lblCurrency");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtInstansi.Text;
                        dtCurrentTable.Rows[i]["Column2"] = lblCurrency.Text;
                        dtCurrentTable.Rows[i]["Column3"] = hdnCurrency.Value;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }

        protected void loadData()
        {
            grdKursPembukuan.DataSource = ObjDb.GetRows("select * from tKursPembukuan a inner join mMataUang b on a.noMataUang = b.noMataUang where b.kodeMataUang LIKE '%" + txtSearch.Text + "%'");
            grdKursPembukuan.DataBind();
        }

        protected void grdKursPembukuan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKursPembukuan.PageIndex = e.NewPageIndex;
            loadData();
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

        protected void grdKursPembukuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdKursPembukuan.SelectedRow.RowIndex;
                string noKurs = grdKursPembukuan.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKurs;
                DataSet MySet = ObjDb.GetRows("select MONTH(a.tglKursBuku) as Bulan, YEAR(a.tglKursBuku) as tahun from tKursPembukuan a inner join mMataUang b on a.noMataUang = b.noMataUang where a.noKursBuku = '" + noKurs + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    cboBulan.Text = MyRow["Bulan"].ToString();
                    cboTahun.Text = MyRow["tahun"].ToString();

                    SetInitialRow(noKurs);

                    CloseMessage();
                    this.ShowHideGridAndForm(false, true);
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "Calculate", "Calculate();", true);
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
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdKursPembukuan.Rows)
                {
                    string index = grdKursPembukuan.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdKursPembukuan.Rows)
            {
                string index = grdKursPembukuan.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdKursPembukuan.Rows[gvrow.RowIndex].FindControl("chkCheck");
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

        protected void btnSearch_Click(object sender, EventArgs e)
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
                            ObjDb.Where.Add("noKursBuku", itemRow);
                            ObjDb.Delete("tKursPembukuan", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdKursPembukuan.Rows)
                    {
                        string itemId = grdKursPembukuan.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdKursPembukuan.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noKursBuku", itemId);
                            ObjDb.Delete("tKursPembukuan", ObjDb.Where);
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

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        HiddenField hdnCurrency = (HiddenField)grdInstansi.Rows[i].FindControl("hdnCurrency");
                        DataSet mySet = ObjDb.GetRows("Select * from tKursPembukuan where tglKursBuku = '" + cboTahun.Text + "-" + cboBulan.Text + "-01" + "' and noMataUang='" + hdnCurrency.Value + "'");
                        if (mySet.Tables[0].Rows.Count > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                            ShowMessage("error", "Data sudah terdaftar.");
                            //string error = Convert.ToDecimal(txtIdUser.Text).ToString();
                        }
                        else
                        {
                            if (txtInstansi.Text != "")
                            {
                                ObjDb.Data.Clear();
                                ObjDb.Where.Clear();
                                ObjDb.Where.Add("noKursBuku", hdnId.Value);
                                ObjDb.Data.Add("noMataUang", hdnCurrency.Value);
                                ObjDb.Data.Add("tglKursBuku", cboTahun.Text + "-" + cboBulan.Text + "-01");
                                ObjDb.Data.Add("nilaiKursBuku", txtInstansi.Text);
                                ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                                ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                                ObjDb.Update("tKursPembukuan", ObjDb.Data, ObjDb.Where);

                                ShowMessage("success", "Data berhasil disimpan.");
                            }
                        }
                    }
                    //clearData();
                    this.ShowHideGridAndForm(true, false);
                    loadData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            loadData();
            this.ShowHideGridAndForm(true, false);
        }

    }
}