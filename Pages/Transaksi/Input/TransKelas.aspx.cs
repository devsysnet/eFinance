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

namespace eFinance.Pages.Transaksi.Input
{
    public partial class TransKelas : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //loadDataFirst();
                loadCombo();
                button.Visible = false;
            }
            
        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("tahunajaran", cboTA.Text);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDataKelasSiswa", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
            if (grdKelasSiswa.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

            loadDataCombo();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void loadCombo()
        {

                cboTA.DataSource = ObjDb.GetRows("select distinct tahunAjaran as id, tahunAjaran as name from Parameter");
                cboTA.DataValueField = "id";
                cboTA.DataTextField = "name";
                cboTA.DataBind();
            }
        protected void loadDataCombo()
        {
            
            for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
            {
                HiddenField hdnJenisSekolah = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnJenisSekolah");
                DropDownList cboJnsSekolah = (DropDownList)grdKelasSiswa.Rows[i].FindControl("cboJnsSekolah");

                string sql = "";
                if (hdnJenisSekolah.Value == "SMA")
                    sql = "select '10' as name union select '11' as name union select '12' as name union select 'Siswa Baru' as name union select 'Siswa Lulus' as name";
                else if (hdnJenisSekolah.Value == "SMP")
                    sql = "select '7' as name union select '8' as name union select '9' as name union select 'Siswa Baru' as name union select 'Siswa Lulus' as name";
                else if (hdnJenisSekolah.Value == "SD")
                    sql = "select '1' as name union select '2' as name union select '3' as name union select '4' as name union select '5' as name union select '6' as name union select 'Siswa Baru' as name union select 'Siswa Lulus' as name";
                else if (hdnJenisSekolah.Value == "TK")
                    sql = "select 'A' as name union select 'B' as name union select 'PG' as name union select 'Siswa Baru'  as name union select 'Siswa Lulus' as name";

                cboJnsSekolah.DataSource = ObjDb.GetRows(sql);
                cboJnsSekolah.DataValueField = "name";
                cboJnsSekolah.DataTextField = "name";
                cboJnsSekolah.DataBind();

                cboTA.DataSource = ObjDb.GetRows("select distinct tahunAjaran as id, tahunAjaran as name from Parameter");
                cboTA.DataValueField = "id";
                cboTA.DataTextField = "name";
                cboTA.DataBind();
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

        protected void grdKelasSiswa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CloseMessage();
            grdKelasSiswa.PageIndex = e.NewPageIndex;
            loadDataFirst();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < grdKelasSiswa.Rows.Count; i++)
                {
                    
                    HiddenField hdnNoSiswa = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnNoSiswa");
                    HiddenField hdnJenisSekolah = (HiddenField)grdKelasSiswa.Rows[i].FindControl("hdnJenisSekolah");
                    DropDownList cboJnsSekolah = (DropDownList)grdKelasSiswa.Rows[i].FindControl("cboJnsSekolah");
                    CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                    DropDownList cboJnsSekolahx = (DropDownList)grdKelasSiswa.Rows[i].FindControl("cboJnsSekolah");
                    TextBox txtKelasKe = (TextBox)grdKelasSiswa.Rows[i].FindControl("txtKelasKe");

                    if (chkCheck.Checked == true)
                    {
                        count++;
                        string kelas = "";
                        kelas = cboJnsSekolahx.SelectedValue + " " + txtKelasKe.Text;
                        ObjDb.Data.Clear();
                        ObjDb.Data.Add("noSiswa", hdnNoSiswa.Value);
                        ObjDb.Data.Add("kelas", kelas);
                        ObjDb.Data.Add("tahunAjaran", cboTA.Text);
                        ObjDb.Data.Add("noCabang", ObjSys.GetCabangId);
                        ObjDb.Data.Add("createdBy", ObjSys.GetUserId);
                        ObjDb.Data.Add("createdDate", ObjSys.GetNow);
                        ObjDb.Data.Add("sts", "1");
                        ObjDb.Insert("TransKelas", ObjDb.Data);
                    }
                }
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("error", "Data harus dipilih.");
                }
                else
                {
                    loadDataFirst();
                    ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                    ShowMessage("success", "Data berhasil disimpan.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "ToTheTop", "ToTopOfPage();", true);
                ShowMessage("error", ex.Message);
            }
        }
    }
}