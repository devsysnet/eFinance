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
    public partial class MstJenisKegiatanUpdate : System.Web.UI.Page
    {
        protected string execBind = string.Empty;
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private void SetInitialRow(string noTransKas = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("RowNumber2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("RowNumber3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            DataSet mySet = ObjDb.GetRows("select b.namaKegiatan,c.ket,b.noMKegiatanD from mJenisKegiatan a inner join mJenisKegiatanD b on a.noMkegiatan=b.noMkegiatan inner join mRekening c on b.norek = c.noRek  WHERE a.noMkegiatan = '" + noTransKas + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["namaKegiatan"].ToString();
                dr["RowNumber2"] = 2;
                dr["Column2"] = myRow["ket"].ToString();
                dr["RowNumber3"] = 3;
                dr["Column3"] = myRow["noMKegiatanD"].ToString();

                dt.Rows.Add(dr);
            }
            if (mySet.Tables[0].Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = string.Empty;
                dr["RowNumber2"] = 2;
                dr["Column2"] = string.Empty;
                dr["RowNumber3"] = 3;
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

                        txtInstansi.Text = dt.Rows[i]["Column1"].ToString();
                        TextBox hdnNoMKegiatanD = (TextBox)grdInstansi.Rows[i].FindControl("hdnNoMKegiatanD");

                        hdnNoMKegiatanD.Text = dt.Rows[i]["Column3"].ToString();
                        DropDownList txtRekening = (DropDownList)grdInstansi.Rows[i].FindControl("txtRekening");

                        setUnitData(txtRekening);
                        ListItem check = txtRekening.Items.FindByText(dt.Rows[i]["Column2"].ToString());
                        if (check != null)
                        {
                            check.Selected = true;
                        }
                        else
                        {
                            txtRekening.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        private void AddNewRow()
        {
            //if (ViewState["CurrentTable"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            //        {
            //            TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");

            //            drCurrentRow = dtCurrentTable.NewRow();
            //            dtCurrentTable.Rows[i]["Column1"] = txtInstansi.Text;
            //        }
            //        dtCurrentTable.Rows.Add(drCurrentRow);
            //        ViewState["CurrentTable"] = dtCurrentTable;
            //        grdInstansi.DataSource = dtCurrentTable;
            //        grdInstansi.DataBind();
            //    }
            //}
            //SetPreviousData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            execBind = Page.ClientScript.GetPostBackEventReference(cmdMode, string.Empty);
            if (!IsPostBack)
            {

                loadData();
                LoadDataSearch();
            }
        }
        protected void loadData()
        {
            grdAsset.DataSource = ObjDb.GetRows("select distinct a.namaKegiatan,a.noMkegiatan from mJenisKegiatan a inner join mJenisKegiatanD b on a.noMkegiatan=b.noMkegiatan where  (a.namaKegiatan LIKE '%" + txtSearch.Text + "%')");
            grdAsset.DataBind();
        }

        #region brand
        protected void btnCariBrand_Click(object sender, EventArgs e)
        {
            LoadDataSearch();
            mpe2.Show();
        }

        protected void grdBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBrand.PageIndex = e.NewPageIndex;
            LoadDataSearch();
            mpe2.Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtBrand.Text == "")
            {
                message += ObjSys.CreateMessage("Brand tidak boleh kosong.");
                valid = false;
                mpe2.Show();
            }
            if (valid == true)
            {
                try
                {
                    if (ObjDb.GetRows("select * from mLokasi where nocabang='" + ObjSys.GetCabangId + "' and Lokasi = '" + txtBrand.Text + "'").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Lokasi sudah ada.");
                    }
                    else
                    {

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("Lokasi", txtBrand.Text);
                        ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Insert("mLokasi", ObjDb.Data);

                        mpe2.Show();
                        txtBrand.Text = "";
                        LoadDataSearch();
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", message);
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

        protected void grdBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detail")
            {
                try
                {
                    int rowIndex = int.Parse(e.CommandArgument.ToString());
                    string itemId = grdBrand.DataKeys[rowIndex].Value.ToString();

                    Label lblInstansi = (Label)grdBrand.Rows[rowIndex].FindControl("lblInstansi");
                    HiddenField hdnInstansi = (HiddenField)grdBrand.Rows[rowIndex].FindControl("hdnInstansi");

                    txtBrand.Text = lblInstansi.Text;
                    hdnBrand.Value = hdnInstansi.Value;
                    mpe2.Show();
                    btnUpdateBrand.Visible = true;
                    btnSave.Visible = false;
                }
                catch (Exception ex)
                {
                    Response.Write("Error:" + ex.ToString());
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Data is not valid');", true);
                }
            }
        }

        protected void btnUpdateBrand_Click(object sender, EventArgs e)
        {

        }
        protected void grdBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = (GridViewRow)grdBrand.Rows[e.RowIndex];
                HiddenField hdnInstansi = (HiddenField)row.FindControl("hdnInstansi");

                ObjDb.Where.Clear();
                ObjDb.Where.Add("noLokasi", hdnInstansi.Value);
                ObjDb.Delete("mLokasi", ObjDb.Where);
                mpe2.Show();
                LoadDataSearch();
            }
        }
        protected void LoadDataSearch()
        {
            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMkegiatan id, namaKegiatan name FROM mJenisKegiatan ) a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();

            //txtRekening.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noRek id, ket name FROM mRekening ) a");
            //txtRekening.DataValueField = "id";
            //txtRekening.DataTextField = "name";
            //txtRekening.DataBind();
        }
        protected void setUnitData(DropDownList txtRekening)
        {
            txtRekening.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noRek id, ket name FROM mRekening ) a");
            txtRekening.DataValueField = "id";
            txtRekening.DataTextField = "name";
            txtRekening.DataBind();
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            mpe2.Show();
            LoadDataSearch();
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
                            ObjDb.Where.Add("noMkegiatan", itemRow);
                            ObjDb.Delete("mJenisKegiatan", ObjDb.Where);
                            ObjDb.Delete("mJenisKegiatanD", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdAsset.Rows)
                    {
                        string itemId = grdAsset.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdAsset.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("noMkegiatan", itemId);
                            ObjDb.Delete("mJenisKegiatan", ObjDb.Where);
                            ObjDb.Delete("mJenisKegiatanD", ObjDb.Where);
                        }
                    }
                }
                /*END DELETE ALL SELECTED*/
                //loadData();
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("success", "Data berhasil dihapus.");
                loadData();
                LoadDataSearch();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.ToString());
            }
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
                LoadDataSearch();
                int rowIndex = grdAsset.SelectedRow.RowIndex;
                string noKurs = grdAsset.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noKurs;
                DataSet MySet = ObjDb.GetRows("select a.namaKegiatan from mJenisKegiatan a inner join mJenisKegiatanD b on a.noMkegiatan=b.noMkegiatan inner join mRekening c on b.norek = c.noRek where a.noMkegiatan = '" + noKurs + "'");
                if (MySet.Tables[0].Rows.Count > 0)
                {
                    DataRow MyRow = MySet.Tables[0].Rows[0];
                    cboLokasi.SelectedItem.Text = MyRow["namaKegiatan"].ToString();

                    SetInitialRow(hdnId.Value);
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

        protected void btnCari_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            //if (cboLokasi.Text == "0")
            //{
            //    message += ObjSys.CreateMessage("Lokasi tidak boleh kosong.");
            //    valid = false;
            //}
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        TextBox hdnNoMKegiatanD = (TextBox)grdInstansi.Rows[i].FindControl("hdnNoMKegiatanD");
                        DropDownList txtRekening = (DropDownList)grdInstansi.Rows[i].FindControl("txtRekening");
                        
                        
                            ObjDb.Where.Add("noMkegiatanD", hdnNoMKegiatanD.Text);
                            ObjDb.Data.Add("norek", Convert.ToDecimal(txtRekening.SelectedItem.Value).ToString());
                            ObjDb.Data.Add("namaKegiatan", txtInstansi.Text);
                            ObjDb.Update("mJenisKegiatanD", ObjDb.Data, ObjDb.Where);
                        ObjDb.Data.Clear();
                        ObjDb.Where.Clear();

                    }
                    ShowMessage("success", "Data berhasil disimpan.");
                    //clearData();
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            loadData();
            LoadDataSearch();
            this.ShowHideGridAndForm(true, false);
        }
    }
}