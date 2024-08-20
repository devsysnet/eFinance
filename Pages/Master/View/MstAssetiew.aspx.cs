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

namespace eFinance.Pages.Master.View
{
    public partial class MstAssetiew : System.Web.UI.Page
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
            grdAsset.DataSource = ObjDb.GetRows("select nomAsset,CASE kategori WHEN 1 THEN 'Bangunan' WHEN 2 THEN 'Non Bangunan' END kategori,kelompok,tahun,CASE jenis WHEN 1 THEN 'Garis Lurus' WHEN 2 THEN 'Saldo Menurun' END jenis,tarif from mAsset where kelompok LIKE '%" + txtSearch.Text + "%'");
            grdAsset.DataBind();
        }

        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdAsset.Rows)
                {
                    string index = grdAsset.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdAsset.Rows)
            {
                string index = grdAsset.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdAsset.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
        protected void grdAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAsset.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void grdAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdAsset.SelectedRow.RowIndex;
                string noKurs = grdAsset.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKurs;
                DataSet MySet = ObjDb.GetRows("select * from mAsset where nomAsset = '" + noKurs + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    cboBangunan.Text = MyRow["kategori"].ToString();
                    cboJenis.Text = MyRow["jenis"].ToString();
                    txtKelompok.Text = MyRow["kelompok"].ToString();
                    txtMasa.Text = MyRow["tahun"].ToString();
                    txtTarif.Text = MyRow["tarif"].ToString();

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void cmdMode_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        if (hdnMode.Value.ToLower() == "deleteall")
        //        {
        //            /*DELETE ALL SELECTED*/

        //            ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
        //            if (arrayItem != null)
        //            {
        //                foreach (string itemRow in arrayItem)
        //                {
        //                    ObjDb.Where.Clear();
        //                    ObjDb.Where.Add("nomAsset", itemRow);
        //                    ObjDb.Delete("mAsset", ObjDb.Where);
        //                }
        //            }
        //            foreach (GridViewRow gvrow in grdAsset.Rows)
        //            {
        //                string itemId = grdAsset.DataKeys[gvrow.RowIndex].Value.ToString();
        //                CheckBox chkCheck = (CheckBox)grdAsset.Rows[gvrow.RowIndex].FindControl("chkCheck");
        //                if (chkCheck.Checked == true)
        //                {
        //                    ObjDb.Where.Clear();
        //                    ObjDb.Where.Add("nomAsset", itemId);
        //                    ObjDb.Delete("mAsset", ObjDb.Where);
        //                }
        //            }
        //        }
        //        /*END DELETE ALL SELECTED*/
        //        loadData();
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("success", "Data berhasil dihapus.");
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
        //        ShowMessage("error", ex.ToString());
        //    }
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
        protected void clearData()
        {
            cboJenis.Text = "1";
            cboBangunan.Text = "1";
            txtKelompok.Text = "";
            txtMasa.Text = "";
            txtTarif.Text = "";
        }
      
        protected void btnReset_Click(object sender, EventArgs e)
        {
            loadData();
            this.ShowHideGridAndForm(true, false);
        }
    }
}