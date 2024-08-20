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

namespace eFinance.Pages.Transaksi.View
{
    public partial class TransSuratTugasView : System.Web.UI.Page
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
                cboParent.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noKaryawan id, nama name FROM Mstkaryawan where jabatan = '1' ) a");
                cboParent.DataValueField = "id";
                cboParent.DataTextField = "name";
                cboParent.DataBind();

                mhs.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noKaryawan id, nama name FROM Mstkaryawan where jabatan != '1' ) a");
                mhs.DataValueField = "id";
                mhs.DataTextField = "name";
                mhs.DataBind();

                cboUnit.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'--Pilih--' name union all SELECT distinct noJenisTugas id, JenisTugas name FROM MstJenistugas ) a");
                cboUnit.DataValueField = "id";
                cboUnit.DataTextField = "name";
                cboUnit.DataBind();
            }
        }
        protected void loadData()
        {
            grdBudget.DataSource = ObjDb.GetRows("select a.kodetugas,a.noTugas,a.tgl,a.drtgl,a.sptgl,b.JenisTugas,c.nama from TransTugaskaryawan a inner join MstJenistugas b on a.noJenisTugas = b.noJenisTugas inner join MstKaryawan c on a.noKaryawan = c.nokaryawan where c.nama LIKE '%" + txtSearch.Text + "%'");
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
                DataSet MySet = ObjDb.GetRows("select * from TransTugaskaryawan where notugas = '" + noKurs + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    date.Text = Convert.ToDateTime(MyRow["tgl"].ToString()).ToString("dd-MMM-yyyy");
                    fromDate.Text = Convert.ToDateTime(MyRow["drtgl"].ToString()).ToString("dd-MMM-yyyy");

                    toDate.Text = Convert.ToDateTime(MyRow["sptgl"].ToString()).ToString("dd-MMM-yyyy");

                    kdSuratTugas.Text = MyRow["kodeTugas"].ToString();
                    cboUnit.Text = MyRow["noJenisTugas"].ToString();
                    cboParent.Text = MyRow["noAtasan"].ToString();
                    mhs.Text = MyRow["noKaryawan"].ToString();
                    deskripsi.Text = MyRow["diskripsi"].ToString();

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
                            ObjDb.Where.Add("noTugas", itemRow);
                            ObjDb.Delete("TransTugaskaryawan", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdBudget.Rows)
                    {
                        string itemId = grdBudget.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdBudget.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noTugas", itemId);
                            ObjDb.Delete("TransTugaskaryawan", ObjDb.Where);
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
                    ObjDb.Where.Add("noTugas", hdnId.Value);
                    ObjDb.Data.Add("tgl", date.Text);
                    ObjDb.Data.Add("drtgl", fromDate.Text);
                    ObjDb.Data.Add("sptgl", toDate.Text);
                    ObjDb.Data.Add("noJenisTugas", cboUnit.Text);
                    ObjDb.Data.Add("noatasan", cboParent.Text);
                    ObjDb.Data.Add("noKaryawan", mhs.Text);
                    ObjDb.Data.Add("diskripsi", deskripsi.Text);
                    ObjDb.Update("TransTugaskaryawan", ObjDb.Data, ObjDb.Where);

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

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
        protected void clearData()
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            LoadDataParent();
            dlgParentAccount.Show();
        }
    }
}