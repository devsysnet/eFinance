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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class MProjectupdate : System.Web.UI.Page
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
                //ObjSys.SessionCheck("MProjectupdate.aspx");
                //LoadData();
                //LoadDataCombo();
            }
        }

        protected void IndexPakai()
        {
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string itemId = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");


                DataSet mySet1 = ObjDb.GetRows("Select noProject from TransPR_H Where noProject = '" + itemId + "'");
                if (mySet1.Tables[0].Rows.Count > 0)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;

            }

        }

        protected void LoadDataCombo()
        {
            //cboParent.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'-' name union all SELECT distinct noCabang id, namaCabang name FROM mCabang where stscabang = '0' or stscabang = '1' ) a");
            //cboParent.DataValueField = "id";
            //cboParent.DataTextField = "name";
            //cboParent.DataBind();

        }
        protected void LoadData()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("nocabang", ObjSys.GetCabangId);
            grdCabang.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataMasterProject", ObjGlobal.Param);
            grdCabang.DataBind();
            IndexPakai();
        }

        protected void grdCabang_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues_View();
            grdCabang.PageIndex = e.NewPageIndex;
            LoadData();
            PopulateCheckedValues_View();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;

            if (valid == true)
            {
                try
                {
                    ObjGlobal.Param.Clear();
                    ObjGlobal.Param.Add("Id", hdnID.Value);
                    ObjGlobal.Param.Add("Project", txtNama.Text);
                    ObjGlobal.Param.Add("tglProject", Convert.ToDateTime(dtKas.Text).ToString("yyyy-MM-dd"));
                    ObjGlobal.Param.Add("noKontrak", Textnokontrak.Text);
                    ObjGlobal.Param.Add("keterangan", txtUraian.Text);
                    ObjGlobal.Param.Add("modifiedBy", ObjSys.GetCabangId);
                    ObjGlobal.GetDataProcedure("SPUpdateProject", ObjGlobal.Param);

                    ClearData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearData();
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

        protected void ClearData()
        {
            txtNama.Text = "";
            dtKas.Text = "";
            Textnokontrak.Text = "";
            txtUraian.Text = "";
            LoadData();
            CloseMessage();
            showHideForm(true, false);
        }

        protected void showHideForm(bool DivGrid, bool DivForm)
        {
            tabGrid.Visible = DivGrid;
            tabForm.Visible = DivForm;
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void grdCabang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CloseMessage();
                int rowIndex = grdCabang.SelectedRow.RowIndex;
                hdnID.Value = grdCabang.DataKeys[rowIndex].Values[0].ToString();

                ObjGlobal.Param.Clear();
                ObjGlobal.Param.Add("Id", hdnID.Value);
                DataSet mySet = ObjGlobal.GetDataProcedure("SPLoadDatamProject_Detail", ObjGlobal.Param);
                DataRow myRow = mySet.Tables[0].Rows[0];

                txtNama.Text = myRow["Project"].ToString();
                dtKas.Text = myRow["tglProject"].ToString();
                Textnokontrak.Text = myRow["nokontrak"].ToString();
                txtUraian.Text = myRow["keterangan"].ToString();
                             
                CloseMessage();
                showHideForm(false, true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }

        private void PopulateCheckedValues_View()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdCabang.Rows)
                {
                    string index = grdCabang.DataKeys[gvrow.RowIndex].Value.ToString();
                    if (userdetails.Contains(index))
                    {
                        CheckBox myCheckBox = (CheckBox)gvrow.FindControl("chkCheck");
                        myCheckBox.Checked = true;
                    }
                }
            }
        }
        private void SaveCheckedValues_View()
        {
            ArrayList userdetails = new ArrayList();
            for (int i = 0; i < grdCabang.Rows.Count; i++)
            {
                string index = grdCabang.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");
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
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noProject", itemRow);
                            ObjDb.Delete("mProject", ObjDb.Where);
                        }
                    }
                    for (int i = 0; i < grdCabang.Rows.Count; i++)
                    {
                        string itemId = grdCabang.DataKeys[i].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdCabang.Rows[i].FindControl("chkCheck");

                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noProject", itemId);
                            ObjDb.Delete("mProject", ObjDb.Where);
                        }
                    }
                    /*END DELETE ALL SELECTED*/
                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil dihapus.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
        }
    }
}