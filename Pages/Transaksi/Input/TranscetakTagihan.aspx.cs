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
    public partial class TranscetakTagihan : System.Web.UI.Page
    {
        private Database ObjDb = new Database();
        private GlobalLibrary ObjGlobal = new GlobalLibrary();
        private Systems ObjSys = new Systems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDataCombo();
                button.Visible = false;
            }

        }

        protected void loadDataFirst()
        {
            ObjGlobal.Param.Clear();
            ObjGlobal.Param.Add("Search", txtSearch.Text);
            ObjGlobal.Param.Add("kelas", cboKelas.Text);
            ObjGlobal.Param.Add("fromMonth", cbofromMonth.Text);
            ObjGlobal.Param.Add("fromYear", cbofromYear.Text);
            ObjGlobal.Param.Add("Cabang", ObjSys.GetCabangId);
            grdKelasSiswa.DataSource = ObjGlobal.GetDataProcedure("SPLoadDatapPiutangSiswa", ObjGlobal.Param);
            grdKelasSiswa.DataBind();
            if (grdKelasSiswa.Rows.Count > 0)
                button.Visible = true;
            else
                button.Visible = false;

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CloseMessage();
            loadDataFirst();
        }

        protected void loadDataCombo()
        {

            cboKelas.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih Kelas---' as name union select distinct kelas as id, kelas as name from TransKelas where nocabang='" + ObjSys.GetCabangId + "')x");
            cboKelas.DataValueField = "id";
            cboKelas.DataTextField = "name";
            cboKelas.DataBind();

            cbofromMonth.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih Bulan---' as name union select distinct month(tgl) as id, FORMAT(tgl, 'MMMM') as name from TransPiutang where nocabang='" + ObjSys.GetCabangId + "')x ");
            cbofromMonth.DataValueField = "id";
            cbofromMonth.DataTextField = "name";
            cbofromMonth.DataBind();

            cbofromYear.DataSource = ObjDb.GetRows("select * from (select '0' as id, '---Pilih Tahun---' as name union select distinct year(tgl) as id, CONVERT(varchar,year(tgl)) as name from TransPiutang where nocabang = '" + ObjSys.GetCabangId + "')x");
            cbofromYear.DataValueField = "id";
            cbofromYear.DataTextField = "name";
            cbofromYear.DataBind();



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
                    CheckBox chkCheck = (CheckBox)grdKelasSiswa.Rows[i].FindControl("chkCheck");
                    TextBox txtcatatan = (TextBox)grdKelasSiswa.Rows[i].FindControl("txtcatatan");

                    if (chkCheck.Checked == true)
                    {
                        count++;
                        ObjDb.Where.Clear();
                        ObjDb.Data.Clear();
                        ObjDb.Where.Add("nopiutang", hdnNoSiswa.Value);
                        ObjDb.Data.Add("catatan", txtcatatan.Text);
                        ObjDb.Data.Add("stscetak", "1");
                        ObjDb.Update("transpiutang", ObjDb.Data, ObjDb.Where);

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