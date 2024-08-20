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
    public partial class MstProductLocationUpdate1 : System.Web.UI.Page
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
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;
            grdProdLoc.DataSource = dt;
            grdProdLoc.DataBind();
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
                        DropDownList cboGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList box4 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");



                        cboGudang.Text = dt.Rows[i]["Column1"].ToString();
                        box4.Text = dt.Rows[i]["Column2"].ToString();
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
                        DropDownList box1 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList box4 = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i]["Column2"] = box4.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdProdLoc.DataSource = dtCurrentTable;
                    grdProdLoc.DataBind();
                }
            }
            SetPreviousData();
        }
        private void loadData()
        {
            grdVoucher.DataSource = ObjDb.GetRows("select distinct noproduct, prodno, prodnm, manufactur  from Product WHERE (prodno LIKE '%" + txtSearch.Text + "%' or prodnm LIKE '%" + txtSearch.Text + "%') and noproduct in (select noproduct mLokasiBarang)");
            grdVoucher.DataBind();
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
                            ObjDb.Where.Add("noproduct", itemRow);
                            ObjDb.Delete("mLokasiBarang", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdVoucher.Rows)
                    {
                        string itemId = grdVoucher.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdVoucher.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noproduct", itemId);
                            ObjDb.Delete("mLokasiBarang", ObjDb.Where);
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
        protected void clearData()
        {
            txtKapasitas.Text = "";
            txtMinStok.Text = "";
        }
        protected void cboGudang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cboGudang = (DropDownList)sender;
            var row = (GridViewRow)cboGudang.NamingContainer;

            DropDownList Gudang = (DropDownList)row.FindControl("cboGudang");
            DropDownList cboLokasiGudang = (DropDownList)row.FindControl("cboLokasiGudang");

            cboLokasiGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noLokasiGudang id, namaLokGud name FROM LokasiGudang where noGudang = '" + Gudang.SelectedValue + "' ) a");
            cboLokasiGudang.DataValueField = "id";
            cboLokasiGudang.DataTextField = "name";
            cboLokasiGudang.DataBind();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtKapasitas.Text == "")
            {
                message += ObjSys.CreateMessage("Kapasitas tidak boleh kosong.");
                valid = false;
            }
            if (txtMinStok.Text == "")
            {
                message += ObjSys.CreateMessage("Stok tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdProdLoc.Rows.Count; i++)
                    {
                        DropDownList cboGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboGudang");
                        DropDownList cboLokasiGudang = (DropDownList)grdProdLoc.Rows[i].FindControl("cboLokasiGudang");
                        HiddenField hdnNoGudang = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnNoGudang");
                        HiddenField hdnNoLokGud = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnNoLokGud");
                        HiddenField hdnNoProduct = (HiddenField)grdProdLoc.Rows[i].FindControl("hdnNoProduct");

                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();
                        ObjDb.Where.Add("noLokasiBarang", hdnNoProduct.Value);
                        ObjDb.Where.Add("noproduct", hdnId.Value);
                        ObjDb.Where.Add("noGudang", hdnNoGudang.Value);
                        ObjDb.Where.Add("noLokasiGudang", hdnNoLokGud.Value);
                        ObjDb.Data.Add("noLokasiGudang", cboLokasiGudang.Text);
                        ObjDb.Data.Add("noGudang", cboGudang.Text);
                        ObjDb.Data.Add("minSafety", txtMinStok.Text);
                        ObjDb.Data.Add("kapasitas", txtKapasitas.Text);
                        ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                        ObjDb.Update("mLokasiBarang", ObjDb.Data, ObjDb.Where);

                    }

                    ShowMessage("success", "Data berhasil disimpan.");
                    this.ShowHideGridAndForm(true, false, false);
                    clearData();
                    loadData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
            }
        }
        protected void grdProdLoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnNoGudang = (HiddenField)e.Row.FindControl("hdnNoGudang");
                DropDownList cboGudang = (DropDownList)e.Row.FindControl("cboGudang");
                HiddenField hdnNoLokGud = (HiddenField)e.Row.FindControl("hdnNoLokGud");
                DropDownList cboLokasiGudang = (DropDownList)e.Row.FindControl("cboLokasiGudang");

                cboGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noGudang id, namaGudang name FROM Gudang ) a");
                cboGudang.DataValueField = "id";
                cboGudang.DataTextField = "name";
                cboGudang.DataBind();

                cboLokasiGudang.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noLokasiGudang id, namaLokGud name FROM LokasiGudang where noGudang = '" + hdnNoGudang.Value + "' ) a");
                cboLokasiGudang.DataValueField = "id";
                cboLokasiGudang.DataTextField = "name";
                cboLokasiGudang.DataBind();

                cboGudang.Text = hdnNoGudang.Value;
                cboLokasiGudang.Text = hdnNoLokGud.Value;
            }
        }
        protected void grdVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdVoucher.SelectedRow.RowIndex;
                string noLokasiBarang = grdVoucher.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokasiBarang;
                CloseMessage();
                DataSet MySet = ObjDb.GetRows("select * from mLokasiBarang a inner join Gudang b on a.noGudang = b.noGudang inner join Product d on a.noProduct = d.noproduct WHERE a.noproduct ='" + noLokasiBarang + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    lblKodeBarang.Text = MyRow["prodno"].ToString();
                    lblNamaBarang.Text = MyRow["prodnm"].ToString();
                    lblJenis.Text = MyRow["groups"].ToString();
                    txtMinStok.Text = MyRow["minsafety"].ToString();
                    txtKapasitas.Text = MyRow["kapasitas"].ToString();

                    grdProdLoc.DataSource = ObjDb.GetRows("select * from mLokasiBarang a inner join Gudang b on a.noGudang = b.noGudang inner join Product d on a.noProduct = d.noproduct WHERE a.noproduct ='" + hdnId.Value + "'");
                    grdProdLoc.DataBind();
                    grdProdLoc.Enabled = false;
                    for (int i = 1; i < 5; i++)
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
            clearData();
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
        protected void grdVoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdVoucher.PageIndex = e.NewPageIndex;
            loadData();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadData();
        }
    }
}