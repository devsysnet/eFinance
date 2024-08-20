using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace eFinance.Pages.Master.Input
{
    public partial class MstJenisKegiatanInsert : System.Web.UI.Page
    {
        Database ObjDb = new Database();
        Systems ObjSys = new Systems();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("RowNumber2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Column1"] = string.Empty;
            dr["RowNumber2"] = 2;
            dr["Column2"] = string.Empty;
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
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");

                        txtInstansi.Text = dt.Rows[i]["Column1"].ToString();
                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                        cboRekening.DataSource = ObjDb.GetRows("select '0' as id, '--Pilih Rekening--' name union all SELECT distinct noRek as id, Ket as name FROM mRekening where grup='Aktivitas' and sts='2' and pos='1'");
                        cboRekening.DataValueField = "id";
                        cboRekening.DataTextField = "name";
                        cboRekening.DataBind();


                        cboRekening.Text = dt.Rows[i]["Column2"].ToString();
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

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column1"] = txtInstansi.Text;
                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i]["Column2"] = cboRekening.Text;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    grdInstansi.DataSource = dtCurrentTable;
                    grdInstansi.DataBind();
                }
            }
            SetPreviousData();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ObjSys.SessionCheck("MstJenisKegiatanInsert.aspx");
                LoadDataSearch();
                SetInitialRow();
                for (int i = 1; i < 5; i++)
                {
                    AddNewRow();
                }
            }
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
            int count = 0, count2 = 0;

            if (cboLokasi.Text == "0")
            {
                message += ObjSys.CreateMessage("Kegiatan tidak boleh kosong.");
                valid = false;
            }
            if (valid == true)
            {
                try
                {
                    for (int i = 0; i < grdInstansi.Rows.Count; i++)
                    {
                        TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                        TextBox txtkode = (TextBox)grdInstansi.Rows[i].FindControl("txtkode");

                        DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");
                        if (cboRekening.Text != "0")
                        {
                            ObjDb.Data.Clear();
                            ObjDb.Data.Add("noMkegiatan", cboLokasi.Text);
                            ObjDb.Data.Add("namaKegiatan", txtInstansi.Text);
                            ObjDb.Data.Add("kodeKegiatan", txtkode.Text);
                            ObjDb.Data.Add("norek", cboRekening.Text);
                            ObjDb.Insert("mJenisKegiatanD", ObjDb.Data);
                            message += ObjSys.CreateMessage("Baris " + (i + 1) + " <b>" + txtInstansi.Text + "</b> data tersimpan.");
                            count += 1;
                        }

                         
                    }

                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    if (count == 0)
                    {
                        ShowMessage("error", "Data gagal disimpan." + message);
                    }
                    else if (count2 > 0)
                    {
                        ShowMessage("warning", "Data berhasil disimpan." + message);
                    }
                    else
                    {
                        ShowMessage("success", "Data berhasil disimpan.");
                        clearData();
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
        private void clearData()
        {
            for (int i = 0; i < grdInstansi.Rows.Count; i++)
            {
                DropDownList cboRekening = (DropDownList)grdInstansi.Rows[i].FindControl("cboRekening");

                cboRekening.Text = "0";
                TextBox txtInstansi = (TextBox)grdInstansi.Rows[i].FindControl("txtInstansi");
                TextBox txtkode = (TextBox)grdInstansi.Rows[i].FindControl("txtkode");

                txtInstansi.Text = "";
                txtkode.Text = "";
                cboLokasi.Text = "0";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearData();
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
                message += ObjSys.CreateMessage("Jenis Kegiatan tidak boleh kosong.");
                valid = false;
                mpe2.Show();
            }
            if (valid == true)
            {
                try
                {
                    if (ObjDb.GetRows("select * from mJenisKegiatan where namaKegiatan = '" + txtBrand.Text + "' and nocabang='" + ObjSys.GetCabangId + "' ").Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                        ShowMessage("error", "Jenis Kegiatan sudah ada.");
                    }
                    else
                    {

                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("namaKegiatan", txtBrand.Text);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Data.Add("nocabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("sts", "1");
                        ObjDb.Insert("mJenisKegiatan", ObjDb.Data);

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
            string message = "";
            bool valid = true;
            ObjDb.Data.Clear();

            if (txtBrand.Text == "")
            {
                message += ObjSys.CreateMessage("Jenis Kegiatan tidak boleh kosong.");
                valid = false;
                mpe2.Show();
            }
            if (valid == true)
            {
                try
                {
                    ObjDb.Data.Clear();
                    ObjDb.Where.Clear();
                    ObjDb.Where.Add("noMkegiatan", hdnBrand.Value);
                    ObjDb.Data.Add("namaKegiatan", txtBrand.Text);
                    ObjDb.Data.Add("sts", "1");
                    ObjDb.Data.Add("modiBy", ObjSys.GetUserId);
                    ObjDb.Data.Add("modiDate", ObjSys.GetNow);
                    ObjDb.Update("mJenisKegiatan", ObjDb.Data, ObjDb.Where);

                    mpe2.Show();
                    txtBrand.Text = "";
                    LoadDataSearch();
                    btnUpdateBrand.Visible = false;
                    btnSave.Visible = true;
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

        protected void grdBrand_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow row = (GridViewRow)grdBrand.Rows[e.RowIndex];
                HiddenField hdnInstansi = (HiddenField)row.FindControl("hdnInstansi");

                ObjDb.Where.Clear();
                ObjDb.Where.Add("noMkegiatan", hdnInstansi.Value);
                ObjDb.Delete("mJenisKegiatan", ObjDb.Where);
                mpe2.Show();
                LoadDataSearch();
            }
        }
        protected void LoadDataSearch()
        {
            grdBrand.DataSource = ObjDb.GetRows("select * from mJenisKegiatan where namaKegiatan like '%" + TextBox2.Text + "%' and  nocabang='" + ObjSys.GetCabangId + "'");
            grdBrand.DataBind();

            cboLokasi.DataSource = ObjDb.GetRows("select a.* from (select '0' id,'---' name union all SELECT distinct noMkegiatan id, namaKegiatan name FROM mJenisKegiatan where nocabang='" + ObjSys.GetCabangId + "') a");
            cboLokasi.DataValueField = "id";
            cboLokasi.DataTextField = "name";
            cboLokasi.DataBind();
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            mpe2.Show();
            LoadDataSearch();
        }
    }
}