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


namespace eFinance.Pages.Master.Update
{
    public partial class MstApprovalHargaUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void SetInitialRow(string noApprove = "")
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Jabatan", typeof(string)));
                dt.Columns.Add(new DataColumn("drNilai", typeof(string)));
                dt.Columns.Add(new DataColumn("keNilai", typeof(string)));

                DataSet mySet = ObjDb.GetRows("SELECT a.drNilai, a.keNilai, b.hakAkses from MstApproveNilai a "+
                    "inner join mAkses b on a.noAkses = b.noAkses where a.noApprove='" + noApprove + "'  ");
                foreach (DataRow myRow in mySet.Tables[0].Rows)
                {
                    dr = dt.NewRow();
                    dr["Jabatan"] = myRow["hakAkses"].ToString();
                    dr["drNilai"] = ObjSys.IsFormatNumber(myRow["drNilai"].ToString());
                    dr["keNilai"] = ObjSys.IsFormatNumber(myRow["keNilai"].ToString());
                    dt.Rows.Add(dr);
                }
                if (mySet.Tables[0].Rows.Count == 0)
                {
                    dr = dt.NewRow();
                    dr["Jabatan"] = string.Empty;
                    dr["drNilai"] = string.Empty;
                    dr["keNilai"] = string.Empty;
                    dt.Rows.Add(dr);
                }
                ViewState["CurrentTable"] = dt;
                grdApproval.DataSource = dt;
                grdApproval.DataBind();
                SetPreviousData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
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
                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                        cboJabatan.Text = dt.Rows[i]["Jabatan"].ToString();
                        txtdrNilai.Text = dt.Rows[i]["drNilai"].ToString();
                        txtkeNilai.Text = dt.Rows[i]["keNilai"].ToString();
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
                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Jabatan"] = cboJabatan.Text;
                        dtCurrentTable.Rows[i]["drNilai"] = txtdrNilai.Text;
                        dtCurrentTable.Rows[i]["keNilai"] = txtkeNilai.Text;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grdApproval.DataSource = dtCurrentTable;
                    grdApproval.DataBind();
                }
            }
            SetPreviousData();
        }
        private void LoadData()
        {
            grdApprovalScheme.DataSource = ObjDb.GetRows("select * from MstApproveNilai a inner join "+
                "MstParameterApprove b on a.noParameterApprove = b.noParameterApprove inner join "+
                "mUser c on a.noUser = c.noUser inner join mCabang d on d.noCabang = c.noCabang inner join "+
                "mAkses e on e.noAkses = a.noAkses " +
                "where (c.namauser LIKE '%" + txtSearch.Text + "%' "+
                "or b.namaParameterApprove LIKE '%" + txtSearch.Text + "%') and a.peruntukan = '" + cboUntukHeader.Text + "'");
            grdApprovalScheme.DataBind();
        }
        private void PopulateCheckedValues()
        {
            ArrayList userdetails = (ArrayList)ViewState["CHECKED_ITEMS"];
            if (userdetails != null && userdetails.Count > 0)
            {
                foreach (GridViewRow gvrow in grdApprovalScheme.Rows)
                {
                    string index = grdApprovalScheme.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdApprovalScheme.Rows)
            {
                string index = grdApprovalScheme.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdApprovalScheme.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                            ObjDb.Where.Add("noApprove", itemRow);
                            ObjDb.Delete("MstApproveNilai", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdApprovalScheme.Rows)
                    {
                        string itemId = grdApprovalScheme.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdApprovalScheme.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noApprove", itemId);
                            ObjDb.Delete("MstApproveNilai", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                ClearData();
                LoadData();
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
        protected void ClearData()
        {

            CloseMessage();
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
            CloseMessage();
            LoadData();
        }
        
        protected void grdApprovalScheme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SaveCheckedValues();
            grdApprovalScheme.PageIndex = e.NewPageIndex;
            CloseMessage();
            LoadData();
            PopulateCheckedValues();
        }

        protected void grdApprovalScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = grdApprovalScheme.SelectedRow.RowIndex;
                string noApprove = grdApprovalScheme.DataKeys[rowIndex].Values[0].ToString();
                HiddenField hdnnoParameterApprove = (HiddenField)grdApprovalScheme.Rows[rowIndex].FindControl("hdnnoParameterApprove");
                hdnId.Value = noApprove;
                DataSet mySet = ObjDb.GetRows("SELECT a.*,c.kategori from MstApproveNilai a left join MstParameterApprove c on a.noParameterApprove=c.noParameterApprove where a.noApprove='" + noApprove + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                cboCategory.DataSource = ObjDb.GetRows("select a.* from (select '' no,'---Pilih Kategori---' nama " +
                        "union all " +
                        "SELECT distinct noParameterApprove no, namaParameterApprove nama FROM MstParameterApprove) a");
                cboCategory.DataValueField = "no";
                cboCategory.DataTextField = "nama";
                cboCategory.DataBind();

                cboCategory.Text = myRow["noParameterApprove"].ToString();
                cboUntuk.Text = myRow["peruntukan"].ToString();
                
                SetInitialRow(noApprove);
                ShowHideGridAndForm(false, true, false);
                CloseMessage();
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

            if (cboCategory.Text == "0")
            {
                valid = false;
                message += ObjSys.CreateMessage("Kategori harus dipilih.");
            }
            if (cboUntuk.Text == "-")
            {
                valid = false;
                message += ObjSys.CreateMessage("Peruntukan harus dipilih.");
            }

            //int ada = 0, adaisi = 0;
            //for (int i = 0; i < grdApproval.Rows.Count; i++)
            //{
            //    DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
            //    DropDownList cboLevel = (DropDownList)grdApproval.Rows[i].FindControl("cboLevel");

            //    if (cboJabatan.SelectedValue != "0")
            //    {
            //        adaisi = 1;
            //        DataSet mySet = ObjDb.GetRows("Select * from MstApproveNilai where noParameterApprove = '" + cboCategory.Text + "' and noCabang = '" + ObjSys.GetCabangId + "' and noAkses = '" + cboJabatan.Text + "' and noApprove <> '" + hdnId.Value + "'");
            //        if (mySet.Tables[0].Rows.Count > 0)
            //        {
            //            ada = 1;
            //        }
            //    }
            //}
            //if (adaisi == 0)
            //{
            //    valid = false;
            //    message += ObjSys.CreateMessage("Detil harus dipilih.");
            //}
            //if (ada == 1)
            //{
            //    valid = false;
            //    message += ObjSys.CreateMessage("Data sudah terdaftar.");
            //}&& adaisi == 1

            if (valid == true)
            {
                try
                {

                    for (int i = 0; i < grdApproval.Rows.Count; i++)
                    {
                        DropDownList cboJabatan = (DropDownList)grdApproval.Rows[i].FindControl("cboJabatan");
                        TextBox txtdrNilai = (TextBox)grdApproval.Rows[i].FindControl("txtdrNilai");
                        TextBox txtkeNilai = (TextBox)grdApproval.Rows[i].FindControl("txtkeNilai");

                        string drNilai = "0", keNilai = "0";
                        if (txtdrNilai.Text != "")
                            drNilai = txtdrNilai.Text;
                        if (txtkeNilai.Text != "")
                            keNilai = txtkeNilai.Text;

                        if (cboJabatan.SelectedValue != "0")
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Data.Clear();
                            ObjDb.Where.Add("noApprove", hdnId.Value);
                            ObjDb.Data.Add("drNilai", Convert.ToDecimal(drNilai).ToString());
                            ObjDb.Data.Add("keNilai", Convert.ToDecimal(keNilai).ToString());
                            ObjDb.Data.Add("modifiedBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("modifiedDate", ObjSys.GetNow);
                            ObjDb.Update("MstApproveNilai", ObjDb.Data, ObjDb.Where);

                        }
                    }

                    LoadData();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil diupdate.");
                    this.ShowHideGridAndForm(true, false, false);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Invalid transaction data to database.");
                }
            }

        }
       
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ShowHideGridAndForm(true, false, false);
            CloseMessage();
        }

    }
}