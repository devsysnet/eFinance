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
    public partial class mKategoribrgupd : System.Web.UI.Page
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
        private void SetInitialRow( string kat = "")
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("cbnorek", typeof(string)));
            dt.Columns.Add(new DataColumn("cborekkd", typeof(string)));
            dt.Columns.Add(new DataColumn("cborekdb", typeof(string)));
            dt.Columns.Add(new DataColumn("hdnkategori", typeof(string)));

            DataSet mySet = ObjDb.GetRows("select * from mkategori WHERE kategori ='" + kat + "'");
            foreach (DataRow myRow in mySet.Tables[0].Rows)
            {
                dr = dt.NewRow();
                dr["RowNumber"] = 1;
                dr["Column1"] = myRow["Kategori"].ToString();
                dr["cbnorek"] = myRow["norek"].ToString();
                dr["cborekkd"] = myRow["norekkd"].ToString();
                dr["cborekdb"] = myRow["norekdb"].ToString();
                dr["hdnkategori"] = myRow["noKategori"].ToString();

                dt.Rows.Add(dr);
            }
            ViewState["CurrentTable"] = dt;
            grdKategoriBrgDetil.DataSource = dt;
            grdKategoriBrgDetil.DataBind();

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
                        TextBox txtkategori = (TextBox)grdKategoriBrgDetil.Rows[i].FindControl("txtkategori");
                        DropDownList cbnorek = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cbnorek");
                        DropDownList cborekkd = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekkd");
                        DropDownList cborekdb = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekdb");
                        HiddenField hdnkategori = (HiddenField)grdKategoriBrgDetil.Rows[i].FindControl("hdnkategori");

                        txtkategori.Text = dt.Rows[i]["Column1"].ToString();
                        cbnorek.Text = dt.Rows[i]["cbnorek"].ToString();
                        cborekkd.Text = dt.Rows[i]["cborekkd"].ToString();
                        cborekdb.Text = dt.Rows[i]["cborekdb"].ToString();
                        hdnkategori.Value = dt.Rows[i]["hdnkategori"].ToString();

                        LoadRekDetil(cbojnsBarang.Text);

                    }
                }
            }
        }

        protected void LoadRekDetil(string jsnBarang = "")
        {
            for (int i = 0; i < grdKategoriBrgDetil.Rows.Count; i++)
            {
                DropDownList cbnorek = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cbnorek");
                DropDownList cborekdb = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekdb");
                DropDownList cborekkd = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekkd");

                cborekdb.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Debit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=8) a");
                cborekdb.DataValueField = "id";
                cborekdb.DataTextField = "name";
                cborekdb.DataBind();

                cborekkd.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---Pilih COA (Kredit)---' name union all SELECT distinct norek id,ket  name FROM mRekening where jenis=7 ) a");
                cborekkd.DataValueField = "id";
                cborekkd.DataTextField = "name";
                cborekkd.DataBind();
               
                if (jsnBarang == "1" || jsnBarang == "4")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where Grup='PosisiKeuangan' and pos='1' and jenis='9'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                if (jsnBarang == "2")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='1' and sts='2' and Kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                if (jsnBarang == "3")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where grup = 'Aktivitas' and pos='1' and sts='2' and jenis='20' and Kelompok='PERUBAHAN ASSET NETTO TIDAK TERIKAT'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
                }
                if (jsnBarang == "5")
                {
                    cbnorek.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih COA--' name union all SELECT distinct norek as id, ket as name FROM mrekening where Grup='PosisiKeuangan' and pos='1' and jenis='30'");
                    cbnorek.DataValueField = "id";
                    cbnorek.DataTextField = "name";
                    cbnorek.DataBind();
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
                        TextBox txtkategori = (TextBox)grdKategoriBrgDetil.Rows[i].FindControl("txtkategori");
                        DropDownList cbnorek = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cbnorek");
                        DropDownList cborekkd = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekkd");
                        DropDownList cborekdb = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekdb");
                        HiddenField hdnkategori = (HiddenField)grdKategoriBrgDetil.Rows[i].FindControl("hdnkategori");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtkategori.Text;
                        dtCurrentTable.Rows[i]["cbnorek"] = cbnorek.Text;
                        dtCurrentTable.Rows[i]["cborekkd"] = cborekkd.Text;
                        dtCurrentTable.Rows[i]["cborekdb"] = cborekdb.Text;
                        dtCurrentTable.Rows[i]["hdnkategori"] = hdnkategori.Value;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdKategoriBrgDetil.DataSource = dtCurrentTable;
                    grdKategoriBrgDetil.DataBind();
                }
            }
            SetPreviousData();
        }

        private void loadData()
        {
            grdKategoriBrg.DataSource = ObjDb.GetRows("select case when jns=1 then 'Asset' when jns=2 then 'Non Asset' when jns=3 then 'Jasa' when jns=4 then 'Inventaris' when jns=5 then 'Sales'  end as jns,kategori from mkategori where noKategori not in (select kategoriBarang from mBarang where kategoriBarang = noKategori) and kategori LIKE '%" + txtSearch.Text + "%'");
            grdKategoriBrg.DataBind();
        }
        protected void grdKategoriBrg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKategoriBrg.PageIndex = e.NewPageIndex;
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

        protected void grdKategoriBrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string message = "";
            bool valid = true;
            try
            {
                int rowIndex = grdKategoriBrg.SelectedRow.RowIndex;
                string noLokasiBarang = grdKategoriBrg.DataKeys[rowIndex].Values[0].ToString();
                hdnId.Value = noLokasiBarang;
                CloseMessage();

                DataSet mySet = ObjDb.GetRows("select * from mkategori where kategori = '" + hdnId.Value + "'");
                DataRow myRow = mySet.Tables[0].Rows[0];

                cbojnsBarang.Text = myRow["jns"].ToString();
                SetInitialRow(hdnId.Value);

                //LoadRekDetil(cbojnsBarang.Text);
                
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
                    int cek = 0;
                    ArrayList arrayItem = (ArrayList)ViewState["CHECKED_ITEMS"];
                    if (arrayItem != null)
                    {
                        foreach (string itemRow in arrayItem)
                        {
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("kategori", itemRow);
                            ObjDb.Delete("mkategori", ObjDb.Where);
                        }
                    }
                    foreach (GridViewRow gvrow in grdKategoriBrg.Rows)
                    {
                        string itemId = grdKategoriBrg.DataKeys[gvrow.RowIndex].Value.ToString();
                        CheckBox chkCheck = (CheckBox)grdKategoriBrg.Rows[gvrow.RowIndex].FindControl("chkCheck");
                        if (chkCheck.Checked == true)
                        {
                            cek++;
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("kategori", itemId);
                            ObjDb.Delete("mkategori", ObjDb.Where);
                        }
                    }

                    if (cek == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Data harus dipilih.");
                    }
                    else
                    {
                        /*END DELETE ALL SELECTED*/
                        loadData();
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("success", "Data berhasil dihapus.");
                    }
                }

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
                foreach (GridViewRow gvrow in grdKategoriBrg.Rows)
                {
                    string index = grdKategoriBrg.DataKeys[gvrow.RowIndex].Value.ToString();
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
            foreach (GridViewRow gvrow in grdKategoriBrg.Rows)
            {
                string index = grdKategoriBrg.DataKeys[gvrow.RowIndex].Value.ToString();
                CheckBox chkCheck = (CheckBox)grdKategoriBrg.Rows[gvrow.RowIndex].FindControl("chkCheck");
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
                    for (int i = 0; i < grdKategoriBrgDetil.Rows.Count; i++)
                    {
                        TextBox txtkategori = (TextBox)grdKategoriBrgDetil.Rows[i].FindControl("txtkategori");
                        HiddenField hdnkategori = (HiddenField)grdKategoriBrgDetil.Rows[i].FindControl("hdnkategori");
                        DropDownList cbnorek = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cbnorek");
                        DropDownList cborekkd = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekkd");
                        DropDownList cborekdb = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekdb");

                        DataSet mySet = ObjDb.GetRows("Select * from mBarang a inner join mKategori b on a.kategoriBarang=b.nokategori where b.kategori = '" + txtkategori.Text.Replace("'", "`") + "'");
                        if (mySet.Tables[0].Rows.Count > 0)
                        {
                            ShowMessage("error", "Sudah Ada Transaksi");

                        }
                        else
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Where.Clear();
                            ObjDb.Where.Add("nokategori", hdnkategori.Value);
                            ObjDb.Data.Add("kategori", txtkategori.Text);
                            ObjDb.Data.Add("norek", cbnorek.Text);
                            ObjDb.Data.Add("norekkd", cborekkd.Text);
                            ObjDb.Data.Add("norekdb", cborekdb.Text);
                            ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                            ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                            ObjDb.Update("mkategori", ObjDb.Data, ObjDb.Where);

                            this.ShowHideGridAndForm(true, false);
                            ShowMessage("success", "Data berhasil diupdate.");
                            clearData();
                            loadData();

                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", ex.ToString());
                }

            }
        }
        private void clearData()
        {
            for (int i = 0; i < grdKategoriBrgDetil.Rows.Count; i++)
            {
                TextBox txtkategori = (TextBox)grdKategoriBrgDetil.Rows[i].FindControl("txtkategori");
                HiddenField hdnkategori = (HiddenField)grdKategoriBrgDetil.Rows[i].FindControl("hdnkategori");
                DropDownList cbnorek = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cbnorek");
                DropDownList cborekkd = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekkd");
                DropDownList cborekdb = (DropDownList)grdKategoriBrgDetil.Rows[i].FindControl("cborekdb");

                txtkategori.Text = "";
                cbnorek.Text = "0";
                cborekkd.Text = "0";
                cborekdb.Text = "0";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.ShowHideGridAndForm(true, false);
        }
    }
}