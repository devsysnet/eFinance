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

namespace eFinance.Pages.Transaksi.Update
{
    public partial class TransSaldoawalKasiUpdate : System.Web.UI.Page
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
        protected void IndexPakai()
        {
            for (int i = 0; i < grdSaldoGL.Rows.Count; i++)
            {
                string itemId = grdSaldoGL.DataKeys[i].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdSaldoGL.Rows[i].FindControl("chkCheck");

                DataSet mySet = ObjDb.GetRows("Select norek from tsaldokas Where nosldkas = '" + itemId + "'");
                if (mySet.Tables[0].Rows.Count > 1)
                    chkCheck.Visible = false;
                else
                    chkCheck.Visible = true;
            }

        }
       
        private void loadData()
        {
            grdSaldoGL.DataSource = ObjDb.GetRows("select a.nosldkas,a.kdRek,b.Ket,a.Tgl,a.Nilai from tSaldokas a inner join mRekening b on a.norek=b.noRek where a.sts=1 and a.jns=1 and a.noCabang='" + ObjSys.GetCabangId + "' and ket LIKE '%" + txtSearch.Text + "%' order by a.nosldkas");
            grdSaldoGL.DataBind();
            IndexPakai();
        }
        protected void grdSaldoGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSaldoGL.PageIndex = e.NewPageIndex;
            loadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
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

        protected void grdSaldoGL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdSaldoGL.SelectedRow.RowIndex;
                string noLokasiBarang = grdSaldoGL.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokasiBarang;
                CloseMessage();


                grdSaldoGLDetil.DataSource = ObjDb.GetRows("select a.nosldkas,b.kdrek,b.ket,a.nilai,a.tgl from tsaldokas a inner join mrekening b on a.norek=b.norek WHERE a.nosldkas ='" + hdnId.Value + "'");
                grdSaldoGLDetil.DataBind();

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
                            ObjDb.Where.Add("nosldkas", itemRow);
                            ObjDb.Delete("tsaldokas", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdSaldoGL.Rows)
                    {
                        string itemId = grdSaldoGL.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdSaldoGL.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nosldkas", itemId);
                            ObjDb.Delete("tsaldokas", ObjDb.Where);
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
                foreach (GridViewRow gvrow in grdSaldoGL.Rows)
                {
                    string index = grdSaldoGL.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdSaldoGL.Rows)
            {
                string index = grdSaldoGL.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdSaldoGL.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
            ObjDb.Data.Clear();
            ObjDb.Where.Clear();

            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdSaldoGLDetil.Rows.Count; i++)
                    {
                        TextBox txtnilai = (TextBox)grdSaldoGLDetil.Rows[i].FindControl("txtnilai");
                        TextBox dtkas = (TextBox)grdSaldoGLDetil.Rows[i].FindControl("dtkas");
                        HiddenField hdnnosldkas = (HiddenField)grdSaldoGLDetil.Rows[i].FindControl("hdnnosldkas");

                        if (txtnilai.Text != "")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nosldkas", hdnnosldkas.Value);
                            ObjDb.Data.Add("nilai", txtnilai.Text);
                            ObjDb.Data.Add("tgl", Convert.ToDateTime(dtkas.Text).ToString("yyyy-MM-dd"));
                            ObjDb.Update("tsaldokas", ObjDb.Data, ObjDb.Where);

                        }
                    }
                    this.ShowHideGridAndForm(true, false);
                    ShowMessage("success", "Data berhasil diupdate.");
                    loadData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}